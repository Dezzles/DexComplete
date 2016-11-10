using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Services
{
	public class AbilityService
	{
		public static IEnumerable<Dto.AbilitySet> GetAbilitiesByGame(string gameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			Log.Info("GetAbilitiesByGame", new { gameId = gameId });
			return Repository.Abilities.GetAbilitiesByGame(gameId, Log);
		}
	}
}
