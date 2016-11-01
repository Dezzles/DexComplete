using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.View
{
	public static class TmRepository
	{
		public static IEnumerable<Transfer.IdNameTransfer> GetTmsByGame(string GameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			List<Transfer.IdNameTransfer> ret = new List<Transfer.IdNameTransfer>();

			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var game = ctr.Games.SingleOrDefault(e => e.GameId == GameId);
				if (game == null) 
					throw new Code.ExceptionResponse("INVALID_GAME");

				var tmList = ctr.TMs.Where(u => u.TmSetId == game.TMSetId).OrderBy(e => e.Index);
				/*if (game == null || game.TMSet == null)
					throw new Code.ExceptionResponse("Invalid game");
				var tmList = game.TMSet.TMs.OrderBy(e => e.Index);/**/
				foreach (var tm in tmList)
				{
					ret.Add(new Transfer.IdNameTransfer()
						{
							 Index = tm.Index,
							 Name = tm.Move.Name
						});
				}/**/
			}
			return ret;
		}
	}
}
