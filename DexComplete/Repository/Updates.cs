﻿using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Repository
{
	public class Updates
	{
		private readonly Data.PokedexModel Model_;

		public Updates(Data.PokedexModel Model)
		{
			this.Model_ = Model;
		}
		public IEnumerable<Data.Update> GetUpdates()
		{
			

			return Model_.Updates.OrderByDescending(v => v.Date).ToList();
		}

		public IEnumerable<Data.ComingSoon> GetComingSoon()
		{
			
			var result = Model_.ComingSoon;
			return result.ToList();

		}
	}
}