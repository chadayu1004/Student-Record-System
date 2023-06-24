namespace Production.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminType")]
    public partial class AdminType
    {
        [Key]
        [StringLength(50)]
        public string UserlevelID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserlevelID_Desp { get; set; }
    }
}
