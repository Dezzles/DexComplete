using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Data
{
	public partial class AbilitySet
	{
		public string PokemonId { get; set; }
		public string GenerationId { get; set; }
		public int AbilityId { get; set; }
		public virtual Generation Generation { get; set; }
		public virtual ICollection<AbilityEntry> Entries { get; set; }
	}
}
