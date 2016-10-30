using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Data
{
	public partial class TM
	{
		public int Id { get; set; }
		public String MoveId { get; set; }
		public string TmSetId { get; set; }
		public int Index { get; set; }
		public virtual Move Move { get; set; }
		//public virtual TMSet TMSet { get; set; }

	}
}
