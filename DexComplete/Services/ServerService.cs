using DexComplete.Utilities;
using SharpLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Services
{
	public class ServerService
	{
		private readonly Data.PokedexModel Model_;
		public ServerService(Data.PokedexModel Model)
		{
			Model_ = Model;
		}
		public void ThrowMaintenance(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			if (InMaintenance(Log))
				throw new Code.ExceptionMaintenance();
		}
		public bool InMaintenance(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			return Model_.ServerSettings.Single(e => e.Config == "Maintenance").Boolean.Value;
		}

		public string GetEmailAddress(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			return Model_.ServerSettings.Single(e => e.Config == "EmailAddress").String;

		}
		public string GetSMTPSettings(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			return Model_.ServerSettings.Single(e => e.Config == "SMTPDetails").String;
		}
		public string GetEmailPassword(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			return Model_.ServerSettings.Single(e => e.Config == "EmailPassword").String;

		}
		public int GetSMTPPort(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			return Model_.ServerSettings.Single(e => e.Config == "SMTPPort").Integer.Value;
		}

		public string GetServerAddress(SLLog Log)
		{
			Log = Logging.GetLog(Log);
			return Model_.ServerSettings.Single(e => e.Config == "ServerAddress").String;
		}
	}
}
