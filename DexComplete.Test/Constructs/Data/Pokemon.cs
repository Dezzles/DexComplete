using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Test.Constructs.Data
{
	class Pokemon
	{
		public static DexComplete.Data.Pokemon Data_001()
		{
			return new DexComplete.Data.Pokemon()
			{
				Index = 1,
				Name = "Bulbasaur",
				PokemonId = "bulbasaur"
			};
		}
		public static DexComplete.Data.Pokemon Data_002()
		{
			return new DexComplete.Data.Pokemon()
			{
				Index = 2,
				Name = "Ivysaur",
				PokemonId = "ivysaur"
			};
		}
		public static DexComplete.Data.Pokemon Data_003()
		{
			return new DexComplete.Data.Pokemon()
			{
				Index = 3,
				Name = "Venusaur",
				PokemonId = "venusaur"
			};
		}

		public static DexComplete.Data.Pokemon Data_004()
		{
			return new DexComplete.Data.Pokemon()
			{
				Index = 4,
				Name = "Charmander",
				PokemonId = "charmander"
			};
		}
	}
}
