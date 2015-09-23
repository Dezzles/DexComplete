using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.View
{
	public static class EggGroupRepository
	{
		public static IEnumerable<Transfer.IdNameTransfer> GetEggGroupsByGame(string gameId)
		{
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var eggGroups = ctr.EggGroups.OrderBy( e =>e.Index);
				List<Transfer.IdNameTransfer> ret = new List<Transfer.IdNameTransfer>();
				foreach (var egg in eggGroups)
				{
					ret.Add(new Transfer.IdNameTransfer()
						{
							Id = egg.Id,
							Name = egg.Name
						});
				}
				return ret;
			}
		}
	}
}
