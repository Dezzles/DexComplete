using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Services
{
	public static class TmService
	{
		public static IEnumerable<Dto.TM> GetTmsByGame(string GameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			var ret = Repository.TMs.GetTMs(GameId, Log);
			/*List<Transfer.IdNameTransfer> ret = new List<Transfer.IdNameTransfer>();
			var tmList = Repository.TMs.GetTMs(GameId, Log);
			foreach (var tm in tmList)
			{
				ret.Add(new Transfer.IdNameTransfer()
					{
							Index = tm.Index,
							Name = tm.Move.Name
					});
			}/**/
			return ret;
		}
	}
}
