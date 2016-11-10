using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DexComplete.Repository
{
	public class Berries
	{
		public static IEnumerable<Dto.Berry> GetBerries(string GameId, SLLog Log)
		{
			Log = Logging.GetLog(Log);
			var game = Games.GetGameById(GameId, Log);
			if (game == null)
				return null;
			return game.Generation.Berries;
		}

	}
}