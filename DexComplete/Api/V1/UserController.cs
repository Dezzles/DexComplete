using DexComplete.Transfer;
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
			View.ServerRepository.ThrowMaintenance();
			return Response.Succeed(View.UserRepository.Login(User));
		}

		[HttpPost, Route("users/register")]
		public Response PostRegister([FromBody]Models.UserModel User)
		{
			View.ServerRepository.ThrowMaintenance();
			return Response.Succeed(View.UserRepository.Register(User));
		}

		[HttpPost, Route("users/logout")]
		public Response Logout()
		{
			View.ServerRepository.ThrowMaintenance();
			if (!View.UserRepository.Validate(Request.Headers.GetValues("username").First(), Request.Headers.GetValues("token").First()))
				return Response.NotLoggedIn();
			return Response.Succeed(View.UserRepository.Logout(Request.Headers.GetValues("username").First(), Request.Headers.GetValues("token").First()));
		}


		[HttpPost, Route("users/validate")]
		public Response PostValidate([FromBody]Models.UserModel user)
		{
			View.ServerRepository.ThrowMaintenance();
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
			View.ServerRepository.ThrowMaintenance();
			if (!View.UserRepository.Validate(Request.Headers.GetValues("username").First(), Request.Headers.GetValues("token").First()))
				return Response.NotLoggedIn();
			if (string.IsNullOrWhiteSpace(request.SaveName))
				return Response.Error("Save name cannot be empty");
			return Response.Succeed(View.UserRepository.CreateGame(Request.Headers.GetValues("username").First(), request));
		}

		[HttpGet, Route("user/{user}/games/list")]
		public Response GetAllGames(string user)
		{
			View.ServerRepository.ThrowMaintenance();
			return Response.Succeed(View.UserRepository.GetAllGames(user));
		}

		[HttpGet, Route("user/{user}/game/{save}")]
		public Response GetGame(string user, string save)
		{
			View.ServerRepository.ThrowMaintenance();
			return Response.Succeed(View.UserRepository.GetSaveData(user, save));
		}

		[HttpPost, Route("user/{user}/game/{save}")]
		public Response SaveGame(string user, string save, [FromBody]Transfer.Saves data)
		{
			View.ServerRepository.ThrowMaintenance();
			if (!View.UserRepository.Validate(Request.Headers.GetValues("username").First(), Request.Headers.GetValues("token").First()))
				return Response.NotLoggedIn();
			data.SaveName = save;
			return Response.Succeed(View.UserRepository.SetSaveData(user, data));
		}

		[HttpGet, Route("user/{user}/game/{save}/progress")]
		public Response GetGameProgress(string user, string save)
		{
			View.ServerRepository.ThrowMaintenance();
			return Response.Succeed(View.UserRepository.GetGameProgress(user, save));
		}

		[HttpGet, Route("user/{user}/game/{save}/identifier")]
		public Response GetGameIdentifier(string user, string save)
		{
			View.ServerRepository.ThrowMaintenance();
			var games = View.UserRepository.GetAllGames(user);
			var single = games.SingleOrDefault(u => u.SaveName.ToLower() == save.ToLower());
			if (single != null)
				return Response.Succeed(single.GameIdentifier);
			return Response.Error("No save found");

		}

		[HttpPost, Route("users/resetpassword")]
		public Response ResetPassword([FromBody]Models.UserModel user)
		{
			View.ServerRepository.ThrowMaintenance();

			var result = View.UserRepository.ResetPassword(user.Username, user.Password, user.Token);
			if (result)
				return Response.Succeed();
			return Response.Error("Could not reset password");
		}

		[HttpGet, Route("user/{username}/resetpassword")]
		public Response RequestPasswordReset(string username)
		{
			View.ServerRepository.ThrowMaintenance();
			View.UserRepository.RequestPasswordReset(username);
			return Response.Succeed();
		}
	}
}
