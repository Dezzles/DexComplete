using DexComplete.Transfer;
using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DexComplete.Api.V1
{
	[RoutePrefix("api/v1")]
	public class BerryController : ApiController
	{
		private readonly Services.ServerService ServerService_;
		private readonly Services.BerryService BerryService_;
		public BerryController(Services.ServerService ServerService, 
			Services.BerryService BerryService)
		{
			this.ServerService_ = ServerService;
			this.BerryService_ = BerryService;
		}

		[HttpGet, Route("berries/{gameId}")]
		public Response GetBerryList(string gameId)
		{
			ServerService_.ThrowMaintenance();
			var result = BerryService_.GetBerriesByGame(gameId);
			return Response.Succeed(result, false);
		}

	}
}
