using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Repository
{
	public class Abilities
	{
		public static IEnumerable<Dto.AbilitySet> GetAbilitiesByGame(string GameId, SLLog Log)
		{
			Log = Utilities.Logging.GetLog(Log);
			var res = Games.GetGameById(GameId, Log).Generation.AbilitySets;
			return res;
		}

	}
}