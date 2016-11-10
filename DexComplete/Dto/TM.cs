using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Dto
{
	public class TM
	{
		public int Index { get; set; }
		public string Name { get; set; }

		public TM(Data.TM tm)
		{
			this.Index = tm.Index;
			this.Name = tm.Move.Name;
		}
	}
}