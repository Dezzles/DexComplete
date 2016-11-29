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
	public class TmController : ApiController
	{
		private readonly Services.ServerService ServerService_;
		private readonly Services.TmService TmService_;
		private readonly Cache Cache_;
		public TmController(Services.ServerService ServerService,
			Services.TmService TmService, Cache Cache)
		{
			this.TmService_ = TmService;
			this.Cache_ = Cache;
			this.ServerService_ = ServerService;
		}

		[HttpGet, Route("tms/{gameId}")]
		public Response GetTmList(string gameId)
		{
			ServerService_.ThrowMaintenance();
			//string name = Helpers.GetCurrentMethod(gameId);
			var result = Cache_.GetResult(TmService_.GetTmsByGame, gameId);
			return Response.Succeed(result, false);
		}

	}
}
