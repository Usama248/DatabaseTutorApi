using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DbContext.Entities
{
    [Table("Database_Tutor_StudentClass")]
    public class StudentClass 
    {
        [Key]
        [Column("dtsc_key")]
        public int Id { get; set; }

        [Column("dtsc_student_id")]
        public string StudentId { get; set; }

        [Column("dtsc_class_id")]
        public int ClassId { get; set; }

        [ForeignKey("StudentId")]
        public virtual DatabaseTutorUser Student { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Classes { get; set; }



    }
}
