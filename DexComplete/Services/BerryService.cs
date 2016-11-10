using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Services
{
	public static class BerryService
	{
		public static IEnumerable<Dto.Berry> GetBerriesByGame(string GameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			var ret = Repository.Berries.GetBerries(GameId, Log);
			return ret;
		}
	}
}
