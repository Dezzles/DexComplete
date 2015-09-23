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
	public class TmController : ApiController
	{
		[HttpGet, Route("tms/{gameId}")]
		public Response GetTmList(string gameId)
		{
			View.ServerRepository.ThrowMaintenance();
			var result = View.TmRepository.GetTmsByGame(gameId);
			return Response<IEnumerable<IdNameTransfer>>.Succeed(result);
		}

	}
}
