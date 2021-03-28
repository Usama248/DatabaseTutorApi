using DatabaseTutor.DTOs;
using DatabaseTutor.DTOs.RequestDtos.User;
using DatabaseTutor.DTOs.ResponseDTOs.User;
using EntityLayer.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Infrastructures
{
    public interface IUserRepo : IRepositoryBase<DatabaseTutorUser>
    {
        Task<ResponseDTO<LoginResponseDTO>> ProcessLogin(RequestDTO<LoginRequestDTO> model);
        Task<ResponseDTO<RegisterResponseDTO>> Register(RequestDTO<RegisterRequestDTO> model);
        Task<ResponseDTO<UserResponseDTO>> GetUserDetails();
    }
}
