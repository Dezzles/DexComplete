namespace DexComplete.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pokemon")]
    public partial class Pokemon
    {
        public Pokemon()
        {
            Entries = new HashSet<PokedexEntry>();
        }

		public string PokemonId { get; set; }

        public int Index { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public virtual ICollection<PokedexEntry> Entries { get; set; }
		public virtual ICollection<AbilityEntry> AbilityEntries { get; set; }
    }
}
