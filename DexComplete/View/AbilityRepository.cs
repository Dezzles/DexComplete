using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.View
{
	public class AbilityRepository
	{
		public static IEnumerable<Transfer.AbilitySetTfr> GetAbilitiesByGame(string gameId)
		{
			if (ServerRepository.RequiresAbilityUpdate())
				UpdateAbilitySet();
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var game = ctr.Games.SingleOrDefault(e => e.Identifier == gameId);
				if (game == null)
					throw new Code.ExceptionResponse("Invalid game");
				if (game.Generation.AbilitySets.Count == 0)
					throw new Code.Exception404();
				var sets = game.Generation.AbilitySets.OrderBy(e => e.Index);
				List<Transfer.AbilitySetTfr> ret = new List<Transfer.AbilitySetTfr>();
				foreach (var set in sets)
				{
					var entries = ctr.AbilityEntries.Where(e => e.AbilitySetId == set.Id);
					Transfer.AbilitySetTfr tfr = new Transfer.AbilitySetTfr();
					tfr.SetId = set.Id;
					tfr.AbilityId = set.AbilityId;
					tfr.Pokemon = new List<Transfer.AbilityPokemonTfr>();
					foreach ( var pkm in entries)
					{
						Transfer.AbilityPokemonTfr mon = new Transfer.AbilityPokemonTfr();
						var ability = ctr.Abilities.Single(e => e.Id == pkm.AbilityId);
						var dbmon = ctr.Pokemons.Single(e => e.Id == pkm.PokemonId);
						mon.Ability = ability.Name;
						mon.Id = dbmon.Id;
						mon.Name = dbmon.Name;
						mon.Note = pkm.Note;
						tfr.Pokemon.Add(mon);
					}
					ret.Add(tfr);
				}
				return ret;
			}
		}

		public static void UpdateAbilitySet()
		{
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				int minId5 = ctr.AbilitySets.Where(e => e.GenerationId == 5).OrderBy(e => e.Id).First().Id;
				int minId6 = ctr.AbilitySets.Where(e => e.GenerationId == 6).OrderBy(e => e.Id).First().Id;
				foreach (var u in ctr.AbilitySets)
				{
					u.AbilityId = u.Id - (u.GenerationId == 5 ? minId5 : minId6);
				}
				ctr.SaveChanges();
			}
			ServerRepository.AbilityUpdatePerformed();
		}
	}
}
