using DatabaseTutor.DTOs.BaseDTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTutor.DTOs.ResponseDTOs.User
{
   public class UserResponseDTO : UserBaseDTO
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
