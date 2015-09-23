namespace DexComplete.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Generation
    {
        public Generation()
        {
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

        public virtual ICollection<Game> Games { get; set; }

		public virtual ICollection<AbilitySet> AbilitySets { get; set; }
		public virtual ICollection<BerryMap> Berries { get; set; }
    }
}
