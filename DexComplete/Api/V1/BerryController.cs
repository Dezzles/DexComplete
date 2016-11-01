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
		[HttpGet, Route("berries/{gameId}")]
		public Response GetBerryList(string gameId, SLLog Log = null)
		{
			Log = Logging.GetLog(Log);
			View.ServerRepository.ThrowMaintenance(Log);
			var result = View.BerryRepository.GetBerriesByGame(gameId, Log);
			return Response.Succeed(result);
		}

	}
}
