using DexComplete.Transfer;
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
			View.ServerRepository.ThrowMaintenance();
			return Response.Succeed(true);
		}

		[HttpGet, Route("server/updates")]
		public Response Updates()
		{
			View.ServerRepository.ThrowMaintenance();
			var comingSoon = View.UpdatesRepository.GetComingSoon();
			var updates = View.UpdatesRepository.GetRecentUpdates();

			return Response.Succeed(new SiteUpdates() { ComingSoon = comingSoon, Updates = updates });
		}

	}
}
