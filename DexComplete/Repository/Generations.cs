using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Repository
{
	public class Generations
	{
		public static Dto.Generation GetGenerationById(string GenId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			using (var ctr = new Data.PokedexModel())
			{
				var res = ctr.Generations.SingleOrDefault(u => u.GenerationId == GenId);
				if (res == null)
				{
					Log.Error("GetGenerationById", new { GenId = GenId });
				}

				return new Dto.Generation(res);
			}
		}
	}
}