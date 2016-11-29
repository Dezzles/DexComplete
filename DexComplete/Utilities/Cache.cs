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
		Dictionary<string, object> Data = new Dictionary<string, object>();
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
		internal Out GetResult<Out, In1, In2, In3>(Func<In1, In2, In3, Out> F, 
			In1 P1, In2 P2, In3 P3)
		{
			string t = Utilities.Helpers.GetName(F) + Utilities.Helpers.GetParameters(P1, P2, P3);
			if (Data.ContainsKey(t))
			{
				return (Out)Data[t];
			}
			Out u = F.Invoke(P1, P2, P3);
			try
			{
				Data.Add(t, u);
			}
			catch (ArgumentException)
			{
			}
			return u;
		}

		internal Out GetResult<Out, In1, In2>(Func<In1, In2, Out> F, In1 P1, In2 P2)
		{
			string t = Utilities.Helpers.GetName(F) + Utilities.Helpers.GetParameters(P1, P2);
			if (Data.ContainsKey(t))
			{
				return (Out)Data[t];
			}
			Out u = F.Invoke(P1, P2);
			try
			{
				Data.Add(t, u);
			}
			catch (ArgumentException)
			{
			}
			return u;
		}

		internal Out GetResult<Out, In1>(Func<In1, Out> F, In1 P1)
		{
			string t = Utilities.Helpers.GetName(F) + Utilities.Helpers.GetParameters(P1);
			if (Data.ContainsKey(t))
			{
				return (Out)Data[t];
			}
			Out u = F.Invoke(P1);
			try
			{ 
				Data.Add(t, u);
			}
			catch (ArgumentException)
			{
			}
			return u;
		}
		internal Out GetResult<Out>(Func<Out> F)
		{
			string t = Utilities.Helpers.GetName(F) + Utilities.Helpers.GetParameters();
			if (Data.ContainsKey(t))
			{
				return (Out)Data[t];
			}
			Out u = F.Invoke();
			try
			{
				Data.Add(t, u);
			}
			catch (ArgumentException)
			{
			}
			return u;
		}
	}
}