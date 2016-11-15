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
		public virtual IEnumerable<Dto.AbilitySet> GetAbilitiesByGame(string GameId)
		{
			var res = Games_.GetGameById(GameId).Generation.AbilitySets;
			return res;
		}

	}
}