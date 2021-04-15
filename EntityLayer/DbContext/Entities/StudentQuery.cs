using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DbContext.Entities
{
    [Table("Database_Tutor_Student_Query")]
    public class StudentQuery : BaseEntity
    {
        [Key]
        [Column("dtsq_key")]
        public int Id { get; set; }

        [Column("dtsq_student_id")]
        public string StudentId { get; set; }

        [Column("dtsq_database")]
        public string Database { get; set; }

        [Column("dtsq_query_name")]
        public string QueryName { get; set; }

        [Required]
        [Column("dtsq_query")]
        public string Query { get; set; }

        [ForeignKey("StudentId")]
        public virtual DatabaseTutorUser User { get; set; }

    }
}
