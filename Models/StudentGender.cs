namespace Production.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StudentGender")]
    public partial class StudentGender
    {
        [Key]
        [StringLength(50)]
        public string Stu_Gender_ID { get; set; }

        [StringLength(50)]
        public string Stu_Gender_Desc { get; set; }
    }
}
