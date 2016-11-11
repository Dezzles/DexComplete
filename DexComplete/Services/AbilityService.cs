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
		private readonly Repository.Abilities Abilities_;
		public AbilityService(Repository.Abilities Abilities)
		{
			Abilities_ = Abilities;
		}
		public IEnumerable<Dto.AbilitySet> GetAbilitiesByGame(string gameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			Log.Info("GetAbilitiesByGame", new { gameId = gameId });
			return Abilities_.GetAbilitiesByGame(gameId, Log);
		}
	}
}
