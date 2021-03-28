using AutoMapper;
using CommonLayer.Helper;
using DatabaseTutor.DTOs;
using DatabaseTutor.DTOs.RequestDTOs.Assignment;
using DatabaseTutor.DTOs.ResponseDTOs.Assignment;
using EntityLayer.DbContext;
using EntityLayer.DbContext.Entities;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repos
{
    public class AssignmentRepo : RepositoryBase<Assignment>, IAssignmentRepo
    {
        private readonly IMapper _mapper;

        public AssignmentRepo(IServiceProvider serviceProvider, DatabaseTutorDbContext context) : base(context, serviceProvider)
        {
            _mapper = _serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<ResponseDTO<AddEditAssignmentResponseDTO>> AddAssignment(RequestDTO<AddEditAssignmentRequestDTO> model)
        {
            var entity = _mapper.Map<Assignment>(model.Data);
            await Post(
               entity, true);
            return Responses.OKAdded<AddEditAssignmentResponseDTO>("Assignment", null);
        }

        public ResponseDTO<bool> DeleteAssignmentById(int id)
        {
            SoftDelete(id, true);
            return Responses.OKDeleted("Assignment", true);
        }

        public ResponseDTO<List<ListAssignmentResponseDTO>> GetAllAssignments()
        {
            return Responses.OKGetAll("Assignments", Get().Where(x => x.IsDeleted == false).Select(x => new ListAssignmentResponseDTO()
            {
                Id = x.Id,
                Name = x.Title
            }).ToList());
        }

        public ResponseDTO<AddEditAssignmentResponseDTO> GetAssignmentById(int id)
        {
            var response = GetById(id);
            var entity = _mapper.Map<AddEditAssignmentResponseDTO>(response);
            return Responses.OK<AddEditAssignmentResponseDTO>("Assignment", entity);
        }

        public ResponseDTO<bool> UpdateAssignment(RequestDTO<AddEditAssignmentRequestDTO> model)
        {
            var entity = _mapper.Map<Assignment>(model.Data);
            Put(
              entity, true);
            return Responses.OKUpdated<bool>("Assignment", true);
        }

    }
}
