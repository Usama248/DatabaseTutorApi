using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTutor.DTOs.RequestDTOs.StudentQuery
{
    public class ExecuteQueryRequestDTO
    {
        public string Database { get; set; }
        public string Query { get; set; }
    }
}
