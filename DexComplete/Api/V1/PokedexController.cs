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
		[HttpGet, Route("pokedexes")]
		public Response GetAllPokedexes()
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);
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

		[HttpGet, Route("pokedex/{pokedexId}")]
		public Response GetPokedexByGameAndId(string pokedexId)
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);
			return Response.Succeed(Services.PokedexService.GetPokedex(pokedexId, Log));
		}
	}
}