namespace DexComplete.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Game
    {
        public Game()
        {
            Saves = new HashSet<Save>();
            Pokedexes = new HashSet<Pokedex>();
        }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public string GenerationId { get; set; }

		[Required]
        [StringLength(255)]
        public string GameId { get; set; }

		public string TMSetId { get; set; }

        public virtual Generation Generation { get; set; }

        public virtual ICollection<Save> Saves { get; set; }

        public virtual ICollection<Pokedex> Pokedexes { get; set; }

		public virtual ICollection<Collection> Collections { get; set; }

		//public virtual TMSet TMSet { get; set; }
    }
}
