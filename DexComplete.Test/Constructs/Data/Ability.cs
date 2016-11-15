using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Test.Constructs.Data
{
	class Ability
	{
		public static DexComplete.Data.Ability Data_001()
		{
			return new DexComplete.Data.Ability()
			{
				AbilityId = "chlorophyll",
				Name = "Chlorophyll"
			};
		}
		public static DexComplete.Data.Ability Data_002()
		{
			return new DexComplete.Data.Ability()
			{
				AbilityId = "blaze",
				Name = "Blaze"
			};
		}
	}
}
