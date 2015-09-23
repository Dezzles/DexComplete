using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Transfer
{
	public class AddGame
	{
		public string Identifier { get; set; }
		public string SaveName { get; set; }
	}

	public class Saves
	{
		public string SaveName { get; set; }
		public string GameIdentifier { get; set; }
		public string SaveData { get; set; }
		public string AbilityData { get; set; }
		public string DittoData { get; set; }
		public string BerryData { get; set; }
		public string EggGroupData { get; set; }
		public string GameTitle { get; set; }
		public string TMData { get; set; }
	}

	public class GameToolItems
	{
		public string Identifier { get; set; }
		public string Title { get; set; }
	}

	public class GameTools
	{
		public IEnumerable<Models.PokedexModel> Pokedex { get; set; }
		public IEnumerable<GameToolItems> Tools { get; set; }
		public IEnumerable<GameToolItems> Collections { get; set; }
	}

	public class AbilityPokemonTfr
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Ability { get; set; }
		public string Note { get; set; }
	}

	public class AbilitySetTfr
	{
		public int SetId { get; set; }
		public List<AbilityPokemonTfr> Pokemon { get; set; }

		public int AbilityId { get; set; }
	}

	public class IdNameTransfer
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class GameProgress
	{
		public List<ItemProgress> Pokedexes { get; set; }
		public List<ItemProgress> Collections { get; set; }
	}

	public class ItemProgress
	{
		public string Identifier { get; set; }
		public string Title { get; set; }
		public int Completion { get; set; }
		public int Total { get; set; }
	}
}
