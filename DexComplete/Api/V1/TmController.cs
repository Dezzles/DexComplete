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
		public TmController(Services.ServerService ServerService,
			Services.TmService TmService)
		{
			this.TmService_ = TmService;
			this.ServerService_ = ServerService;
		}

		[HttpGet, Route("tms/{gameId}")]
		public Response GetTmList(string gameId)
		{
			ServerService_.ThrowMaintenance();
			var result = TmService_.GetTmsByGame(gameId);
			return Response.Succeed(result, false);
		}

	}
}
