using DatabaseTutor.DTOs.BaseDTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTutor.DTOs.ResponseDTOs.User
{
    public class LoginResponseDTO : LoginBaseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public UserResponseDTO AccountDetails { get; set; }
    }
}
