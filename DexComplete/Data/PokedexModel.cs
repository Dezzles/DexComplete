namespace DexComplete.Data
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class PokedexModel : DbContext
	{
		public PokedexModel()
			: base("name=PokedexModel")
		{
			Database.SetInitializer<PokedexModel>(null);
		}

		public virtual DbSet<Entry> Entries { get; set; }
		public virtual DbSet<Game> Games { get; set; }
		public virtual DbSet<Generation> Generations { get; set; }
		public virtual DbSet<Pokedex> Pokedexes { get; set; }
		public virtual DbSet<Pokemon> Pokemons { get; set; }
		public virtual DbSet<Save> Saves { get; set; }
		public virtual DbSet<Token> Tokens { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Ability> Abilities { get; set; }
		public virtual DbSet<AbilityEntry> AbilityEntries { get; set; }
		public virtual DbSet<AbilitySet> AbilitySets { get; set; }
		public virtual DbSet<Berry> Berries { get; set; }
		public virtual DbSet<BerryMap> BerryMap { get; set; }
		public virtual DbSet<EggGroups> EggGroups { get; set; }
		public virtual DbSet<Move> Moves { get; set; }
		public virtual DbSet<TM> TMs { get; set; }
		public virtual DbSet<TMSet> TMSet { get; set; }
		public virtual DbSet<ServerSetting> ServerSettings { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Game>()
				.HasMany(e => e.Saves)
				.WithRequired(e => e.Game)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Game>()
				.HasMany(e => e.Pokedexes)
				.WithMany(e => e.Games)
				.Map(m => m.ToTable("GamePokedex").MapLeftKey("GameId").MapRightKey("PokedexId"));

			modelBuilder.Entity<Generation>()
				.HasMany(e => e.Games)
				.WithRequired(e => e.Generation)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Pokedex>()
				.HasMany(e => e.Entries)
				.WithRequired(e => e.Pokedex)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Pokemon>()
				.HasMany(e => e.Entries)
				.WithRequired(e => e.Pokemon)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Saves)
				.WithRequired(e => e.User)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Tokens)
				.WithRequired(e => e.User)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<AbilityEntry>()
				.HasRequired<Ability>(e => e.Ability)
				.WithMany(f => f.Entries)
				.HasForeignKey(e => e.AbilityId);

			modelBuilder.Entity<Ability>()
				.HasMany<AbilityEntry>(e => e.Entries)
				.WithRequired(v => v.Ability)
				.HasForeignKey(v => v.AbilityId);

			modelBuilder.Entity<AbilityEntry>()
				.HasRequired<Pokemon>(e => e.Pokemon)
				.WithMany(f => f.AbilityEntries)
				.HasForeignKey(e => e.PokemonId);

			modelBuilder.Entity<AbilityEntry>()
				.HasRequired<AbilitySet>(e => e.Set)
				.WithMany(f => f.Entries)
				.HasForeignKey(e => e.AbilitySetId);

			modelBuilder.Entity<AbilitySet>()
				.HasRequired<Generation>(e => e.Generation)
				.WithMany(v => v.AbilitySets)
				.HasForeignKey(e => e.GenerationId);

			modelBuilder.Entity<BerryMap>()
				.HasRequired<Berry>(e => e.Berry)
				.WithMany(v => v.BerryMaps)
				.HasForeignKey(e => e.BerryId);

			modelBuilder.Entity<BerryMap>()
				.HasRequired<Generation>(e => e.Generation)
				.WithMany(v => v.Berries)
				.HasForeignKey(e => e.GenerationId);

			modelBuilder.Entity<Game>()
				.HasMany<Collection>(e => e.Collections)
				.WithMany(u => u.Games)
				.Map(m => m.ToTable("GameCollectionMap").MapLeftKey("GameId").MapRightKey("CollectionId"));

			modelBuilder.Entity<Game>()
				.HasOptional<TMSet>(e => e.TMSet)
				.WithMany(u => u.Games)
				.HasForeignKey(v => v.TMSetId);

			modelBuilder.Entity<TMSet>()
				.HasMany(e => e.TMs)
				.WithRequired(u => u.TMSet)
				.HasForeignKey(u => u.TmSetId);

			modelBuilder.Entity<TM>()
				.HasRequired<Move>(e => e.Move)
				.WithMany(u => u.Tms)
				.HasForeignKey(e => e.MoveId);
		}
	}
}
