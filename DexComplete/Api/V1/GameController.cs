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
		private readonly Services.ServerService ServerService_;
		private readonly Services.GameService GameService_;
		private readonly Services.PokedexService PokedexService_;
		public GameController(Services.ServerService ServerService,
			Services.GameService GameService, Services.PokedexService PokedexService)
		{
			this.ServerService_ = ServerService;
			this.GameService_ = GameService;
			this.PokedexService_ = PokedexService;
		}

		[HttpGet, Route("games/list")]
		public Response GetAllGames()
		{
			ServerService_.ThrowMaintenance();
			var games = GameService_.GetGames();
			return Response.Succeed(games, false);
		}

		[HttpGet, Route("game/{gameName}/dex")]
		public Response GetGameDexList(string gameName )
		{
			ServerService_.ThrowMaintenance();
			var dexList = PokedexService_.GetPokedexesByGame(gameName);
			return Response.Succeed(dexList, false);
		}

		[HttpGet, Route("game/{gameName}/dex/{dexName}")]
		public Response GetGamePokedex(string gameName, string dexName)
		{
			ServerService_.ThrowMaintenance();
			var games = PokedexService_.GetPokedex(dexName);
			return Response.Succeed(games, false);
		}

		[HttpGet, Route("game/{gameName}/allTools")]
		public Response GetGameTools(string gameName)
		{
			ServerService_.ThrowMaintenance();
			var result = GameService_.GetGameTools(gameName);
			return Response.Succeed(result, false);
		}

	}
}