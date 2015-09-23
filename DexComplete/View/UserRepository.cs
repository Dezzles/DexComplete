using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.View
{
	static class UserRepository
	{
		public static bool Validate(string Username, string Token)
		{
			Data.PokedexModel ctr = new Data.PokedexModel();
			var query = ctr.Tokens.Where(e => e.Value == Token && e.User.Username == Username.ToLower() && (DateTime.Now < e.ExpiryDate) && (e.TokenType == Data.TokenType.LoginToken));
			if (query.Count() == 0)
				return false;
			else
				return true;

		}

		public static bool CreateGame(string Username, Transfer.AddGame addGame)
		{
			using (Data.PokedexModel mdl = new Data.PokedexModel())
			{
				if (!Utilities.isAlphaNumeric(addGame.SaveName))
					throw new Code.ExceptionResponse("Save name can only contain alphanumeric characters and spaces");

				Data.User user = mdl.Users.Where(e => e.Username == Username).First();
				var saves = mdl.Saves.Where(e => (e.UserId == user.Id) && (e.SaveName.ToLower() == addGame.SaveName.ToLower()));
				if (saves.Count() > 0)
					throw new Code.ExceptionResponse("Save name already exists");

				var result = mdl.Saves.Create();
				result.Code = new byte[256];
				result.AbilityData = new byte[80];
				result.BerryData = new byte[16];
				result.EggGroupData = new byte[16];
				result.DittoData = new byte[16];
				result.TMData = new byte[16];

				result.User = user;
				result.SaveName = addGame.SaveName;
				result.Game = mdl.Games.Where(e => e.Identifier == addGame.Identifier).First();
				mdl.Saves.Add(result);
				mdl.SaveChanges();
			}
			return true;
		}

		public static IEnumerable<Transfer.Saves> GetAllGames(string Username)
		{
			using (Data.PokedexModel mdl = new Data.PokedexModel())
			{
				var user = mdl.Users.SingleOrDefault(e => e.Username == Username);
				if (user == null)
					throw new Code.ExceptionResponse("Invalide username");
				List<Transfer.Saves> results = new List<Transfer.Saves>();
				foreach (var save in user.Saves)
				{
					results.Add(new Transfer.Saves()
						{
							GameIdentifier = save.Game.Identifier,
							SaveName = save.SaveName,
							GameTitle = save.Game.Title
						}
						);
				}
				return results;
			}
		}

		public static Transfer.Saves GetSaveData(string Username, string saveName)
		{
			using (Data.PokedexModel mdl = new Data.PokedexModel())
			{
				var query = mdl.Users.Where(e => e.Username == Username);
				if (query.Count() == 0)
					throw new Code.ExceptionResponse("No such user exists");
				var user = query.First();
				Transfer.Saves result = new Transfer.Saves();
				var save = user.Saves.Single(e => e.SaveName.ToLower() == saveName.ToLower());
				if (save == null)
					throw new Code.ExceptionResponse("No game found");
				result.GameIdentifier = save.Game.Identifier;
				result.GameTitle = save.Game.Title;
				result.SaveName = save.SaveName;
				result.SaveData = Convert.ToBase64String(save.Code);
				result.AbilityData = Convert.ToBase64String(save.AbilityData);
				result.BerryData = Convert.ToBase64String(save.BerryData);
				result.DittoData = Convert.ToBase64String(save.DittoData);
				result.EggGroupData = Convert.ToBase64String(save.EggGroupData);
				result.TMData = Convert.ToBase64String(save.TMData);
				return result;
			}
		}

		public static bool SetSaveData(string username, Transfer.Saves data)
		{
			using (Data.PokedexModel mdl = new Data.PokedexModel())
			{
				var user = mdl.Users.SingleOrDefault(e => e.Username == username);
				if (user == null)
					throw new Code.ExceptionResponse("Invalid user");
				var save = user.Saves.SingleOrDefault(e => e.SaveName.ToLower() == data.SaveName.ToLower());
				if (save == null)
					throw new Code.ExceptionResponse("No game found");
				var actual = save;
				if (data.SaveData != null)
					actual.Code = Convert.FromBase64String(data.SaveData);
				if (data.AbilityData != null)
					actual.AbilityData = Convert.FromBase64String(data.AbilityData);
				if (data.BerryData != null)
					actual.BerryData = Convert.FromBase64String(data.BerryData);
				if (data.DittoData != null)
					actual.DittoData = Convert.FromBase64String(data.DittoData);
				if (data.EggGroupData != null)
					actual.EggGroupData = Convert.FromBase64String(data.EggGroupData);
				if (data.TMData != null)
					actual.TMData = Convert.FromBase64String(data.TMData);
				mdl.SaveChanges();
				return true;
			}
		}

		public static Models.UserModel Register(Models.UserModel user)
		{

			user.Username = user.Username.Trim();
			user.Email = user.Email.Trim();
			if (string.IsNullOrWhiteSpace(user.Username))
			{
				throw new Code.ExceptionResponse("Username cannot be empty");
			}
			if (string.IsNullOrWhiteSpace(user.Email))
			{
				throw new Code.ExceptionResponse("Email cannot be empty");
			}
			Data.PokedexModel ctr = new Data.PokedexModel();
			var query = ctr.Users.Where(e => e.Username.ToLower() == user.Username.Trim().ToLower());
			if (query.Count() > 0)
			{
				throw new Code.ExceptionResponse("Username already in use");
			}
			query = ctr.Users.Where(e => e.Email.ToLower() == user.Email.ToLower());
			if (query.Count() > 0)
			{
				throw new Code.ExceptionResponse("Email address already in use");
			}
			string seed = Utilities.GenerateText(16);

			Data.User newuser = ctr.Users.Create();
			newuser.Email = user.Email;
			newuser.Username = user.Username.ToLower();
			newuser.Password = Utilities.GetMd5Hash(user.Password + seed);
			newuser.Salt = seed;
			ctr.Users.Add(newuser);
			ctr.SaveChanges();
			return new Models.UserModel()
			{
				Token = GetToken(newuser.Username, Data.TokenType.LoginToken)
			};
		}

		public static Models.UserModel Login(Models.UserModel User)
		{
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var query = ctr.Users.Where(e => User.Username.ToLower() == e.Username);
				if (query.Count() == 0)
				{
					throw new Code.ExceptionResponse("Incorrect username or password");
				}
				Data.User user = query.First();
				string password = Utilities.GetMd5Hash(User.Password + user.Salt);
				if (user.Password.Equals(password))
				{
					string token = GetToken(User.Username, Data.TokenType.LoginToken);
					return new Models.UserModel() { Username = user.Username, Token = token };
				}
				throw new Code.ExceptionResponse("Invalid username or password");
			}
		}

		private static string GetToken(string Username, Data.TokenType Type)
		{
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{

				DateTime expiry = DateTime.Now.AddMonths(1);
				string token = Utilities.GetMd5Hash(Utilities.GenerateText(16) + expiry.ToLongDateString());

				var query = ctr.Users.Where(e => Username.ToLower() == e.Username);
				Data.User user = query.First();
				Data.Token tkn = ctr.Tokens.Create();
				tkn.ExpiryDate = expiry;
				tkn.Value = token;
				tkn.TokenType = Type;
				user.Tokens.Add(tkn);
				ctr.SaveChanges();
				return token;
			}
		}

		public static bool Logout(string Username, string Token)
		{
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var tkn = ctr.Tokens.SingleOrDefault(e => (e.User.Username == Username) && (e.Value == Token));
				if (tkn != null)
				{
					ctr.Tokens.Remove(tkn);
					return true;
				}
				return true;
			}
		}
	}
}
