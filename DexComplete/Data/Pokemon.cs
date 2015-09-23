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
            Entries = new HashSet<Entry>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }
		public virtual ICollection<AbilityEntry> AbilityEntries { get; set; }
    }
}
