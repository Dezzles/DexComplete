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
				if (!Utilities.Encryption.isAlphaNumeric(addGame.SaveName))
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

			Data.User newuser = ctr.Users.Create();
			newuser.Email = user.Email;
			newuser.Username = user.Username.ToLower();
			string seed = Utilities.Encryption.GenerateText(16);
			newuser.Password = Utilities.Encryption.GetMd5Hash(user.Password + seed);
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
				string password = Utilities.Encryption.GetMd5Hash(User.Password + user.Salt);
				string brokenPassword = Utilities.Encryption.GetMd5Hash(user.Salt);
				if (user.Password.Equals(brokenPassword))
				{
					throw new Code.ExceptionResponse("Password reset required");
				}
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
				string token = Utilities.Encryption.GetMd5Hash(Utilities.Encryption.GenerateText(16) + expiry.ToLongDateString());

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

		public static string GetResetContents(string Username, string Token)
		{
			string contents = Utilities.Templates.GetTemplate("passwordReset.html");
			contents = contents.Replace("{URL}", View.ServerRepository.GetServerAddress());
			contents = contents.Replace("{TOKEN}", Token);

			return contents;
		}

		public static void RequestPasswordReset(string username)
		{
			using (Data.PokedexModel model = new Data.PokedexModel())
			{
				{
					var theUser = model.Users.FirstOrDefault(u => u.Username == username.ToLower());
					if (theUser == null)
						return;
					var token = GetToken(username, Data.TokenType.PasswordReset);
					string Contents = GetResetContents(theUser.Username, token);
					Utilities.Email.SendEmail(theUser.Email, "DexComplete Password Reset", Contents);
				}
				
				{

				}

			}
		}

		public static bool ResetPassword(string user, string password, string token)
		{
			using (Data.PokedexModel model = new Data.PokedexModel())
			{
				var theUser = model.Users.FirstOrDefault(u => u.Username == user.ToLower());
				if (!theUser.Tokens.Any(v => v.TokenType == Data.TokenType.PasswordReset && v.Value == token))
					return false;
				if (theUser == null)
					return false;
				string seed = Utilities.Encryption.GenerateText(16);
				theUser.Password = Utilities.Encryption.GetMd5Hash(password + seed);
				theUser.Salt = seed;
				var allTokens = model.Tokens.Where(u => u.User.Id == theUser.Id);
				model.Tokens.RemoveRange(allTokens);
				model.SaveChanges();
				return true;
			}
		}

		public static Transfer.GameProgress GetGameProgress(string user, string save)
		{
			var progress = new Transfer.GameProgress();
			progress.Pokedexes = new List<Transfer.ItemProgress>();
			progress.Collections = new List<Transfer.ItemProgress>();
			using (Data.PokedexModel model = new Data.PokedexModel())
			{
				var saveData = model.Saves.Single(u => (u.User.Username.ToLower() == user.ToLower()) && (u.SaveName.ToLower() == save.ToLower()));
				var gameTools = GameRepository.GetGameTools(saveData.Game.Identifier);
				var dexes = PokedexRepository.GetPokedexesByGame(saveData.Game.Identifier);
				int[] dex = ConvertData(saveData.Code, 2);
				int[] tm = ConvertData(saveData.TMData);
				int[] abilities = ConvertData(saveData.AbilityData, 2);
				int[] dittos = ConvertData(saveData.DittoData);
				int[] berries = ConvertData(saveData.BerryData);
				int[] eggGroup = ConvertData(saveData.EggGroupData);

				foreach (var d in dexes)
				{
					var item = new Transfer.ItemProgress();
					var entries = PokedexRepository.GetPokedex(d.Id);
					item.Title = entries.Title;
					item.Identifier = d.Identifier;
					item.Total = entries.Pokemon.Count();
					foreach (var entry in entries.Pokemon)
					{
						if (dex[entry.Id] > 0)
							item.Completion++;
					}
					progress.Pokedexes.Add(item);

				}
				if (gameTools.Collections.Any(u => u.Identifier == "abilities"))
				{
					var item = new Transfer.ItemProgress();
					var entries = AbilityRepository.GetAbilitiesByGame(saveData.Game.Identifier);
					item.Title = "Hidden Abilities";
					item.Total = entries.Count();
					item.Identifier = "abilities";
					foreach (var entry in entries)
					{
						if (abilities[entry.AbilityId] > 0)
							item.Completion++;
					}
					progress.Collections.Add(item);
				}

				if (gameTools.Collections.Any(u => u.Identifier == "eggGroups"))
				{
					var item = new Transfer.ItemProgress();
					var entries = EggGroupRepository.GetEggGroupsByGame(saveData.Game.Identifier);
					item.Title = "Egg Groups";
					item.Total = entries.Count();
					item.Identifier = "egggroups";
					foreach (var entry in entries)
					{
						if (eggGroup[entry.Id] > 0)
							item.Completion++;
					}
					progress.Collections.Add(item);
				}

				if (gameTools.Collections.Any(u => u.Identifier == "berries"))
				{
					var item = new Transfer.ItemProgress();
					var entries = BerryRepository.GetBerriesByGame(saveData.Game.Identifier);
					item.Title = "Berries";
					item.Identifier = "berries";
					item.Total = entries.Count();
					foreach (var entry in entries)
					{
						if (berries[entry.Id] > 0)
							item.Completion++;
					}
					progress.Collections.Add(item);
				}

				if (gameTools.Collections.Any(u => u.Identifier == "dittos"))
				{
					var item = new Transfer.ItemProgress();
					item.Title = "Ditto Natures";
					item.Identifier = "dittos";
					item.Total = 25;
					for (int i = 0; i < 25; ++i )
					{
						if (dittos[i] > 0)
							item.Completion++;
					}
					progress.Collections.Add(item);
				}

				if (gameTools.Collections.Any(u => u.Identifier == "tms"))
				{
					var item = new Transfer.ItemProgress();
					var entries = TmRepository.GetTmsByGame(saveData.Game.Identifier);
					item.Title = "Technical Machines";
					item.Identifier = "tms";
					item.Total = entries.Count();
					foreach (var entry in entries)
					{
						if (tm[entry.Id] > 0)
							item.Completion++;
					}
					progress.Collections.Add(item);
				}
			}
			return progress;
		}

		public static int[] ConvertData(byte[] data, int bitCount = 1)
		{
			int mod = 0;
			int pow = 1;
			for (int i = 0; i < bitCount; ++i)
			{
				mod += pow;
				pow *= 2;
			}
			int Index = 0;
			int[] ret = new int[data.Length * 8 / bitCount];
			foreach (var u in data)
			{
				int p = 0;
				int c = u;
				while (p < 8)
				{
					ret[Index] = c & mod;
					c = c >> bitCount;
					p += bitCount;
					++Index;
				}
			}
			return ret;
		}
	}
}
