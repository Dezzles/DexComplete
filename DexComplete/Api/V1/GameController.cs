using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using DexComplete.Transfer;
using SharpLogging;
using DexComplete.Utilities;

namespace DexComplete.Api.V1
{
	[RoutePrefix("api/v1")]
	public class GameController : ApiController
	{
		[HttpGet, Route("games/list")]
		public Response GetAllGames(SLLog Log = null)
		{
			Log = Logging.GetLog(Log);
			View.ServerRepository.ThrowMaintenance(Log);
			var games = View.GameRepository.GetGames(Log);
			return Response.Succeed(games);
		}

		[HttpGet, Route("game/{gameName}/dex")]
		public Response GetGameDexList(string gameName, SLLog Log = null)
		{
			Log = Logging.GetLog(Log);
			View.ServerRepository.ThrowMaintenance(Log);
			var dexList = View.PokedexRepository.GetPokedexesByGame(gameName, Log);
			return Response.Succeed(dexList);
		}

		[HttpGet, Route("game/{gameName}/dex/{dexName}")]
		public Response GetGamePokedex(string gameName, string dexName, SLLog Log = null)
		{
			Log = Logging.GetLog(Log);
			View.ServerRepository.ThrowMaintenance(Log);
			var games = View.PokedexRepository.GetPokedexByName(gameName, dexName, Log);
			return Response.Succeed(games);
		}

		[HttpGet, Route("game/{gameName}/allTools")]
		public Response GetGameTools(string gameName, SLLog Log = null)
		{
			Log = Logging.GetLog(Log);
			View.ServerRepository.ThrowMaintenance(Log);
			var result = View.GameRepository.GetGameTools(gameName, Log);
			return Response.Succeed(result);
		}

	}
}