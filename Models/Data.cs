using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Production.Models
{
    public partial class Data : DbContext
    {
        public Data()
            : base("name=Data1")
        {
        }

        public virtual DbSet<AdminType> AdminTypes { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentGender> StudentGenders { get; set; }
        public virtual DbSet<StudentYear> StudentYears { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminType>()
                .Property(e => e.UserlevelID)
                .IsUnicode(false);

            modelBuilder.Entity<AdminType>()
                .Property(e => e.UserlevelID_Desp)
                .IsUnicode(false);

            modelBuilder.Entity<Faculty>()
                .Property(e => e.Faculty_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Faculty>()
                .Property(e => e.Faculty_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Faculty>()
                .Property(e => e.Faculty_Location)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Stu_Firstname)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Stu_Lastname)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Stu_Gender_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Stu_Address)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Stu_PhoneNo)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Stu_Faculty_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Stu_Edit_By)
                .IsUnicode(false);

            modelBuilder.Entity<StudentGender>()
                .Property(e => e.Stu_Gender_ID)
                .IsUnicode(false);

            modelBuilder.Entity<StudentGender>()
                .Property(e => e.Stu_Gender_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<StudentYear>()
                .Property(e => e.Stu_Year_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<UserLogin>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<UserLogin>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UserLogin>()
                .Property(e => e.Firstname)
                .IsUnicode(false);

            modelBuilder.Entity<UserLogin>()
                .Property(e => e.Lastname)
                .IsUnicode(false);

            modelBuilder.Entity<UserLogin>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<UserLogin>()
                .Property(e => e.UserlevelID)
                .IsUnicode(false);
        }
    }
}
