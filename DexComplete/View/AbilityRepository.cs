using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.View
{
	public class AbilityRepository
	{
		public static IEnumerable<Transfer.AbilitySetTfr> GetAbilitiesByGame(string gameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			Log.Info("GetAbilitiesByGame", new { gameId = gameId });
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				var game = ctr.Games.SingleOrDefault(e => e.GameId == gameId);
				if (game == null)
				{
					Log.Error("GetAbilitesByGame", new { gameId = gameId, error = "Invalid game" });
					throw new Code.ExceptionResponse("Invalid game");
				}
				if (game.Generation.AbilitySets.Count == 0) {
					Log.Error("GetAbilitesByGame", new { gameId = gameId, error = "No abilities found" });
					throw new Code.Exception404();
				}
				var sets = game.Generation.AbilitySets.OrderBy(e => e.AbilityId);
				Log.Info("GetAbilitiesByGame", new { setsFound = sets.Count() });
				List<Transfer.AbilitySetTfr> ret = new List<Transfer.AbilitySetTfr>();
				foreach (var set in sets)
				{
					var entries = set.Entries;
					Transfer.AbilitySetTfr tfr = new Transfer.AbilitySetTfr();
					tfr.SetId = set.PokemonId;
					tfr.AbilityId = set.AbilityId;
					int lowest = 15000;
					tfr.Pokemon = new List<Transfer.AbilityPokemonTfr>();
					foreach ( var pkm in entries)
					{
						Transfer.AbilityPokemonTfr mon = new Transfer.AbilityPokemonTfr();
						var ability = ctr.Abilities.Single(e => e.AbilityId == pkm.AbilityId);
						var dbmon = ctr.Pokemons.Single(e => e.PokemonId == pkm.PokemonId);
						mon.Ability = ability.Name;
						mon.Id = dbmon.PokemonId;
						mon.Name = dbmon.Name;
						mon.Note = pkm.Note;
						lowest = dbmon.Index < lowest ? dbmon.Index : lowest;
						tfr.Pokemon.Add(mon);
					}
					tfr.AbilityId = lowest;
					ret.Add(tfr);
				}
				Log.Info("GetAbilitiesByGame", new { message = "Successfully found games"});
				return ret;
			}
		}
	}
}
