using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DexComplete.Dto;
using DexComplete.Services;
using System.Text;

namespace DexComplete.Utilities
{
	public class Cache
	{
		public string GetPath(string fn, params object[] p)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(fn);
			sb.Append("(");
			int count = p.Count();
			for (int i = 0; i < count; ++i)
			{
				if (i != 0)
					sb.Append(",");
				sb.Append(p[i]);
			}
			return sb.ToString();
		}
		public object GetCache(string Path)
		{
			return "";
		}
		internal Out GetResult<Out, In1, In2>(Func<In1, In2, Out> F, In1 P1, In2 P2)
		{

			return F.Invoke(P1, P2);
		}

		internal Out GetResult<Out, In1>(Func<In1, Out> F, In1 P1)
		{
			string t = F.ToString();
			return F.Invoke(P1);
			//throw new NotImplementedException();
		}
		internal Out GetResult<Out>(Func<Out> F)
		{
			throw new NotImplementedException();
		}
	}
}