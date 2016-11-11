using DexComplete.Transfer;
using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace DexComplete.Api.V1
{
	[RoutePrefix("api/v1")]
	public class UserController : ApiController
	{
		private readonly Services.ServerService ServerService_;
		private readonly Services.UserService UserService_;
		public UserController(Services.ServerService ServerService,
			Services.UserService UserService)
		{
			this.ServerService_ = ServerService;
			this.UserService_ = UserService;
		}

		[HttpPost, Route("users/login")]
		public Response PostLogin([FromBody]Models.UserModel User)
		{
			var Log = Logging.GetLog();
			ServerService_.ThrowMaintenance(Log);
			return Response.Succeed(UserService_.Login(User, Log));
		}

		[HttpPost, Route("users/register")]
		public Response PostRegister([FromBody]Models.UserModel User)
		{
			var Log = Logging.GetLog();
			ServerService_.ThrowMaintenance(Log);
			return Response.Succeed(UserService_.Register(User, Log));
		}

		[HttpPost, Route("users/logout")]
		public Response Logout()
		{
			var Log = Logging.GetLog();
			ServerService_.ThrowMaintenance(Log);
			if (!UserService_.Validate(Request.Headers.GetValues("username").First(), Request.Headers.GetValues("token").First(), Log))
				return Response.NotLoggedIn();
			return Response.Succeed(UserService_.Logout(Request.Headers.GetValues("username").First(), Request.Headers.GetValues("token").First(), Log));
		}


		[HttpPost, Route("users/validate")]
		public Response PostValidate([FromBody]Models.UserModel user)
		{
			var Log = Logging.GetLog(null);
			ServerService_.ThrowMaintenance(Log);
			var result = UserService_.Validate(user.Username, user.Token, Log);
			if (!result)
				throw new Code.ExceptionResponse("Invalid token");
			else
				return Response.Succeed("Success");
		}

		[HttpPost, Route("users/games/add")]
		public Response CreateGame([FromBody]Transfer.AddGame request)
		{
			var Log = Logging.GetLog();
			ServerService_.ThrowMaintenance(Log);
			if (!UserService_.Validate(Request.Headers.GetValues("username").First(), Request.Headers.GetValues("token").First(), Log))
				return Response.NotLoggedIn();
			if (string.IsNullOrWhiteSpace(request.SaveName))
				return Response.Error("Save name cannot be empty");
			return Response.Succeed(UserService_.CreateGame(Request.Headers.GetValues("username").First(), request, Log));
		}

		[HttpGet, Route("user/{user}/games/list")]
		public Response GetAllGames(string user)
		{
			var Log = Logging.GetLog(null);
			ServerService_.ThrowMaintenance(Log);
			return Response.Succeed(UserService_.GetAllGames(user, Log));
		}

		[HttpGet, Route("user/{user}/game/{save}")]
		public Response GetGame(string user, string save)
		{
			var Log = Logging.GetLog(null);
			ServerService_.ThrowMaintenance(Log);
			return Response.Succeed(UserService_.GetSaveData(user, save, Log));
		}

		[HttpPost, Route("user/{user}/game/{save}")]
		public Response SaveGame(string user, string save, [FromBody]Transfer.Saves data)
		{
			var Log = Logging.GetLog();
			ServerService_.ThrowMaintenance(Log);
			if (!UserService_.Validate(Request.Headers.GetValues("username").First(), Request.Headers.GetValues("token").First(), Log))
				return Response.NotLoggedIn();
			data.SaveName = save;
			return Response.Succeed(UserService_.SetSaveData(user, data, Log));
		}

		[HttpGet, Route("user/{user}/game/{save}/progress")]
		public Response GetGameProgress(string user, string save)
		{
			var Log = Logging.GetLog();
			ServerService_.ThrowMaintenance(Log);
			return Response.Succeed(UserService_.GetGameProgress(user, save, Log));
		}

		[HttpGet, Route("user/{user}/game/{save}/identifier")]
		public Response GetGameIdentifier(string user, string save)
		{
			var Log = Logging.GetLog();
			ServerService_.ThrowMaintenance(Log);
			var games = UserService_.GetAllGames(user, Log);
			var single = games.SingleOrDefault(u => u.SaveName.ToLower() == save.ToLower());
			if (single != null)
				return Response.Succeed(single.GameIdentifier);
			return Response.Error("No save found");

		}

		[HttpPost, Route("users/resetpassword")]
		public Response ResetPassword([FromBody]Models.UserModel user)
		{
			var Log = Logging.GetLog();
			ServerService_.ThrowMaintenance(Log);

			var result = UserService_.ResetPassword(user.Username, user.Password, user.Token, Log);
			if (result)
				return Response.Succeed();
			return Response.Error("Could not reset password");
		}

		[HttpGet, Route("user/{username}/resetpassword")]
		public Response RequestPasswordReset(string username)
		{
			var Log = Logging.GetLog();
			ServerService_.ThrowMaintenance(Log);
			UserService_.RequestPasswordReset(username, Log);
			return Response.Succeed();
		}
	}
}
