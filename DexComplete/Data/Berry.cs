using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Data
{
	public class Berry : NamedIndex
	{
		public string BerryId { get; set; }
		public virtual ICollection<BerryMap> BerryMaps { get; set; }
	}
}
