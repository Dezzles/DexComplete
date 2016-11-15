using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Test.Constructs.Data
{
	class Berry
	{
		public static DexComplete.Data.Berry Data_001()
		{
			return new DexComplete.Data.Berry()
			{
				BerryId = "some_berry",
				Index = 1,
				Name = "Some Berry"
			};
		}
		public static DexComplete.Data.Berry Data_002()
		{
			return new DexComplete.Data.Berry()
			{
				BerryId = "some_berry_else",
				Index = 2,
				Name = "Some Berry Else"
			};
		}
		public static DexComplete.Data.Berry Data_003()
		{
			return new DexComplete.Data.Berry()
			{
				BerryId = "some_berry_to_love",
				Index = 3,
				Name = "Some Berry To Love"
			};
		}
	}
}
