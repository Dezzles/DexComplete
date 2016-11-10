using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Services
{
	public static class EggGroupService
	{
		public static IEnumerable<Dto.IndexedItem> GetEggGroupsByGame(string gameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			return Repository.EggGroups.GetEggGroups(Log);
		}
	}
}
