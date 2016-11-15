using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Data
{
	public partial class AbilityEntry
	{
		public int Id { get; set; }
		public int Index { get; set; }
		public string Note { get; set; }

		public string PokemonId { get; set; }
		public string AbilityId { get; set; }
		public string AbilitySetId { get; set; }
		public string GenerationId { get; internal set; }

		public virtual Pokemon Pokemon { get; set; }
		public virtual Ability Ability { get; set; }
		public virtual AbilitySet Set { get; set; }
	}
}
