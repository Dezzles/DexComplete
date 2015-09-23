using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Data
{
	public partial class Ability
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<AbilityEntry> Entries { get; set; }
	}
}
