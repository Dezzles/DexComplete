using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;

namespace DexComplete.Utilities
{
	public class Helpers
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string GetCurrentMethod(params object[] lst)
		{
			StackTrace st = new StackTrace();
			StackFrame sf = st.GetFrame(1);
			StringBuilder sb = new StringBuilder();
			var method = sf.GetMethod();
			sb.Append(method.Module.FullyQualifiedName + "." + method.Name);
			sb.Append("(");
			var first = true;
			for (var v = 0; v < lst.Length; ++v)
			{
				if (!first) sb.Append(",");
				sb.Append(lst[v]);
			}
			sb.Append(")");
			return sb.ToString();
		}
	}
}