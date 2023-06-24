namespace Production.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserLogin")]
    public partial class UserLogin
    {
        [Key]
        [StringLength(255)]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "Username must contain only alphanumeric characters.")]
        public string Username { get; set; }

        [StringLength(255)]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "Password must contain only alphanumeric characters.")]
        public string Password { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Position { get; set; }

        public string UserlevelID { get; set; }
    }
}
