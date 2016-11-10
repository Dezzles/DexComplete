using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Services
{
	public static class ServerService
	{
		public static void ThrowMaintenance(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			if (InMaintenance(Log))
				throw new Code.ExceptionMaintenance();
		}
		public static bool InMaintenance(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				return ctr.ServerSettings.Single(e => e.Config == "Maintenance").Boolean.Value;
			}
		}

		public static string GetEmailAddress(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				return ctr.ServerSettings.Single(e => e.Config == "EmailAddress").String;
			}

		}
		public static string GetSMTPSettings(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				return ctr.ServerSettings.Single(e => e.Config == "SMTPDetails").String;
			}

		}
		public static string GetEmailPassword(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				return ctr.ServerSettings.Single(e => e.Config == "EmailPassword").String;
			}

		}
		public static int GetSMTPPort(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				return ctr.ServerSettings.Single(e => e.Config == "SMTPPort").Integer.Value;
			}

		}

		public static string GetServerAddress(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			using (Data.PokedexModel ctr = new Data.PokedexModel())
			{
				return ctr.ServerSettings.Single(e => e.Config == "ServerAddress").String;
			}
		}
	}
}
