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
			var Log = Logging.GetLog();
			ServerService_.ThrowMaintenance(Log);
			var games = GameService_.GetGames(Log);
			return Response.Succeed(games);
		}

		[HttpGet, Route("game/{gameName}/dex")]
		public Response GetGameDexList(string gameName )
		{
			var Log = Logging.GetLog();
			ServerService_.ThrowMaintenance(Log);
			var dexList = PokedexService_.GetPokedexesByGame(gameName, Log);
			return Response.Succeed(dexList);
		}

		[HttpGet, Route("game/{gameName}/dex/{dexName}")]
		public Response GetGamePokedex(string gameName, string dexName)
		{
			var Log = Logging.GetLog();
			ServerService_.ThrowMaintenance(Log);
			var games = PokedexService_.GetPokedex(dexName, Log);
			return Response.Succeed(games);
		}

		[HttpGet, Route("game/{gameName}/allTools")]
		public Response GetGameTools(string gameName)
		{
			var Log = Logging.GetLog();
			ServerService_.ThrowMaintenance(Log);
			var result = GameService_.GetGameTools(gameName, Log);
			return Response.Succeed(result);
		}

	}
}