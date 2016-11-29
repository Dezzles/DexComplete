﻿using DexComplete.Transfer;
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
		private readonly SLLog Log_;
		private readonly Cache Cache_;
		public EggGroupsController(Services.ServerService ServerService, Services.EggGroupService Service,
			SLLog Log, Cache Cache )
		{
			Log_ = Log;
			Service_ = Service;
			Cache_ = Cache;
			this.ServerService_ = ServerService;
		}

		[HttpGet, Route("eggGroups/{gameId}")]
		public Response GetEggGroups(string gameId)
		{
			Log_.Info("GetEggGroups", new { gameId });
			ServerService_.ThrowMaintenance();
			var result = Cache_.GetResult(Service_.GetEggGroupsByGame, gameId);
			return Response.Succeed(result, false);
		}

	}
}
