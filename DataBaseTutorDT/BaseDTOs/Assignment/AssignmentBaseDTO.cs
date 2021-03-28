using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTutor.DTOs.BaseDTOs.Assignment
{
    public class AssignmentBaseDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
