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
		public Response GetAllGames()
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);
			var games = Services.GameService.GetGames(Log);
			return Response.Succeed(games);
		}

		[HttpGet, Route("game/{gameName}/dex")]
		public Response GetGameDexList(string gameName )
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);
			var dexList = Services.PokedexService.GetPokedexesByGame(gameName, Log);
			return Response.Succeed(dexList);
		}

		[HttpGet, Route("game/{gameName}/dex/{dexName}")]
		public Response GetGamePokedex(string gameName, string dexName)
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);
			var games = Services.PokedexService.GetPokedex(dexName, Log);
			return Response.Succeed(games);
		}

		[HttpGet, Route("game/{gameName}/allTools")]
		public Response GetGameTools(string gameName)
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);
			var result = Services.GameService.GetGameTools(gameName, Log);
			return Response.Succeed(result);
		}

	}
}