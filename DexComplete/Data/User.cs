namespace DexComplete.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public User()
        {
            Saves = new HashSet<Save>();
            Tokens = new HashSet<Token>();
        }

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        public string Salt { get; set; }

        public virtual ICollection<Save> Saves { get; set; }

        public virtual ICollection<Token> Tokens { get; set; }
    }
}
