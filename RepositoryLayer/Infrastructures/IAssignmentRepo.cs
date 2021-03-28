using DatabaseTutor.DTOs;
using DatabaseTutor.DTOs.RequestDTOs.Assignment;
using DatabaseTutor.DTOs.ResponseDTOs.Assignment;
using EntityLayer.DbContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Infrastructures
{
        public interface IAssignmentRepo : IRepositoryBase<Assignment>
        {
            ResponseDTO<List<ListAssignmentResponseDTO>> GetAllAssignments();
            Task<ResponseDTO<AddEditAssignmentResponseDTO>> AddAssignment(RequestDTO<AddEditAssignmentRequestDTO> model);
            ResponseDTO<AddEditAssignmentResponseDTO> GetAssignmentById(int id);
            ResponseDTO<bool> DeleteAssignmentById(int id);
            ResponseDTO<bool> UpdateAssignment(RequestDTO<AddEditAssignmentRequestDTO> model);
        }
}
