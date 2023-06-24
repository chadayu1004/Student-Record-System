namespace Production.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Faculty")]
    public partial class Faculty
    {
        [Key]
        [StringLength(50)]
        public string Faculty_ID { get; set; }

        [Required]
        public string Faculty_Name { get; set; }

        [Required]
        public string Faculty_Location { get; set; }
    }
}
