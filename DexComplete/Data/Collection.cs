using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Data
{
	public partial class Collection
	{
		public string Title { get; set; }
		public string CollectionId { get; set; }
		public Data.CollectionType Type { get; set; }

		public virtual ICollection<Game> Games { get; set; }
	}
}
