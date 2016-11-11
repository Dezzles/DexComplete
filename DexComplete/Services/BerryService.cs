using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Services
{
	public class BerryService
	{
		private readonly Repository.Berries Berries_;
		public BerryService(Repository.Berries Berries)
		{
			this.Berries_ = Berries;
		}
		public IEnumerable<Dto.Berry> GetBerriesByGame(string GameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			var ret = Berries_.GetBerries(GameId, Log);
			return ret;
		}
	}
}
