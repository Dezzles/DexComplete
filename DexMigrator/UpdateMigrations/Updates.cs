using DexMigrator.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexMigrator.UpdateMigrations
{
	static class UpdateUtil
	{
		static UpdateUtil()
		{
			DateTime now = DateTime.Now;
			Now = new DateTime(now.Year, now.Month, now.Day);
		}
		static DateTime Now;
		public static void AddUpdate(FluentMigrator.Migration obj, string Text)
		{
			obj.Insert.IntoTable("Updates")
				.Row(new { Date = Now, Text = Text });
		}
	}

	[FluentMigrator.Migration(201510211822)]
	public class Update_0001 : FluentMigrator.Migration
	{
		public override void Up()
		{
			UpdateUtil.AddUpdate(this, "Improved navigation menu when viewing other user's pages");
		}

		public override void Down()
		{
			throw new NotImplementedException();
		}
	}

	[FluentMigrator.Migration(201602061757)]
	public class Update_0002 : FluentMigrator.Migration
	{
		public override void Up()
		{
			UpdateUtil.AddUpdate(this, "Implemented password resetting");
			UpdateUtil.AddUpdate(this, "Fixed issue where logins weren't working");
			UpdateUtil.AddUpdate(this, "Improved password security");

			Insert.IntoTable("ServerSettings")
				.Row(new { Config = "EmailAddress", String = "no-reply@dezzles.com" })
				.Row(new { Config = "EmailPassword", String = "testPasswordHere" })
				.Row(new { Config = "SMTPDetails", String = "smtp.gmail.com"})
				.Row(new { Config = "SMTPPort", Integer = 587 });
		}

		public override void Down()
		{
			throw new NotImplementedException();
		}
	}

	[FluentMigrator.Migration(201610301627)]
	public class Update_SunMoon_0001 : FluentMigrator.Migration
	{
		public override void Down()
		{
			throw new NotImplementedException();
		}

		public override void Up()
		{

		}
	}
}
