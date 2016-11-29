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
		public static string GetParameters(params object[] lst)
		{
			StringBuilder sb = new StringBuilder();
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
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string GetCurrentMethod(params object[] lst)
		{
			StackTrace st = new StackTrace();
			StackFrame sf = st.GetFrame(1);
			StringBuilder sb = new StringBuilder();
			var method = sf.GetMethod();
			sb.Append(method.DeclaringType.FullName + "." + method.Name);
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


		#region FunctionName_Getters

		internal static string GetName<Out>(Func<Out> F)
		{
			return F.Target.ToString() + "." + F.Method.Name;
		}
		internal static string GetName<Out, I>(Func<Out, I> F)
		{
			return F.Target.ToString() + "." + F.Method.Name;
		}
		internal static string GetName<Out, I1, I2>(Func<Out, I1, I2> F)
		{
			return F.Target.ToString() + "." + F.Method.Name;
		}

		internal static string GetName<Out, I1, I2, I3>(Func<Out, I1, I2, I3> F)
		{
			return F.Target.ToString() + "." + F.Method.Name;
		}



		#endregion

	}
}