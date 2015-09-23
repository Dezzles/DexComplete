using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DexMigrator
{
	class Program
	{
		static void Main(string[] args)
		{
			string connection = @"data source=DEZZLES-LAPTOP\SQLEXPRESS;initial catalog=DexComplete;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
			MigrateToLatest(connection);
		}

		public static void MigrateToLatest(string connectionString)
		{
			// var announcer = new NullAnnouncer();
			var announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
			var assembly = Assembly.GetExecutingAssembly();

			var migrationContext = new RunnerContext(announcer)
			{
				Namespace = "DexMigrator.Migrations"
				
			};

			var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
			var factory = new FluentMigrator.Runner.Processors.SqlServer.SqlServer2008ProcessorFactory();
			using (var processor = factory.Create(connectionString, announcer, options))
			{
				var runner = new MigrationRunner(assembly, migrationContext, processor);
				runner.MigrateUp(true);
			}
		}
	}

	public class MigrationOptions : IMigrationProcessorOptions
    {
        public bool PreviewOnly { get; set; }
        public string ProviderSwitches { get; set; }
        public int Timeout { get; set; }
    }

   
}
