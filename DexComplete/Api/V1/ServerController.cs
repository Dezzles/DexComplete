using DexComplete.Transfer;
using DexComplete.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DexComplete.Api.V1
{
	[RoutePrefix("api/v1")]
	public class ServerController : ApiController
	{
		[HttpGet, Route("server/ping")]
		public Response Ping()
		{
			var Log = Logging.GetLog(null);
			View.ServerRepository.ThrowMaintenance(Log);
			return Response.Succeed(true);
		}

		[HttpGet, Route("server/updates")]
		public Response Updates()
		{
			var Log = Logging.GetLog(null);
			View.ServerRepository.ThrowMaintenance(Log);
			var comingSoon = View.UpdatesRepository.GetComingSoon(Log);
			var updates = View.UpdatesRepository.GetRecentUpdates(Log);

			return Response.Succeed(new SiteUpdates() { ComingSoon = comingSoon, Updates = updates });
		}

	}
}
