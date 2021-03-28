using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DbContext.Entities
{
    [Table("Database_Tutor_TeacherClass")]
    public class TeacherClass
    {
        [Key]
        [Column("dttc_key")]
        public int Id { get; set; }

        [Column("dttc_teacher_id")]
        public string TeacherId { get; set; }

        [Column("dttc_class_id")]
        public int ClassId { get; set; }

        [ForeignKey("TeacherId")]
        public virtual DatabaseTutorUser Teacher { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Classes { get; set; }
    }
}
