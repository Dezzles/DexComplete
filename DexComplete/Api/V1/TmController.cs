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
		[HttpGet, Route("tms/{gameId}")]
		public Response GetTmList(string gameId)
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);
			var result = Services.TmService.GetTmsByGame(gameId, Log);
			return Response.Succeed(result);
		}

	}
}
