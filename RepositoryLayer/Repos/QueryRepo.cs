using AutoMapper;
using CommonLayer.Helper;
using DatabaseTutor.DTOs;
using DatabaseTutor.DTOs.RequestDTOs.StudentQuery;
using DatabaseTutor.DTOs.ResponseDTOs.StudentQuery;
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
        public class QueryRepo : RepositoryBase<StudentQuery>, IQueryRepo
    {
            private readonly IMapper _mapper;

            public QueryRepo(IServiceProvider serviceProvider, DatabaseTutorDbContext context) : base(context, serviceProvider)
            {
                _mapper = _serviceProvider.GetRequiredService<IMapper>();
            }

            public async Task<ResponseDTO<AddEditStudentQueryResponseDTO>> AddStudentQuery(RequestDTO<AddEditStudentQueryRequestDTO> model)
            {
                var entity = _mapper.Map<StudentQuery>(model.Data);
                await Post(
                   entity, true);
                return Responses.OKAdded<AddEditStudentQueryResponseDTO>("Student Query", null);
            }

            public ResponseDTO<bool> DeleteStudentQueryById(int id)
            {
                SoftDelete(id, true);
                return Responses.OKDeleted("Student Query", true);
            }

            public ResponseDTO<List<ListStudentQueryResponseDTO>> GetAllStudentQueries()
            {
                return Responses.OKGetAll("Student Queries", Get().Where(x => x.IsDeleted == false).Select(x => new ListStudentQueryResponseDTO()
                {
                    Id = x.Id,
                    QueryName = x.QueryName,
                    Query= x.Query,
                    Database= x.Database
                }).ToList());
            }

            public ResponseDTO<AddEditStudentQueryResponseDTO> GetStudentQueryById(int id)
            {
                var response = GetById(id);
                var entity = _mapper.Map<AddEditStudentQueryResponseDTO>(response);
                return Responses.OK<AddEditStudentQueryResponseDTO>("Student Query", entity);
            }

            public ResponseDTO<bool> UpdateStudentQuery(RequestDTO<AddEditStudentQueryRequestDTO> model)
            {
                var entity = _mapper.Map<StudentQuery>(model.Data);
                Put(
                  entity, true);
                return Responses.OKUpdated<bool>("Student Query", true);
            }

        }
}
