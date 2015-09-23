using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DexMigrator
{
	class Utilities
	{
		public static string GetResource(string name)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = name;

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					string result = reader.ReadToEnd();
					return result;
				}
			}
		}

		class DataInfo
		{
			public enum InternalType
			{
				TStr,
				TInt
			}
			public DataInfo(string info)
			{
				var i = info.Split(new char[] { '|' });
				ColumnName = i[0];
				if (i[1] == "i")
					BaseType = InternalType.TInt;
				if (i[1] == "s")
					BaseType = InternalType.TStr;
			}
			public object Convert(string s)
			{
				switch (BaseType)
				{
					case InternalType.TStr:
						return s;
					case InternalType.TInt:
						return int.Parse(s);
				}
				return null;
			}

			public InternalType BaseType { get; set; }
			public string ColumnName { get; set; }
		}

		public static List<Dictionary<string, object>> GetEntries(string name)
		{
			System.Console.WriteLine("Loading: " + name);
			char[] spl = new char[] { '\t' };
			string data = GetResource(name);
			List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();
			string[] entries = data.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
			string[] titles = entries[0].Split(spl);
			DataInfo[] headers = new DataInfo[titles.Length];
			for (int i = 0; i < titles.Length; ++i )
			{
				headers[i] = new DataInfo(titles[i]);
			}
			for (int i = 1; i < entries.Length; ++i)
			{
				Dictionary<string, object> o = new Dictionary<string, object>();
				string[] bit = entries[i].Split(spl);
				for (int j = 0; j < bit.Length; ++j)
				{
					o.Add(headers[j].ColumnName, headers[j].Convert(bit[j]));
				}
				results.Add(o);
			}

			return results;
		}
	}
}
