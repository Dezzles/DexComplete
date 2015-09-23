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

	}
}
