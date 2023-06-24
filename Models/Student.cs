namespace Production.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        public List<Student> students { get; set; }
        [Key]
        public string Stu_ID { get; set; }

        [Required]
        public string Stu_Firstname { get; set; }

        public string Stu_Lastname { get; set; }

        [StringLength(50)]
        public string Stu_Gender_ID { get; set; }

        public string Stu_Address { get; set; }

        public string Stu_PhoneNo { get; set; }

        public int? Stu_Year_ID { get; set; }

        [StringLength(50)]
        public string Stu_Faculty_ID { get; set; }

        [StringLength(255)]
        public string Stu_Edit_By { get; set; }
        public string Faculty_Name { get; set; }
    }
}
