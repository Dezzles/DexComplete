namespace DexComplete.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Token
    {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Value { get; set; }

        public DateTime ExpiryDate { get; set; }

        public long UserId { get; set; }

		public TokenType TokenType { get; set; }

        public virtual User User { get; set; }
    }
}
