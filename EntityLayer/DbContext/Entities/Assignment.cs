using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DbContext.Entities
{
    [Table("Database_Tutor_Assignment")]
    public class Assignment : BaseEntity
    {
        [Key]
        [Column("dta_key")]
        public int Id { get; set; }

        [Required]
        [Column("dta_title", TypeName = "varchar(200)")]
        public string Title { get; set; }

        [Required]
        [Column("dta_class_id")]
        public int ClassId { get; set; }

        [Required]
        [Column("dta_file_location")]
        public string FileLocation { get; set; }

        [ForeignKey("ClassId")]
        public virtual StudentClass Classes { get; set; }

        public ICollection<AssignmentSolution> AssignmentSolutions { get; set; }
    }
}
