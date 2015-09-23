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

		public int PokemonId { get; set; }
		public int AbilityId { get; set; }
		public int AbilitySetId { get; set; }

		public virtual Pokemon Pokemon { get; set; }
		public virtual Ability Ability { get; set; }
		public virtual AbilitySet Set { get; set; }
	}
}
