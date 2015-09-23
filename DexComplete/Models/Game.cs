using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Models
{
	public class GameModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Identifier { get; set; }
		public PokedexModel Pokedexes { get; set; }
	}
}