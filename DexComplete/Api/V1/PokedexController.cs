using DexComplete.Transfer;
using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace DexComplete.Api.V1
{
	[RoutePrefix("api/v1")]
	public class PokedexController : ApiController
	{
		[HttpGet, Route("pokedex/all")]
		public Response GetAllPokedexes(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			View.ServerRepository.ThrowMaintenance(Log);
			Data.PokedexModel ctr = new Data.PokedexModel();
			StringBuilder sb = new StringBuilder();
			foreach (var game in ctr.Games)
			{
				sb.AppendLine(game.Title);
				foreach (var dex in game.Pokedexes)
				{
					sb.AppendFormat("\t- {0}\n", dex.Title);
				}
				sb.AppendLine();
			}
			return Response.Succeed(sb.ToString());
		}

		[HttpGet, Route("pokedex/{gameId}/{pokedexId}")]
		public Response GetPokedexByGameAndId(string gameId, string pokedexId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			View.ServerRepository.ThrowMaintenance(Log);
			return Response.Succeed(View.PokedexRepository.GetPokedex(gameId, pokedexId, Log));
		}
	}
}