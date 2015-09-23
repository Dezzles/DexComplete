using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Transfer
{
	public class Requests
	{
		public string Username { get; set; }
		public string Token { get; set; }
	}

	public class Requests<T> : Requests
	{
		public T Value { get; set; }
	}
}
