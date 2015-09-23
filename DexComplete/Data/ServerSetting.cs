using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Data
{
	public partial class ServerSetting
	{
		public int Id { get; set; }
		public string Config { get; set; }
		public Boolean? Boolean { get; set; }
		public int? Integer { get; set; }
		public String String { get; set; }
	}
}