using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Repository
{
	public class Abilities
	{
		private readonly Games Games_;
		public Abilities(Games Games)
		{
			this.Games_ = Games;
		}
		public IEnumerable<Dto.AbilitySet> GetAbilitiesByGame(string GameId, SLLog Log)
		{
			Log = Utilities.Logging.GetLog(Log);
			var res = Games_.GetGameById(GameId, Log).Generation.AbilitySets;
			return res;
		}

	}
}