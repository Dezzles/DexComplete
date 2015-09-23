using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Data
{
	public class Berry
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<BerryMap> BerryMaps { get; set; }
	}
}
