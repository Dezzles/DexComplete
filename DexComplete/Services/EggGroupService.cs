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
		private readonly SLLog Log_;
		public EggGroupService(Repository.EggGroups EggGroups, SLLog Log)
		{
			this.EggGroups_ = EggGroups;
			this.Log_ = Log;
		}
		public IEnumerable<Dto.IndexedItem> GetEggGroupsByGame(string gameId)
		{
			Log_.Info("GetEggGroupsByGame", new { gameId });
			return EggGroups_.GetEggGroups();
		}
	}
}
