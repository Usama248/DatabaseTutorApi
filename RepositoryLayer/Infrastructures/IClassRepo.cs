using DatabaseTutor.DTOs;
using DatabaseTutor.DTOs.RequestDTOs.Assignment;
using DatabaseTutor.DTOs.ResponseDTOs.Class;
using EntityLayer.DbContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Infrastructures
{
    public interface IClassRepo : IRepositoryBase<Class>
    {
        ResponseDTO<List<ListClassResponseDTO>> GetAllClasses();
        Task<ResponseDTO<AddEditClassResponseDTO>> AddClass(RequestDTO<AddEditClassRequestDTO> model);
        ResponseDTO<AddEditClassResponseDTO> GetClassById(int id);
        ResponseDTO<bool> DeleteClassById(int id);
        ResponseDTO<bool> UpdateClass(RequestDTO<AddEditClassRequestDTO> model);
        ResponseDTO<dynamic> GetClassAsLookup();
    }
}
