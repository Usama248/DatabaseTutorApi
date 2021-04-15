using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTutor.DTOs.BaseDTOs.StudentQuery
{
    public class StudentQueryBaseDTO : BaseDTO
    {
        public int? Id { get; set; }
        public string QueryName { get; set; }
        public string Query { get; set; }
        public string Database { get; set; }
    }
}
