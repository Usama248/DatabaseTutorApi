using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DbContext.Entities
{
    [Table("Database_Tutor_Class")]
    public class Class : BaseEntity
    {
        [Key]
        [Column("dtc_key")]
        public int Id { get; set; }

        [Required]
        [Column("dtc_name", TypeName = "varchar(200)")]
        public string Name { get; set; }
    }
}
