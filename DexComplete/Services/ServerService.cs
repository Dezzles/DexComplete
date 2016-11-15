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
		public void ThrowMaintenance()
		{
			
			if (InMaintenance())
				throw new Code.ExceptionMaintenance();
		}
		public bool InMaintenance()
		{
			
			return Model_.ServerSettings.Single(e => e.Config == "Maintenance").Boolean.Value;
		}

		public string GetEmailAddress()
		{
			
			return Model_.ServerSettings.Single(e => e.Config == "EmailAddress").String;

		}
		public string GetSMTPSettings()
		{
			
			return Model_.ServerSettings.Single(e => e.Config == "SMTPDetails").String;
		}
		public string GetEmailPassword()
		{
			
			return Model_.ServerSettings.Single(e => e.Config == "EmailPassword").String;

		}
		public int GetSMTPPort()
		{
			
			return Model_.ServerSettings.Single(e => e.Config == "SMTPPort").Integer.Value;
		}

		public string GetServerAddress()
		{
			
			return Model_.ServerSettings.Single(e => e.Config == "ServerAddress").String;
		}
	}
}
