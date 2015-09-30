using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using DexComplete.Transfer;
namespace DexComplete.Api.V1
{
	[RoutePrefix("api/v1")]
	public class GameController : ApiController
	{
		[HttpGet, Route("games/list")]
		public Response GetAllGames()
		{
			View.ServerRepository.ThrowMaintenance();
			var games = View.GameRepository.GetGames();
			return Response.Succeed(games);
		}

		[HttpGet, Route("game/{gameName}/dex")]
		public Response GetGameDexList(string gameName)
		{
			View.ServerRepository.ThrowMaintenance();
			var dexList = View.PokedexRepository.GetPokedexesByGame(gameName);
			return Response.Succeed(dexList);
		}

		[HttpGet, Route("game/{gameName}/dex/{dexName}")]
		public Response GetGamePokedex(string gameName, string dexName)
		{
			View.ServerRepository.ThrowMaintenance();
			var games = View.PokedexRepository.GetPokedexByName(gameName, dexName);
			return Response.Succeed(games);
		}

		[HttpGet, Route("game/{gameName}/allTools")]
		public Response GetGameTools(string gameName)
		{
			View.ServerRepository.ThrowMaintenance();
			var result = View.GameRepository.GetGameTools(gameName);
			return Response.Succeed(result);
		}

	}
}