using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Data
{
	public partial class TMSet
	{
		public int Id { get; set; }
		public string Identifier { get; set;}

		public virtual ICollection<TM> TMs { get; set; }
		public virtual ICollection<Game> Games { get; set; }
	}
}
