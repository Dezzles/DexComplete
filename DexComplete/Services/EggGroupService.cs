using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Services
{
	public class EggGroupService
	{
		private readonly Repository.EggGroups EggGroups_;
		public EggGroupService(Repository.EggGroups EggGroups)
		{
			this.EggGroups_ = EggGroups;
		}
		public IEnumerable<Dto.IndexedItem> GetEggGroupsByGame(string gameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			return EggGroups_.GetEggGroups(Log);
		}
	}
}
