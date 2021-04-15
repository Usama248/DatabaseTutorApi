using DatabaseTutor.DTOs;
using DatabaseTutor.DTOs.RequestDTOs.StudentQuery;
using DatabaseTutor.DTOs.ResponseDTOs.StudentQuery;
using EntityLayer.DbContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Infrastructures
{
        public interface IQueryRepo : IRepositoryBase<StudentQuery>
        {
            ResponseDTO<List<ListStudentQueryResponseDTO>> GetAllStudentQueries();
            Task<ResponseDTO<AddEditStudentQueryResponseDTO>> AddStudentQuery(RequestDTO<AddEditStudentQueryRequestDTO> model);
            ResponseDTO<AddEditStudentQueryResponseDTO> GetStudentQueryById(int id);
            ResponseDTO<bool> DeleteStudentQueryById(int id);
            ResponseDTO<bool> UpdateStudentQuery(RequestDTO<AddEditStudentQueryRequestDTO> model);
        }
}
