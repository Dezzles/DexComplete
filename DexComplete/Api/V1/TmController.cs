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
		public Response GetTmList(string gameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			View.ServerRepository.ThrowMaintenance(Log);
			var result = View.TmRepository.GetTmsByGame(gameId, Log);
			return Response.Succeed(result);
		}

	}
}
