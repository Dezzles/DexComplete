namespace DexComplete.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pokedex
    {
        public Pokedex()
        {
            Entries = new HashSet<PokedexEntry>();
            Games = new HashSet<Game>();
        }

        public string PokedexId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

		public bool Regional { get; set; }

        public virtual ICollection<PokedexEntry> Entries { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
