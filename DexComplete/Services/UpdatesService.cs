using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Services
{
	public class UpdatesService
	{
		private readonly Repository.Updates Updates_;
		public UpdatesService(Repository.Updates Updates)
		{
			Updates_ = Updates;
		}
		public Dictionary<DateTime, List<string>> GetRecentUpdates(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			var items = Updates_.GetUpdates(Log);
			Dictionary<DateTime, List<String>> results = new Dictionary<DateTime, List<string>>();
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

			return results;
		}

		public IEnumerable<string> GetComingSoon(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			List<string> results = new List<string>();
			var items = Updates_.GetComingSoon(Log);
			foreach (var item in items)
			{
				results.Add(item.Text);
			}

			return results;
		}
	}
}
