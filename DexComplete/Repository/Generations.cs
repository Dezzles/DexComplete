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
		private readonly Data.PokedexModel Model_;
		private readonly SLLog Log_;
		public Generations(Data.PokedexModel Model, SLLog Log)
		{
			Model_ = Model;
			Log_ = Log;
		}
		public Dto.Generation GetGenerationById(string GenId)
		{
			
			var res = Model_.Generations.SingleOrDefault(u => u.GenerationId == GenId);
			if (res == null)
			{
				Log_.Error("GetGenerationById", new { GenId = GenId });
			}

			return new Dto.Generation(res);
		}
	}
}