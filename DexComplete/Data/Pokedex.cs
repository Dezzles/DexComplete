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
            Entries = new HashSet<Entry>();
            Games = new HashSet<Game>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Identifier { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
