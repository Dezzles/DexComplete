using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Data
{
	public partial class AbilitySet
	{
		public int Id { get; set; }
		public int Index { get; set; }
		public int GenerationId { get; set; }
		public int AbilityId { get; set; }
		public virtual Generation Generation { get; set; }
		public virtual ICollection<AbilityEntry> Entries { get; set; }
	}
}
