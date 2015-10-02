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
	public class EggGroupsController : ApiController
	{
		[HttpGet, Route("eggGroups/{gameId}")]
		public Response GetEggGroups(string gameId)
		{
			View.ServerRepository.ThrowMaintenance();
			var result = View.EggGroupRepository.GetEggGroupsByGame(gameId);
			return Response.Succeed(result);
		}

	}
}
