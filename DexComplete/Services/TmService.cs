using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Services
{
	public class TmService
	{
		private readonly Repository.TMs Tms_;
		public TmService(Repository.TMs Tms)
		{
			this.Tms_ = Tms;
		}
		public IEnumerable<Dto.TM> GetTmsByGame(string GameId)
		{
			
			var ret = Tms_.GetTMs(GameId);
			return ret;
		}
	}
}
