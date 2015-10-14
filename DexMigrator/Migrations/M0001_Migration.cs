using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexMigrator.Migrations
{
	public class MigrationTools
	{
		public static void InputTable(FluentMigrator.Migration mig, string Table, List<Dictionary<string, object>> data)
		{
			System.Console.WriteLine("Adding Table: " + Table);
			foreach (var entry in data)
			{
				mig.Insert.IntoTable(Table)
					.Row(entry);
			}
			System.Console.WriteLine("Added Table: " + Table);

		}

	}
	[FluentMigrator.Migration(201508131831)]
	public class M0001_CreateDatabase : FluentMigrator.Migration
	{
		public override void Up()
		{
			Create.Table("Users")
				.WithColumn("Id").AsInt64().Identity()
				.WithColumn("Username").AsString()
				.WithColumn("Email").AsString()
				.WithColumn("Password").AsString()
				.WithColumn("Salt").AsString();

			Create.Table("Tokens")
				.WithColumn("Id").AsInt64().Identity()
				.WithColumn("Value").AsString()
				.WithColumn("ExpiryDate").AsDateTime()
				.WithColumn("UserId").AsInt64();

			Create.Table("Pokedexes")
				.WithColumn("Id").AsInt32()
				.WithColumn("Title").AsString()
				.WithColumn("Identifier").AsString();

			Create.Table("Games")
				.WithColumn("Id").AsInt32()
				.WithColumn("Title").AsString()
				.WithColumn("GenerationId").AsInt32()
				.WithColumn("Identifier").AsString();

			Create.Table("Generations")
				.WithColumn("Id").AsInt32()
				.WithColumn("Title").AsString()
				.WithColumn("Identifier").AsString();

			Create.Table("Saves")
				.WithColumn("Id").AsInt64().Identity()
				.WithColumn("Code").AsBinary(256)
				.WithColumn("SaveName").AsString()
				.WithColumn("UserId").AsInt64()
				.WithColumn("GameId").AsInt32();

			Create.Table("Pokemon")
				.WithColumn("Id").AsInt32()
				.WithColumn("Name").AsString();

			Create.Table("Entries")
				.WithColumn("Id").AsInt32().Identity()
				.WithColumn("Index").AsInt32()
				.WithColumn("PokedexId").AsInt32()
				.WithColumn("PokemonId").AsInt32();

			Create.Table("GamePokedex")
				.WithColumn("GameId").AsInt32()
				.WithColumn("PokedexId").AsInt32();

			Create.PrimaryKey("PK_Users")
				.OnTable("Users").Column("Id");

			Create.PrimaryKey("PK_Tokens")
				.OnTable("Tokens").Column("Id");

			Create.PrimaryKey("PK_Pokedexes")
				.OnTable("Pokedexes").Column("Id");

			Create.PrimaryKey("PK_Games")
				.OnTable("Games").Column("Id");

			Create.PrimaryKey("PK_Generations")
				.OnTable("Generations").Column("Id");

			Create.PrimaryKey("PK_Saves")
				.OnTable("Saves").Column("Id");

			Create.PrimaryKey("PK_Pokemon")
				.OnTable("Pokemon").Column("Id");

			Create.PrimaryKey("PK_Entries")
				.OnTable("Entries").Column("Id");

			Create.PrimaryKey("PK_GamePokedex")
				.OnTable("GamePokedex").Columns(new string[] { "GameId", "PokedexId" });

			Create.ForeignKey("FK_UserToken")
				.FromTable("Tokens").ForeignColumn("UserId")
				.ToTable("Users").PrimaryColumn("Id");

			Create.Index("IX_FK_UserToken")
				.OnTable("Tokens")
				.OnColumn("UserId");
			
			Create.ForeignKey("FK_GameGeneration")
				.FromTable("Games").ForeignColumn("GenerationId")
				.ToTable("Generations").PrimaryColumn("Id");

			Create.Index("IX_FK_GameGeneration")
				.OnTable("Games")
				.OnColumn("GenerationId");

			Create.ForeignKey("FK_SaveUser")
				.FromTable("Saves").ForeignColumn("UserId")
				.ToTable("Users").PrimaryColumn("Id");

			Create.Index("IX_FK_SaveUser")
				.OnTable("Saves")
				.OnColumn("UserId");

			Create.ForeignKey("FK_SaveGame")
				.FromTable("Saves").ForeignColumn("GameId")
				.ToTable("Games").PrimaryColumn("Id");

			Create.Index("IX_FK_SaveGame")
				.OnTable("Saves")
				.OnColumn("GameId");

			Create.ForeignKey("FK_GamePokedex")
				.FromTable("GamePokedex").ForeignColumn("GameId")
				.ToTable("Games").PrimaryColumn("Id");

			Create.ForeignKey("FK_GamePokedex_Game")
				.FromTable("GamePokedex").ForeignColumn("PokedexId")
				.ToTable("Pokedexes").PrimaryColumn("Id");

			Create.Index("IX_FK_GamePokedex_Pokedex")
				.OnTable("GamePokedex")
				.OnColumn("PokedexId");

			Create.ForeignKey("FK_PokedexEntry")
				.FromTable("Entries").ForeignColumn("PokedexId")
				.ToTable("Pokedexes").PrimaryColumn("Id");

			Create.Index("IX_FK_PokedexEntry")
				.OnTable("Entries")
				.OnColumn("PokedexId");

			Create.ForeignKey("FK_EntryPokemon")
				.FromTable("Entries").ForeignColumn("PokemonId")
				.ToTable("Pokemon").PrimaryColumn("Id");

			Create.Index("IX_FK_EntryPokemon")
				.OnTable("Entries")
				.OnColumn("PokemonId");
		}

		public override void Down()
		{
			throw new NotImplementedException();
		}

	}

	[FluentMigrator.Migration(201508140832)]
	public class M0002_InsertData : FluentMigrator.Migration
	{

		public override void Up()
		{
			// Load Generations Data
			List<Dictionary<string, object>> generation = Utilities.GetEntries("DexMigrator.Data.S001_Generations.txt");
			List<Dictionary<string, object>> games = Utilities.GetEntries("DexMigrator.Data.S002_Games.txt");
			List<Dictionary<string, object>> pokedexes = Utilities.GetEntries("DexMigrator.Data.S003_Pokedexes.txt");
			List<Dictionary<string, object>> pokedexgame = Utilities.GetEntries("DexMigrator.Data.S004_PokedexGame.txt");
			List<Dictionary<string, object>> pokemon = Utilities.GetEntries("DexMigrator.Data.S005_Pokemon.txt");
			List<Dictionary<string, object>> pokedexentries = Utilities.GetEntries("DexMigrator.Data.S006_PokedexEntries.txt");

			InputTable("Generations", generation);
			InputTable("Games", games);
			InputTable("Pokedexes", pokedexes);
			InputTable("GamePokedex", pokedexgame);
			InputTable("Pokemon", pokemon);
			InputTable("Entries", pokedexentries);
		}

		public override void Down()
		{
			throw new NotImplementedException();
		}

		public void InputTable(string Table, List<Dictionary<string, object>> data)
		{
			System.Console.WriteLine("Adding Table: " + Table);
			foreach (var entry in data)
			{
				Insert.IntoTable(Table)
					.Row(entry);
			}
			System.Console.WriteLine("Added Table: " + Table);

		}
	}

	[FluentMigrator.Migration(201509021714)]
	public class M0003_AddSaveTitle : FluentMigrator.Migration
	{

		public override void Down()
		{
			throw new NotImplementedException();
		}

		public override void Up()
		{
			var abilities = Utilities.GetEntries("DexMigrator.Data.S007_Abilities.txt");
			var abilitysets = Utilities.GetEntries("DexMigrator.Data.S008_AbilitySet.txt");
			var entries = Utilities.GetEntries("DexMigrator.Data.S009_Entries.txt");

			Create.Table("Abilities")
				.WithColumn("Id").AsInt32().PrimaryKey()
				.WithColumn("Name").AsString();

			Create.Table("AbilitySets")
				.WithColumn("Id").AsInt32().PrimaryKey()
				.WithColumn("Index").AsInt32()
				.WithColumn("GenerationId").AsInt32().ForeignKey("Generations", "Id");

			Create.Table("AbilityEntries")
				.WithColumn("Id").AsInt32().PrimaryKey().Identity()
				.WithColumn("PokemonId").AsInt32().NotNullable().ForeignKey("Pokemon", "Id")
				.WithColumn("Index").AsInt32().NotNullable()
				.WithColumn("Note").AsString()
				.WithColumn("AbilityId").AsInt32().NotNullable().ForeignKey("Abilities", "Id")
				.WithColumn("AbilitySetId").AsInt32().NotNullable().ForeignKey("AbilitySets", "Id");


			MigrationTools.InputTable(this, "Abilities", abilities);
			MigrationTools.InputTable(this, "AbilitySets", abilitysets);
			MigrationTools.InputTable(this, "AbilityEntries", entries);
		}

		[FluentMigrator.Migration(201509021756)]
		public class M0004_AddAbilitySave : FluentMigrator.Migration
		{
			public override void Up()
			{
				byte[] row = new byte[80];
				Alter.Table("Saves")
					.AddColumn("AbilityData").AsBinary(80)
					.SetExistingRowsTo(row)
					.Nullable();
			}

			public override void Down()
			{
				throw new NotImplementedException();
			}
		}

		[FluentMigrator.Migration(201509030734)]
		public class M0005_AddDittoBerriesEggGroupSave : FluentMigrator.Migration
		{
			public override void Up()
			{
				byte[] row16 = new byte[16];

				Alter.Table("Saves")
					.AddColumn("DittoData").AsBinary(16)
					.SetExistingRowsTo(row16)
					.Nullable();

				Alter.Table("Saves")
					.AddColumn("EggGroupData").AsBinary(16)
					.SetExistingRowsTo(row16)
					.Nullable();

				Alter.Table("Saves")
					.AddColumn("BerryData").AsBinary(16)
					.SetExistingRowsTo(row16)
					.Nullable();

				Create.Table("EggGroups")
					.WithColumn("Id").AsInt32().PrimaryKey()
					.WithColumn("Name").AsString()
					.WithColumn("Index").AsInt32();

				Create.Table("Berries")
					.WithColumn("Id").AsInt32().PrimaryKey()
					.WithColumn("Name").AsString();

				Create.Table("BerryMaps")
					.WithColumn("Id").AsInt32().PrimaryKey().Identity()
					.WithColumn("BerryId").AsInt32()
					.WithColumn("Index").AsInt32()
					.WithColumn("GenerationId").AsInt32().ForeignKey("Generations", "Id");

			}

			public override void Down()
			{
				throw new NotImplementedException();
			}
		}
		[FluentMigrator.Migration(201509031734)]
		public class M0006_AddDittoBerriesEggGroupSaveData : FluentMigrator.Migration
		{
			public override void Up()
			{
				var eggGroups = Utilities.GetEntries("DexMigrator.Data.S010_EggGroups.txt");
				var berries = Utilities.GetEntries("DexMigrator.Data.S011_Berries.txt");
				var berryMaps = Utilities.GetEntries("DexMigrator.Data.S012_BerryMap.txt");
				MigrationTools.InputTable(this, "EggGroups", eggGroups);
				MigrationTools.InputTable(this, "Berries", berries);
				MigrationTools.InputTable(this, "BerryMaps", berryMaps);

			}

			public override void Down()
			{
				throw new NotImplementedException();
			}
		}

		[FluentMigrator.Migration(201509031755)]
		public class M0007_AddCollectionMappingSupport : FluentMigrator.Migration
		{
			public override void Up()
			{
				Create.Table("Collections")
					.WithColumn("Id").AsInt32().PrimaryKey()
					.WithColumn("Title").AsString()
					.WithColumn("Identifier").AsString()
					.WithColumn("Type").AsInt32();

				Create.Table("GameCollectionMap")
					.WithColumn("Id").AsInt32().PrimaryKey().Identity()
					.WithColumn("CollectionId").AsInt32().NotNullable().ForeignKey("Collections", "Id")
					.WithColumn("GameId").AsInt32().NotNullable().ForeignKey("Games", "Id");

				Insert.IntoTable("Collections")
					.Row(new { Id = 0, Title = "Hidden Abilities", Identifier = "abilities", Type = 0 })
					.Row(new { Id = 1, Title = "Egg Groups", Identifier = "eggGroups", Type = 0 })
					.Row(new { Id = 2, Title = "Berries", Identifier = "berries", Type = 0 })
					.Row(new { Id = 3, Title = "Dittos", Identifier = "dittos", Type = 0 })
					.Row(new { Id = 4, Title = "TMs", Identifier = "tms", Type = 0 });

				var collectionMap = Utilities.GetEntries("DexMigrator.Data.S013_GameCollectionMap.txt");
				MigrationTools.InputTable(this, "GameCollectionMap", collectionMap);
			}

			public override void Down()
			{
				throw new NotImplementedException();
			}
		}

		[FluentMigrator.Migration(201509040803)]
		public class M0008_AddTmCollectionSupport : FluentMigrator.Migration
		{
			public override void Up()
			{
				byte[] row = new byte[16];
				Alter.Table("Saves")
					.AddColumn("TmData").AsBinary(16).SetExistingRowsTo(row);

				Create.Table("Moves")
					.WithColumn("Id").AsInt32().PrimaryKey()
					.WithColumn("Name").AsString().Nullable();

				Create.Table("TmSets")
					.WithColumn("Id").AsInt32().PrimaryKey()
					.WithColumn("Identifier").AsString();

				Create.Table("Tms")
					.WithColumn("Id").AsInt32().PrimaryKey().Identity()
					.WithColumn("Index").AsInt32()
					.WithColumn("TMSetId").AsInt32().ForeignKey("TmSets", "Id")
					.WithColumn("MoveId").AsInt32().ForeignKey("Moves", "Id");

				Alter.Table("Games")
					.AddColumn("TmSetId").AsInt32().Nullable().ForeignKey("TmSets", "Id");

				Insert.IntoTable("TmSets")
					.Row(new { Id = 0, Identifier = "genv" })
					.Row(new { Id = 1, Identifier = "xy" })
					.Row(new { Id = 2, Identifier = "oras" });


				var pokemonMoves = Utilities.GetEntries("DexMigrator.Data.S014_PokemonMoves.txt");
				MigrationTools.InputTable(this, "Moves", pokemonMoves);
				var tms = Utilities.GetEntries("DexMigrator.Data.S015_Tms.txt");
				MigrationTools.InputTable(this, "Tms", tms);

				Update.Table("Games")
					.Set(new { TMSetId = 0 })
					.Where(new { GenerationId = 5 });
				Update.Table("Games")
					.Set(new { TMSetId = 1 })
					.Where(new { Id = 24 });
				Update.Table("Games")
					.Set(new { TMSetId = 1 })
					.Where(new { Id = 25 });
				Update.Table("Games")
					.Set(new { TMSetId = 2 })
					.Where(new { Id = 26 });
				Update.Table("Games")
					.Set(new { TMSetId = 2 })
					.Where(new { Id = 27 });

			}

			public override void Down()
			{
				throw new NotImplementedException();
			}
		}

		[FluentMigrator.Migration(201509070745)]
		public class M0009_AddMaintenanceSupport : FluentMigrator.Migration
		{
			public override void Up()
			{
				Create.Table("ServerSettings")
					.WithColumn("Id").AsInt32().PrimaryKey().Identity()
					.WithColumn("Config").AsString().NotNullable()
					.WithColumn("Boolean").AsBoolean().Nullable()
					.WithColumn("Integer").AsInt32().Nullable()
					.WithColumn("String").AsString().Nullable();

				Insert.IntoTable("ServerSettings")
					.Row(new { Config = "Maintenance", Boolean = false });
			}

			public override void Down()
			{
				throw new NotImplementedException();
			}
		}

		[FluentMigrator.Migration(201509091832)]
		public class M0010_ImproveTokensForEmailAuthAndPasswordResetting : FluentMigrator.Migration
		{
			public override void Up()
			{

				Alter.Table("Tokens")
					.AddColumn("TokenType").AsInt32().SetExistingRowsTo(0);
			}

			public override void Down()
			{
				throw new NotImplementedException();
			}
		}

		[FluentMigrator.Migration(201509182131)]
		public class M0011_AbilitySetBugFix : FluentMigrator.Migration
		{

			public override void Up()
			{
				Alter.Table("AbilitySets")
					.AddColumn("AbilityId").AsInt32().SetExistingRowsTo(0);
				Insert.IntoTable("ServerSettings")
					.Row(new { Config = "AbilityUpdateRequired", Boolean = true });
			}

			public override void Down()
			{
				throw new NotImplementedException();
			}
		}

		[FluentMigrator.Migration(201509201045)]
		public class M0012_NewServerSettings : FluentMigrator.Migration
		{
			public override void Up()
			{
				Insert.IntoTable("ServerSettings")
					.Row(new { Config = "ServerAddress", String = "http://dexcomplete.com" });
			}

			public override void Down()
			{
				throw new NotImplementedException();
			}

		}

		[FluentMigrator.Migration(201510013)]
		public class M0013_UpdatesAndComingSoon : FluentMigrator.Migration
		{
			public override void Up()
			{
				Create.Table("Updates")
					.WithColumn("Id").AsInt32().PrimaryKey().Identity()
					.WithColumn("Date").AsDateTime()
					.WithColumn("Text").AsString();

				Create.Table("ComingSoon")
					.WithColumn("Id").AsInt32().PrimaryKey().Identity()
					.WithColumn("Identifier").AsString()
					.WithColumn("Text").AsString();
				DateTime now = DateTime.Now;
				now = new DateTime(now.Year, now.Month, now.Day);
				Insert.IntoTable("Updates")
					.Row(new { Date = now, Text = "Added Updates and Coming Soon" })
					.Row(new { Date = now, Text = "Add home page instead of immediately going to login page." } )
					.Row(new { Date = now, Text = "Shuffled around the user page." })
					.Row(new { Date = now, Text = "Added FAQ section on main page." })
					;

				Insert.IntoTable("ComingSoon")
					.Row(new { Identifier = "CS0001", Text = "Ability to reset your passwords." })
					.Row(new { Identifier = "CS0002", Text = "Spinners to let you know when things are loading." })
					.Row(new { Identifier = "CS0003", Text = "Move FAQ section to its own page." })
					.Row(new { Identifier = "CS0004", Text = "Ability to set a description of yourself." })
					;
			}

			public override void Down()
			{
				throw new NotImplementedException();
			}
		}
	}
}

