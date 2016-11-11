using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Services
{
	public class GameService
	{
		private readonly Repository.Games Games_;
		private readonly PokedexService PokedexService_;
		public GameService(Repository.Games Games, PokedexService PokedexService)
		{
			this.Games_ = Games;
			this.PokedexService_ = PokedexService;
		}
		public IEnumerable<Models.GameModel> GetGames(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			var games = Games_.GetGames(Log);
			List<Models.GameModel> result = new List<Models.GameModel>();
			foreach(var game in games)
			{
				result.Add(new Models.GameModel()
					{
						Title = game.Title,
						Identifier = game.GameId
					});
			}
			return result;
		}

		public Transfer.GameTools GetGameTools(string gameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			List<Transfer.GameToolItems> Collections = new List<Transfer.GameToolItems>();
			List<Transfer.GameToolItems> Tools = new List<Transfer.GameToolItems>();
			Transfer.GameTools ret = new Transfer.GameTools();
			ret.Collections = Collections;
			ret.Tools = Tools;
			var collections = Games_.GetCollectionsByGame(gameId, Log);
			if (collections == null)
				throw new Code.ExceptionResponse("Invalid game");
			foreach (var u in collections)
			{
				Transfer.GameToolItems item = new Transfer.GameToolItems()
				{
					Identifier = u.CollectionId,
					Title = u.Title
				};
				if ( u.Type == Data.CollectionType.Collection)
				{
					Collections.Add(item);
				}
				else if (u.Type == Data.CollectionType.Tool)
				{
					Tools.Add(item);
				}
			}
			ret.Pokedex = PokedexService_.GetPokedexesByGame(gameId, Log);
			return ret;
			
		}

		public Models.GameModel GetGameByName(string gameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			var query = Games_.GetGameById(gameId, Log);
			if (query == null)
				throw new Code.ExceptionResponse("Invalid game");
			return new Models.GameModel()
			{
				Title = query.Title,
				Identifier = query.GameId
			};
		}
	}
}
