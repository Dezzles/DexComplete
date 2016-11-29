using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Models
{
	public class PokedexModel
	{
		public string Title { get; set; }
		public string PokedexId { get; set; }
		public bool Regional { get; set; }
		public IEnumerable<PokemonModel> Pokemon { get; set; }
	}
}
