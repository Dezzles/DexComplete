using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.View
{
	public static class UpdatesRepository
	{
		public static Dictionary<DateTime, List<string>> GetRecentUpdates()
		{
			Dictionary<DateTime, List<String>> results = new Dictionary<DateTime, List<string>>();
			using (Data.PokedexModel model = new Data.PokedexModel())
			{
				var items = model.Updates.OrderByDescending(v => v.Date);

				foreach (var item in items)
				{
					if (!results.ContainsKey(item.Date))
					{
						if (results.Count == 5)
							return results;
						results.Add(item.Date, new List<string>());
					}
					results[item.Date].Add(item.Text);
				}
			}

			return results;
		}

		public static IEnumerable<string> GetComingSoon()
		{
			List<string> results = new List<string>();

			using (Data.PokedexModel model = new Data.PokedexModel())
			{
				var items = model.ComingSoon;
				foreach (var item in items)
				{
					results.Add(item.Text);
				}
			}

			return results;
		}
	}
}
