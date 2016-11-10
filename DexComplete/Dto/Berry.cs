using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Dto
{
	public class Berry
	{
		public string BerryId { get; set; }
		public string Name { get; set; }
		public int Index { get; set; }

		public Berry(Data.Berry Old)
		{
			this.BerryId = Old.BerryId;
			this.Name = Old.Name;
			this.Index = Old.Index;
		}
	}
}