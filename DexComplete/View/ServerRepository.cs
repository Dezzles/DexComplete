using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.View
{
	public static class ServerRepository
	{
		public static void ThrowMaintenance()
		{
			if (InMaintenance())
				throw new Code.ExceptionMaintenance();
		}
		public static bool InMaintenance()
		{
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				return ctr.ServerSettings.Single(e => e.Config == "Maintenance").Boolean.Value;
			}
		}

		public static bool RequiresAbilityUpdate()
		{
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				return ctr.ServerSettings.Single(e => e.Config == "AbilityUpdateRequired").Boolean.Value;
			}
		}

		public static void AbilityUpdatePerformed()
		{
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				ctr.ServerSettings.Single(e => e.Config == "AbilityUpdateRequired").Boolean = false;
				ctr.SaveChanges();
			}
		}
	}
}
