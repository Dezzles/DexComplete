using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Models
{
	public class PokedexModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Identifier { get; set; }
		public IEnumerable<PokemonModel> Pokemon { get; set; }
	}
}
