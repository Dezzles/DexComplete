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
		private readonly Services.ServerService ServerService_;
		private readonly Services.PokedexService PokedexService_;
		public PokedexController(Services.ServerService ServerService,
			Services.PokedexService PokedexService)
		{
			this.ServerService_ = ServerService;
			this.PokedexService_ = PokedexService;
		}

		[HttpGet, Route("pokedex/{pokedexId}")]
		public Response GetPokedexByGameAndId(string pokedexId)
		{
			ServerService_.ThrowMaintenance();
			return Response.Succeed(PokedexService_.GetPokedex(pokedexId));
		}
	}
}