﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using DexComplete.Utilities;
using SharpLogging;

namespace DexComplete.Services
{
	public class UserService
	{
		private readonly Data.PokedexModel Model_;
		private readonly Services.ServerService ServerService_;
		private readonly GameService GameService_;
		private readonly PokedexService PokedexService_;
		private readonly AbilityService AbilityService_;
		private readonly EggGroupService EggGroupService_;
		private readonly BerryService BerryService_;
		private readonly TmService TmService_;
		private readonly EmailService EmailService_;
		public UserService(Data.PokedexModel Model, Services.ServerService ServerService,
			GameService GameService, PokedexService PokedexService,
			AbilityService AbilityService, EggGroupService EggGroupService,
			BerryService BerryService, TmService TmService, EmailService EmailService)
		{
			this.Model_ = Model;
			this.ServerService_ = ServerService;
			this.GameService_ = GameService;
			this.PokedexService_ = PokedexService;
			this.AbilityService_ = AbilityService;
			this.EggGroupService_ = EggGroupService;
			this.BerryService_ = BerryService;
			this.TmService_ = TmService;
			this.EmailService_ = EmailService;
		}
		public bool Validate(string Username, string Token)
		{
			
			var query = Model_.Tokens.Where(e => e.Value == Token && e.User.Username == Username.ToLower() && (DateTime.Now < e.ExpiryDate) && (e.TokenType == Data.TokenType.LoginToken));
			if (query.Count() == 0)
				return false;
			else
				return true;

		}

		public bool CreateGame(string Username, Transfer.AddGame addGame)
		{
			
			if (!Utilities.Encryption.isAlphaNumeric(addGame.SaveName))
				throw new Code.ExceptionResponse("Save name can only contain alphanumeric characters and spaces");

			Data.User user = Model_.Users.Where(e => e.Username == Username).First();
			var saves = Model_.Saves.Where(e => (e.UserId == user.UserId) && (e.SaveName.ToLower() == addGame.SaveName.ToLower()));
			if (saves.Count() > 0)
				throw new Code.ExceptionResponse("Save name already exists");

			var result = Model_.Saves.Create();
			result.Code = new byte[256];
			result.AbilityData = new byte[256];
			result.BerryData = new byte[16];
			result.EggGroupData = new byte[16];
			result.DittoData = new byte[16];
			result.TMData = new byte[16];

			result.User = user;
			result.SaveName = addGame.SaveName;
			result.Game = Model_.Games.Where(e => e.GameId == addGame.Identifier).First();
			Model_.Saves.Add(result);
			Model_.SaveChanges();
			return true;
		}

		public IEnumerable<Transfer.Saves> GetAllGames(string Username)
		{
			
			var user = Model_.Users.SingleOrDefault(e => e.Username == Username);
			if (user == null)
				throw new Code.ExceptionResponse("Invalide username");
			List<Transfer.Saves> results = new List<Transfer.Saves>();
			foreach (var save in user.Saves)
			{
				results.Add(new Transfer.Saves()
					{
						GameIdentifier = save.Game.GameId,
						SaveName = save.SaveName,
						GameTitle = save.Game.Title
					}
					);
			}
			return results;
		}

		public Transfer.Saves GetSaveData(string Username, string saveName)
		{
			
			var query = Model_.Users.Where(e => e.Username == Username);
			if (query.Count() == 0)
				throw new Code.ExceptionResponse("No such user exists");
			var user = query.First();
			Transfer.Saves result = new Transfer.Saves();
			var save = user.Saves.SingleOrDefault(e => e.SaveName.ToLower() == saveName.ToLower());
			if (save == null)
				throw new Code.ExceptionResponse("No game found");
			result.GameIdentifier = save.Game.GameId;
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

		public bool SetSaveData(string username, Transfer.Saves data)
		{
			
			var user = Model_.Users.SingleOrDefault(e => e.Username == username);
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
			Model_.SaveChanges();
			return true;
		}

		public Models.UserModel Register(Models.UserModel user)
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
			var query = Model_.Users.Where(e => e.Username.ToLower() == user.Username.Trim().ToLower());
			if (query.Count() > 0)
			{
				throw new Code.ExceptionResponse("Username already in use");
			}
			query = Model_.Users.Where(e => e.Email.ToLower() == user.Email.ToLower());
			if (query.Count() > 0)
			{
				throw new Code.ExceptionResponse("Email address already in use");
			}

			Data.User newuser = Model_.Users.Create();
			newuser.Email = user.Email;
			newuser.Username = user.Username.ToLower();
			var seed = BCrypt.Net.BCrypt.GenerateSalt(16);
			newuser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, seed);
			newuser.Salt = seed;
			Model_.Users.Add(newuser);
			Model_.SaveChanges();
			return new Models.UserModel()
			{
				Token = GetToken(newuser.Username, Data.TokenType.LoginToken)
			};
		}

		public Models.UserModel Login(Models.UserModel User)
		{
			
			var query = Model_.Users.Where(e => User.Username.ToLower() == e.Username);
			if (query.Count() == 0)
			{
				throw new Code.ExceptionResponse("Incorrect username or password");
			}
			Data.User user = query.First();
			var success = BCrypt.Net.BCrypt.Verify(User.Password, user.Password);
			if (success)
			{
				string token = GetToken(User.Username, Data.TokenType.LoginToken);
				return new Models.UserModel() { Username = user.Username, Token = token };
			}
			throw new Code.ExceptionResponse("Invalid username or password");
		}

		private string GetToken(string Username, Data.TokenType Type)
		{
			

			DateTime expiry = DateTime.Now.AddMonths(1);
			string token = Utilities.Encryption.GetMd5Hash(Utilities.Encryption.GenerateText(16) + expiry.ToLongDateString());

			var query = Model_.Users.Where(e => Username.ToLower() == e.Username);
			Data.User user = query.First();
			Data.Token tkn = Model_.Tokens.Create();
			tkn.ExpiryDate = expiry;
			tkn.Value = token;
			tkn.TokenType = Type;
			user.Tokens.Add(tkn);
			Model_.SaveChanges();
			return token;
		}

		public bool Logout(string Username, string Token)
		{
			
			var tkn = Model_.Tokens.SingleOrDefault(e => (e.User.Username == Username) && (e.Value == Token));
			if (tkn != null)
			{
				Model_.Tokens.Remove(tkn);
				return true;
			}
			return true;
		}

		public string GetResetContents(string Username, string Token)
		{
			
			string contents = Utilities.Templates.GetTemplate("passwordReset.html");
			contents = contents.Replace("{URL}", ServerService_.GetServerAddress());
			contents = contents.Replace("{TOKEN}", Token);

			return contents;
		}

		public void RequestPasswordReset(string username)
		{
			
			var theUser = Model_.Users.FirstOrDefault(u => u.Username == username.ToLower());
			if (theUser == null)
				return;
			var token = GetToken(username, Data.TokenType.PasswordReset);
			string Contents = GetResetContents(theUser.Username, token);
			EmailService_.SendEmail(theUser.Email, "DexComplete Password Reset", Contents);
		}

		public bool ResetPassword(string user, string password, string token)
		{
			

			var theUser = Model_.Users.FirstOrDefault(u => u.Username == user.ToLower());
			if (!theUser.Tokens.Any(v => v.TokenType == Data.TokenType.PasswordReset && v.Value == token))
				return false;
			if (theUser == null)
				return false;
			var seed = BCrypt.Net.BCrypt.GenerateSalt(16);
			theUser.Password = BCrypt.Net.BCrypt.HashPassword(password, seed);
			theUser.Salt = seed;
			var allTokens = Model_.Tokens.Where(u => u.User.UserId == theUser.UserId);
			Model_.Tokens.RemoveRange(allTokens);
			Model_.SaveChanges();
			return true;
		}

		public Transfer.GameProgress GetGameProgress(string user, string save)
		{
			var progress = new Transfer.GameProgress();
			
			progress.Pokedexes = new List<Transfer.ItemProgress>();
			progress.Collections = new List<Transfer.ItemProgress>();
			var saveData = Model_.Saves.SingleOrDefault(u => (u.User.Username.ToLower() == user.ToLower()) && (u.SaveName.ToLower() == save.ToLower()));
			if (saveData == null)
			{
				throw new Code.Exception404();
			}
			var gameTools = GameService_.GetGameTools(saveData.Game.GameId);
			var dexes = PokedexService_.GetPokedexesByGame(saveData.Game.GameId);
			int[] dex = ConvertData(saveData.Code, 2);
			int[] tm = ConvertData(saveData.TMData);
			int[] abilities = ConvertData(saveData.AbilityData, 2);
			int[] dittos = ConvertData(saveData.DittoData);
			int[] berries = ConvertData(saveData.BerryData);
			int[] eggGroup = ConvertData(saveData.EggGroupData);

			foreach (var d in dexes)
			{
				var item = new Transfer.ItemProgress();
				var entries = PokedexService_.GetPokedex(d.PokedexId);
				item.Title = entries.Title;
				item.Identifier = d.PokedexId;
				item.Total = entries.Pokemon.Count();
				foreach (var entry in entries.Pokemon)
				{
					if (dex[entry.Index] > 0)
						item.Completion++;
				}
				progress.Pokedexes.Add(item);

			}
			if (gameTools.Collections.Any(u => u.Identifier == "abilities"))
			{
				var item = new Transfer.ItemProgress();
				var entries = AbilityService_.GetAbilitiesByGame(saveData.Game.GameId);
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
				var entries = EggGroupService_.GetEggGroupsByGame(saveData.Game.GameId);
				item.Title = "Egg Groups";
				item.Total = entries.Count();
				item.Identifier = "egggroups";
				foreach (var entry in entries)
				{
					if (eggGroup[entry.Index] > 0)
						item.Completion++;
				}/**/
				progress.Collections.Add(item);
			}

			if (gameTools.Collections.Any(u => u.Identifier == "berries"))
			{
				var item = new Transfer.ItemProgress();
				var entries = BerryService_.GetBerriesByGame(saveData.Game.GameId);
				item.Title = "Berries";
				item.Identifier = "berries";
				item.Total = entries.Count();
				foreach (var entry in entries)
				{
					if (berries[entry.Index] > 0)
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
				var entries = TmService_.GetTmsByGame(saveData.Game.GameId);
				item.Title = "Technical Machines";
				item.Identifier = "tms";
				item.Total = entries.Count();
				foreach (var entry in entries)
				{
					if (tm[entry.Index] > 0)
						item.Completion++;
				}
				progress.Collections.Add(item);
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
