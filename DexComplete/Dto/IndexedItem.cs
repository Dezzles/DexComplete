using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DexComplete.Data;

namespace DexComplete.Dto
{
	public class IndexedItem
	{
		public IndexedItem(NamedIndex v)
		{
			this.Text = v.Name;
			this.Index = v.Index;
		}

		public string Text { get; set; }
		public int Index { get; set; }
	}
}