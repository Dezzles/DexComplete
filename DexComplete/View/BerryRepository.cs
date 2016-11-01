using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.View
{
	public static class BerryRepository
	{
		public static IEnumerable<Transfer.IdNameTransfer> GetBerriesByGame(string GameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			List<Transfer.IdNameTransfer> ret = new List<Transfer.IdNameTransfer>();
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var generation = ctr.Generations.SingleOrDefault(e => e.Games.Any(u => u.GameId == GameId));
				if (generation == null)
					throw new Code.ExceptionResponse("Invalid game");
				if (generation.Berries.Count == 0)
					throw new Code.Exception404();
				var berries = generation.Berries.OrderBy(e => e.Index);
				foreach (var b in berries)
				{
					ret.Add(new Transfer.IdNameTransfer()
						{
							Id = b.BerryId,
							Name = b.Berry.Name,
							Index = b.Berry.Index
						});
				}
			}
			return ret;
		}
	}
}
