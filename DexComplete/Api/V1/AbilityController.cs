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
		private readonly Services.AbilityService AbilityService_;
		private readonly Services.ServerService ServerService_;
		public AbilityController(Services.ServerService ServerService,
			Services.AbilityService AbilityService)
		{
			this.ServerService_ = ServerService;
			this.AbilityService_ = AbilityService;
		}

		[HttpGet, Route("ability/{gameId}")]
		public Response GetAbilitiesList(string gameId)
		{
			ServerService_.ThrowMaintenance();
			var result = AbilityService_.GetAbilitiesByGame(gameId);
			return Response.Succeed(result, false);
		}

	}
}
