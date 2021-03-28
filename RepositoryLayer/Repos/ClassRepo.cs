using AutoMapper;
using CommonLayer.Helper;
using DatabaseTutor.DTOs;
using DatabaseTutor.DTOs.RequestDTOs.Assignment;
using DatabaseTutor.DTOs.ResponseDTOs.Class;
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
    public class ClassRepo : RepositoryBase<Class>, IClassRepo
    {
        private readonly IMapper _mapper;

        public ClassRepo(IServiceProvider serviceProvider, DatabaseTutorDbContext context) : base(context, serviceProvider)
        {
            _mapper = _serviceProvider.GetRequiredService<IMapper>();
        }

        public async Task<ResponseDTO<AddEditClassResponseDTO>> AddClass(RequestDTO<AddEditClassRequestDTO> model)
        {
            var entity = _mapper.Map<Class>(model.Data);
            await Post(
               entity, true);
            return Responses.OKAdded<AddEditClassResponseDTO>("Class", null);
        }

        public ResponseDTO<bool> DeleteClassById(int id)
        {
            SoftDelete(id, true);
            return Responses.OKDeleted("Assignment", true);
        }

        public ResponseDTO<List<ListClassResponseDTO>> GetAllClasses()
        {
            return Responses.OKGetAll("Class", Get().Where(x => x.IsDeleted == false).Select(x => new ListClassResponseDTO()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList());
        }

        public ResponseDTO<AddEditClassResponseDTO> GetClassById(int id)
        {
            var response = GetById(id);
            var entity = _mapper.Map<AddEditClassResponseDTO>(response);
            return Responses.OK("AddClass", entity);
        }

        public ResponseDTO<bool> UpdateClass(RequestDTO<AddEditClassRequestDTO> model)
        {
            var entity = _mapper.Map<Class>(model.Data);
            Put(
              entity, true);
            return Responses.OKUpdated<bool>("Assignment", true);
        }
        public ResponseDTO<dynamic> GetClassAsLookup()
        {
            return Responses.OKGetAll<dynamic>("Class", Get().Select(p => new
            {
                key = p.Id,
                value = p.Name
            }));
        }

    }
}
