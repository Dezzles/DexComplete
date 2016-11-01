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
	public class AbilityController : ApiController
	{
		[HttpGet, Route("ability/{gameId}")]
		public Response GetAbilitiesList(string gameId)
		{
			var log = Utilities.Logging.GetLog();
			View.ServerRepository.ThrowMaintenance(log);
			var result = View.AbilityRepository.GetAbilitiesByGame(gameId, log);
			return Response.Succeed(result);
		}

	}
}
