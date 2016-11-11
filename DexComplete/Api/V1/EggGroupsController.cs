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
	public class EggGroupsController : ApiController
	{
		private readonly Services.EggGroupService Service_;
		private readonly Services.ServerService ServerService_;

		public EggGroupsController(Services.ServerService ServerService, Services.EggGroupService Service)
		{
			Service_ = Service;
			this.ServerService_ = ServerService;
		}

		[HttpGet, Route("eggGroups/{gameId}")]
		public Response GetEggGroups(string gameId)
		{
			var Log = Logging.GetLog();
			ServerService_.ThrowMaintenance(Log);
			var result = Service_.GetEggGroupsByGame(gameId, Log);
			return Response.Succeed(result);
		}

	}
}
