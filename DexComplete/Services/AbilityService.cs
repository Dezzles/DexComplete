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
		private readonly SLLog Log_;
		public AbilityService(Repository.Abilities Abilities, SLLog Log)
		{
			Abilities_ = Abilities;
			Log_ = Log;
		}
		public IEnumerable<Dto.AbilitySet> GetAbilitiesByGame(string gameId)
		{
			
			Log_.Info("GetAbilitiesByGame", new { gameId = gameId });
			var result = Abilities_.GetAbilitiesByGame(gameId);
			return result;
		}
	}
}
