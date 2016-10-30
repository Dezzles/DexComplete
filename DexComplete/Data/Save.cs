namespace DexComplete.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Save
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SaveId { get; set; }

        [Required]
        public byte[] Code { get; set; }
		public byte[] AbilityData { get; set; }
		public byte[] DittoData { get; set; }
		public byte[] BerryData { get; set; }
		public byte[] EggGroupData { get; set; }
		public byte[] TMData { get; set; }
		public string SaveName { get; set; }

        public long UserId { get; set; }

        public string GameId { get; set; }

        public virtual Game Game { get; set; }

        public virtual User User { get; set; }
    }
}
