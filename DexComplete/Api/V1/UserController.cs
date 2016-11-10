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
		[HttpPost, Route("users/login")]
		public Response PostLogin([FromBody]Models.UserModel User)
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);
			return Response.Succeed(Services.UserService.Login(User, Log));
		}

		[HttpPost, Route("users/register")]
		public Response PostRegister([FromBody]Models.UserModel User)
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);
			return Response.Succeed(Services.UserService.Register(User, Log));
		}

		[HttpPost, Route("users/logout")]
		public Response Logout()
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);
			if (!Services.UserService.Validate(Request.Headers.GetValues("username").First(), Request.Headers.GetValues("token").First(), Log))
				return Response.NotLoggedIn();
			return Response.Succeed(Services.UserService.Logout(Request.Headers.GetValues("username").First(), Request.Headers.GetValues("token").First(), Log));
		}


		[HttpPost, Route("users/validate")]
		public Response PostValidate([FromBody]Models.UserModel user)
		{
			var Log = Logging.GetLog(null);
			Services.ServerService.ThrowMaintenance(Log);
			Data.PokedexModel ctr = new Data.PokedexModel();
			var query = ctr.Tokens.Where(e => e.Value == user.Token && e.User.Username.ToLower() == user.Username.ToLower() && (DateTime.Now < e.ExpiryDate));
			if (query.Count() == 0)
				throw new Code.ExceptionResponse("Invalid token");
			else
				return Response.Succeed("Success");
		}

		[HttpPost, Route("users/games/add")]
		public Response CreateGame([FromBody]Transfer.AddGame request)
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);
			if (!Services.UserService.Validate(Request.Headers.GetValues("username").First(), Request.Headers.GetValues("token").First(), Log))
				return Response.NotLoggedIn();
			if (string.IsNullOrWhiteSpace(request.SaveName))
				return Response.Error("Save name cannot be empty");
			return Response.Succeed(Services.UserService.CreateGame(Request.Headers.GetValues("username").First(), request, Log));
		}

		[HttpGet, Route("user/{user}/games/list")]
		public Response GetAllGames(string user)
		{
			var Log = Logging.GetLog(null);
			Services.ServerService.ThrowMaintenance(Log);
			return Response.Succeed(Services.UserService.GetAllGames(user, Log));
		}

		[HttpGet, Route("user/{user}/game/{save}")]
		public Response GetGame(string user, string save)
		{
			var Log = Logging.GetLog(null);
			Services.ServerService.ThrowMaintenance(Log);
			return Response.Succeed(Services.UserService.GetSaveData(user, save, Log));
		}

		[HttpPost, Route("user/{user}/game/{save}")]
		public Response SaveGame(string user, string save, [FromBody]Transfer.Saves data)
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);
			if (!Services.UserService.Validate(Request.Headers.GetValues("username").First(), Request.Headers.GetValues("token").First(), Log))
				return Response.NotLoggedIn();
			data.SaveName = save;
			return Response.Succeed(Services.UserService.SetSaveData(user, data, Log));
		}

		[HttpGet, Route("user/{user}/game/{save}/progress")]
		public Response GetGameProgress(string user, string save)
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);
			return Response.Succeed(Services.UserService.GetGameProgress(user, save, Log));
		}

		[HttpGet, Route("user/{user}/game/{save}/identifier")]
		public Response GetGameIdentifier(string user, string save)
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);
			var games = Services.UserService.GetAllGames(user, Log);
			var single = games.SingleOrDefault(u => u.SaveName.ToLower() == save.ToLower());
			if (single != null)
				return Response.Succeed(single.GameIdentifier);
			return Response.Error("No save found");

		}

		[HttpPost, Route("users/resetpassword")]
		public Response ResetPassword([FromBody]Models.UserModel user)
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);

			var result = Services.UserService.ResetPassword(user.Username, user.Password, user.Token, Log);
			if (result)
				return Response.Succeed();
			return Response.Error("Could not reset password");
		}

		[HttpGet, Route("user/{username}/resetpassword")]
		public Response RequestPasswordReset(string username)
		{
			var Log = Logging.GetLog();
			Services.ServerService.ThrowMaintenance(Log);
			Services.UserService.RequestPasswordReset(username, Log);
			return Response.Succeed();
		}
	}
}
