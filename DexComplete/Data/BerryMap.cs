using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Data
{
	public partial class BerryMap
	{
		public int Id { get; set; }
		public int BerryId { get; set; }
		public int GenerationId { get; set; }
		public int Index { get; set; }

		public virtual Berry Berry { get; set; }
		public virtual Generation Generation { get; set; }
	}
}
