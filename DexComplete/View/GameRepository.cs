using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.View
{
	static class GameRepository
	{
		public static IEnumerable<Models.GameModel> GetGames(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				List<Models.GameModel> result = new List<Models.GameModel>();
				foreach(var game in ctr.Games)
				{
					result.Add(new Models.GameModel()
						{
							Title = game.Title,
							Identifier = game.GameId
						});
				}
				return result;
			}
		}

		public static Transfer.GameTools GetGameTools(string gameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			List<Transfer.GameToolItems> Collections = new List<Transfer.GameToolItems>();
			List<Transfer.GameToolItems> Tools = new List<Transfer.GameToolItems>();
			Transfer.GameTools ret = new Transfer.GameTools();
			ret.Collections = Collections;
			ret.Tools = Tools;
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var game = ctr.Games.SingleOrDefault(e => e.GameId == gameId);
				if (game == null)
					throw new Code.ExceptionResponse("Invalid game");
				foreach (var u in game.Collections)
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
			}
			ret.Pokedex = PokedexRepository.GetPokedexesByGame(gameId, Log);
			return ret;
			
		}

		public static Models.GameModel GetGameByName(string gameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var query = ctr.Games.Where(e => e.GameId.ToLower() == gameId.ToLower());
				if (query.Count() == 0)
					throw new Code.ExceptionResponse("Invalid game");
				return new Models.GameModel()
				{
					Title = query.First().Title,
					Identifier = query.First().GameId
				};
			}
		}
	}
}
