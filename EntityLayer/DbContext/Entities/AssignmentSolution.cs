using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DbContext.Entities
{
    [Table("Database_Tutor_Assignment_Solution")]
    public class AssignmentSolution : BaseEntity
    {
        [Key]
        [Column("dtas_key")]
        public int Id { get; set; }

        [Column("dtas_user_id")]
        public string StudentId { get; set; }

        [Column("dtas_assignment_id")]
        public int AssignmentId { get; set; }

        [Required]
        [Column("dtas_file_location")]
        public string FileLocation { get; set; }

        [ForeignKey("AssignmentId")]
        public virtual Assignment Assignment { get; set; }

        [ForeignKey("StudentId")]
        public virtual DatabaseTutorUser User { get; set; }

    }
}
