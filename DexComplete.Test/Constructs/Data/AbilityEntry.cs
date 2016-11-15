using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Test.Constructs.Data
{
	class AbilityEntry
	{
		public static DexComplete.Data.AbilityEntry Data_001()
		{
			return new DexComplete.Data.AbilityEntry()
			{
				AbilitySetId = "bulbasaur",
				Index = 1,
				Note = "Potato",
				Pokemon = Pokemon.Data_001(),
				PokemonId = Pokemon.Data_001().PokemonId,
				Ability = Ability.Data_001(),
				AbilityId = Ability.Data_001().AbilityId
			};
		}
		public static DexComplete.Data.AbilityEntry Data_002()
		{
			return new DexComplete.Data.AbilityEntry()
			{
				AbilitySetId = "bulbasaur",
				Index = 2,
				Note = "Potato",
				Pokemon = Pokemon.Data_002(),
				PokemonId = Pokemon.Data_002().PokemonId,
				Ability = Ability.Data_001(),
				AbilityId = Ability.Data_001().AbilityId
			};
		}
		public static DexComplete.Data.AbilityEntry Data_003()
		{
			return new DexComplete.Data.AbilityEntry()
			{
				AbilitySetId = "bulbasaur",
				Index = 3,
				Note = "Potato",
				Pokemon = Pokemon.Data_003(),
				PokemonId = Pokemon.Data_003().PokemonId,
				Ability = Ability.Data_001(),
				AbilityId = Ability.Data_001().AbilityId
			};
		}
		public static DexComplete.Data.AbilityEntry Data_004()
		{
			return new DexComplete.Data.AbilityEntry()
			{
				AbilitySetId = "charmander",
				Index = 1,
				Note = "",
				Pokemon = Pokemon.Data_004(),
				PokemonId = Pokemon.Data_004().PokemonId,
				Ability = Ability.Data_002(),
				AbilityId = Ability.Data_002().AbilityId
			};
		}
	}
}
