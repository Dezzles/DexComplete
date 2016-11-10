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
		[HttpGet, Route("eggGroups/{gameId}")]
		public Response GetEggGroups(string gameId)
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);
			var result = Services.EggGroupService.GetEggGroupsByGame(gameId, Log);
			return Response.Succeed(result);
		}

	}
}
