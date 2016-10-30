using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Data
{
	public partial class Move
	{
		public string MoveId { get; set; }
		public string Name { get; set; }
		public virtual ICollection<TM> Tms { get; set; }
	}
}
