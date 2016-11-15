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
				.WithColumn("UserId").AsInt64().Identity().PrimaryKey()
				.WithColumn("Username").AsString()
				.WithColumn("Email").AsString()
				.WithColumn("Password").AsString()
				.WithColumn("Salt").AsString();

			Create.Table("Tokens")
				.WithColumn("TokenId").AsInt64().Identity().PrimaryKey()
				.WithColumn("Value").AsString()
				.WithColumn("ExpiryDate").AsDateTime()
				.WithColumn("UserId").AsInt64();

			Create.Table("Pokedexes")
				.WithColumn("PokedexId").AsString().PrimaryKey()
				.WithColumn("Title").AsString();

			Create.Table("Games")
				.WithColumn("GameId").AsString().PrimaryKey()
				.WithColumn("Title").AsString()
				.WithColumn("GenerationId").AsString()
				.WithColumn("TmSetId").AsString().Nullable();

			Create.Table("Generations")
				.WithColumn("GenerationId").AsString().PrimaryKey()
				.WithColumn("Title").AsString();

			Create.Table("Saves")
				.WithColumn("SaveId").AsInt64().Identity().PrimaryKey()
				.WithColumn("Code").AsBinary(256)
				.WithColumn("SaveName").AsString()
				.WithColumn("UserId").AsInt64()
				.WithColumn("GameId").AsString()
				.WithColumn("AbilityData").AsBinary(256);

			Create.Table("Pokemon")
				.WithColumn("PokemonId").AsString().PrimaryKey()
				.WithColumn("Index").AsInt32()
				.WithColumn("Name").AsString();

			Create.Table("PokedexEntries")
				.WithColumn("PokedexEntryId").AsInt32().Identity().PrimaryKey()
				.WithColumn("Index").AsInt32()
				.WithColumn("PokedexId").AsString()
				.WithColumn("PokemonId").AsString();

			Create.Table("GamePokedex")
				.WithColumn("GameId").AsString()
				.WithColumn("PokedexId").AsString();

			/*Create.PrimaryKey("PK_GamePokedex")
				.OnTable("GamePokedex").Columns(new string[] { "GameId", "PokedexId" });

			Create.ForeignKey("FK_UserToken")
				.FromTable("Tokens").ForeignColumn("UserId")
				.ToTable("Users").PrimaryColumn("UserId");

			Create.Index("IX_FK_UserToken")
				.OnTable("Tokens")
				.OnColumn("UserId");
			
			Create.ForeignKey("FK_GameGeneration")
				.FromTable("Games").ForeignColumn("GenerationId")
				.ToTable("Generations").PrimaryColumn("GenerationId");

			Create.Index("IX_FK_GameGeneration")
				.OnTable("Games")
				.OnColumn("GenerationId");

			Create.ForeignKey("FK_SaveUser")
				.FromTable("Saves").ForeignColumn("UserId")
				.ToTable("Users").PrimaryColumn("UserId");

			Create.Index("IX_FK_SaveUser")
				.OnTable("Saves")
				.OnColumn("UserId");

			Create.ForeignKey("FK_SaveGame")
				.FromTable("Saves").ForeignColumn("GameId")
				.ToTable("Games").PrimaryColumn("GameId");

			Create.Index("IX_FK_SaveGame")
				.OnTable("Saves")
				.OnColumn("GameId");

			Create.ForeignKey("FK_GamePokedex")
				.FromTable("GamePokedex").ForeignColumn("GameId")
				.ToTable("Games").PrimaryColumn("GameId");

			Create.ForeignKey("FK_GamePokedex_Game")
				.FromTable("GamePokedex").ForeignColumn("PokedexId")
				.ToTable("Pokedexes").PrimaryColumn("PokedexId");

			Create.Index("IX_FK_GamePokedex_Pokedex")
				.OnTable("GamePokedex")
				.OnColumn("PokedexId");

			Create.ForeignKey("FK_PokedexEntry")
				.FromTable("PokedexEntries").ForeignColumn("PokedexId")
				.ToTable("Pokedexes").PrimaryColumn("PokedexId");

			Create.Index("IX_FK_PokedexEntry")
				.OnTable("PokedexEntries")
				.OnColumn("PokedexId");

			Create.ForeignKey("FK_EntryPokemon")
				.FromTable("PokedexEntries").ForeignColumn("PokemonId")
				.ToTable("Pokemon").PrimaryColumn("PokemonId");

			Create.Index("IX_FK_EntryPokemon")
				.OnTable("PokedexEntries")
				.OnColumn("PokemonId");

			Console.WriteLine("Indexes Created"); /**/
		}

		public override void Down()
		{
			throw new NotImplementedException();
		}

	}

	[FluentMigrator.Migration(201508200001)]
	public class M0002_AddGenerations : FluentMigrator.Migration
	{
		public override void Up()
		{
			List<Dictionary<string, object>> generation = Utilities.GetEntries("DexMigrator.Data.S001_Generations.txt");
			MigrationTools.InputTable(this, "Generations", generation);
		}
		public override void Down(){}
	}
	[FluentMigrator.Migration(201508200002)]
	public class M0002_AddGames : FluentMigrator.Migration
	{
		public override void Up()
		{
			List<Dictionary<string, object>> games = Utilities.GetEntries("DexMigrator.Data.S002_Games.txt");
			MigrationTools.InputTable(this, "Games", games);
		}
		public override void Down() { }
	}
	[FluentMigrator.Migration(201508200003)]
	public class M0002_AddPokedexes : FluentMigrator.Migration
	{
		public override void Up()
		{
			List<Dictionary<string, object>> pokedexes = Utilities.GetEntries("DexMigrator.Data.S003_Pokedexes.txt");
			MigrationTools.InputTable(this, "Pokedexes", pokedexes);
		}
		public override void Down() { }
	}
	[FluentMigrator.Migration(201508200004)]
	public class M0002_AddGamePokedex : FluentMigrator.Migration
	{
		public override void Up()
		{
			List<Dictionary<string, object>> pokedexgame = Utilities.GetEntries("DexMigrator.Data.S004_PokedexGame.txt");
			MigrationTools.InputTable(this, "GamePokedex", pokedexgame);
		}
		public override void Down() { }
	}
	[FluentMigrator.Migration(201508200005)]
	public class M0002_AddPokemon : FluentMigrator.Migration
	{
		public override void Up()
		{
			List<Dictionary<string, object>> pokemon = Utilities.GetEntries("DexMigrator.Data.S005_Pokemon.txt");
			MigrationTools.InputTable(this, "Pokemon", pokemon);
		}
		public override void Down() { }
	}
	[FluentMigrator.Migration(201508200006)]
	public class M0002_AddPokedexEntries_A : FluentMigrator.Migration
	{
		public override void Up()
		{
			List<Dictionary<string, object>> pokedexentries = Utilities.GetEntries("DexMigrator.Data.S006_PokedexEntries_A.txt");
			MigrationTools.InputTable(this, "PokedexEntries", pokedexentries);
		}
		public override void Down() { }
	}
	[FluentMigrator.Migration(201508200007)]
	public class M0002_AddPokedexEntries_B : FluentMigrator.Migration
	{
		public override void Up()
		{
			List<Dictionary<string, object>> pokedexentries = Utilities.GetEntries("DexMigrator.Data.S006_PokedexEntries_B.txt");
			MigrationTools.InputTable(this, "PokedexEntries", pokedexentries);
		}
		public override void Down() { }
	}
	[FluentMigrator.Migration(201508200008)]
	public class M0002_AddPokedexEntries_C : FluentMigrator.Migration
	{
		public override void Up()
		{
			List<Dictionary<string, object>> pokedexentries = Utilities.GetEntries("DexMigrator.Data.S006_PokedexEntries_C.txt");
			MigrationTools.InputTable(this, "PokedexEntries", pokedexentries);
		}
		public override void Down() { }
	}
	[FluentMigrator.Migration(201508200009)]
	public class M0002_AddPokedexEntries_D : FluentMigrator.Migration
	{
		public override void Up()
		{
			List<Dictionary<string, object>> pokedexentries = Utilities.GetEntries("DexMigrator.Data.S006_PokedexEntries_D.txt");
			MigrationTools.InputTable(this, "PokedexEntries", pokedexentries);
		}
		public override void Down() { }
	}
	[FluentMigrator.Migration(201508200010)]
	public class M0002_AddPokedexEntries_E : FluentMigrator.Migration
	{
		public override void Up()
		{
			List<Dictionary<string, object>> pokedexentries = Utilities.GetEntries("DexMigrator.Data.S006_PokedexEntries_E.txt");
			MigrationTools.InputTable(this, "PokedexEntries", pokedexentries);
		}
		public override void Down() { }
	}
	[FluentMigrator.Migration(201508200011)]
	public class M0002_AddPokedexEntries_F : FluentMigrator.Migration
	{
		public override void Up()
		{
			List<Dictionary<string, object>> pokedexentries = Utilities.GetEntries("DexMigrator.Data.S006_PokedexEntries_F.txt");
			MigrationTools.InputTable(this, "PokedexEntries", pokedexentries);
		}
		public override void Down() { }
	}
	[FluentMigrator.Migration(201508200012)]
	public class M0002_AddPokedexEntries_G : FluentMigrator.Migration
	{
		public override void Up()
		{
			List<Dictionary<string, object>> pokedexentries = Utilities.GetEntries("DexMigrator.Data.S006_PokedexEntries_G.txt");
			MigrationTools.InputTable(this, "PokedexEntries", pokedexentries);
		}
		public override void Down() { }
	}
	[FluentMigrator.Migration(201509021714)]
	public class M0003_AddAbilities : FluentMigrator.Migration
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
				.WithColumn("AbilityId").AsString().PrimaryKey()
				.WithColumn("Name").AsString();

			Create.Table("AbilitySets")
				.WithColumn("PokemonId").AsString()
				.WithColumn("GenerationId").AsString().ForeignKey("Generations", "GenerationId");

			Create.PrimaryKey("PK_AbilitySet")
				.OnTable("AbilitySets").Columns(new string[] { "PokemonId", "GenerationId" });

			Create.Table("AbilityEntries")
				.WithColumn("Id").AsInt32().PrimaryKey().Identity()
				.WithColumn("PokemonId").AsString().NotNullable().ForeignKey("Pokemon", "PokemonId")
				.WithColumn("Index").AsInt32().NotNullable()
				.WithColumn("Note").AsString()
				.WithColumn("AbilityId").AsString().NotNullable().ForeignKey("Abilities", "AbilityId")
				.WithColumn("AbilitySetId").AsString().NotNullable()
				.WithColumn("GenerationId").AsString().NotNullable();


			MigrationTools.InputTable(this, "Abilities", abilities);
			MigrationTools.InputTable(this, "AbilitySets", abilitysets);
			MigrationTools.InputTable(this, "AbilityEntries", entries);
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
					.WithColumn("EggGroupId").AsString().PrimaryKey()
					.WithColumn("Name").AsString()
					.WithColumn("Index").AsInt32();

				Create.Table("Berries")
					.WithColumn("Index").AsInt32()
					.WithColumn("BerryId").AsString().PrimaryKey()
					.WithColumn("Name").AsString();

				Create.Table("BerryMaps")
					.WithColumn("BerryId").AsString()
					.WithColumn("Index").AsInt32()
					.WithColumn("GenerationId").AsString().ForeignKey("Generations", "GenerationId");

				Create.PrimaryKey("PK_BerryMaps")
					.OnTable("BerryMaps").Columns(new string[] { "BerryId", "GenerationId" });
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
					.WithColumn("Title").AsString()
					.WithColumn("CollectionId").AsString().PrimaryKey()
					.WithColumn("Type").AsInt32();

				Create.Table("GameCollectionMap")
					.WithColumn("Id").AsInt32().PrimaryKey().Identity()
					.WithColumn("CollectionId").AsString().NotNullable().ForeignKey("Collections", "CollectionId")
					.WithColumn("GameId").AsString().NotNullable().ForeignKey("Games", "GameId");

				var collectionTypes = Utilities.GetEntries("DexMigrator.Data.S016_GameCollections.txt");
				MigrationTools.InputTable(this, "Collections", collectionTypes);

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
					.WithColumn("MoveId").AsString().PrimaryKey()
					.WithColumn("Name").AsString().Nullable();

				Create.Table("Tms")
					.WithColumn("Id").AsInt32().PrimaryKey().Identity()
					.WithColumn("Index").AsInt32()
					.WithColumn("TMSetId").AsString()
					.WithColumn("MoveId").AsString().ForeignKey("Moves", "MoveId");

				var pokemonMoves = Utilities.GetEntries("DexMigrator.Data.S014_PokemonMoves.txt");
				MigrationTools.InputTable(this, "Moves", pokemonMoves);
				var tms = Utilities.GetEntries("DexMigrator.Data.S015_Tms.txt");
				MigrationTools.InputTable(this, "Tms", tms);

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

		[FluentMigrator.Migration(201510011313)]
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

