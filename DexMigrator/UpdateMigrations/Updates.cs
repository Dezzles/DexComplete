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
			UpdateUtil.AddUpdate(this, "Improved navigation menu when viewing other user's pages" );
		}

		public override void Down()
		{
			throw new NotImplementedException();
		}
	}
}
