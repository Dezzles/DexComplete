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
	public class ServerController : ApiController
	{
		private readonly Services.UpdatesService UpdatesService_;
		private readonly Services.ServerService ServerService_;
		public ServerController(Services.ServerService ServerService,
			Services.UpdatesService UpdatesService)
		{
			this.ServerService_ = ServerService;
			this.UpdatesService_ = UpdatesService;
		}

		[HttpGet, Route("server/ping")]
		public Response Ping()
		{
			var Log = Logging.GetLog();
			ServerService_.ThrowMaintenance(Log);
			return Response.Succeed(true);
		}

		[HttpGet, Route("server/updates")]
		public Response Updates()
		{
			var Log = Logging.GetLog();
			ServerService_.ThrowMaintenance(Log);
			var comingSoon = UpdatesService_.GetComingSoon(Log);
			var updates = UpdatesService_.GetRecentUpdates(Log);

			return Response.Succeed(new SiteUpdates() { ComingSoon = comingSoon, Updates = updates });
		}

	}
}
