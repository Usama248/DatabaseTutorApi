using AutoMapper;
using DatabaseTutor.DTOs.RequestDTOs.Assignment;
using DatabaseTutor.DTOs.RequestDTOs.StudentQuery;
using DatabaseTutor.DTOs.ResponseDTOs.Assignment;
using DatabaseTutor.DTOs.ResponseDTOs.Class;
using DatabaseTutor.DTOs.ResponseDTOs.StudentQuery;
using DatabaseTutor.DTOs.ResponseDTOs.User;
using EntityLayer.DbContext;
using EntityLayer.DbContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Extended User --> UserResponseDTO
            CreateMap<DatabaseTutorUser, UserResponseDTO>().
            ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName)).
            ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            //Add Edit Assignment --> DatabaseTutor Assignment
            CreateMap<AddEditAssignmentResponseDTO, Assignment>();
            CreateMap<Assignment, AddEditAssignmentResponseDTO>();
            CreateMap<AddEditAssignmentRequestDTO, Assignment>();

            //Add Edit Class --> DatabaseTutor Class
            CreateMap<AddEditClassResponseDTO, Class>();
            CreateMap<Class, AddEditAssignmentResponseDTO>();
            CreateMap<AddEditClassRequestDTO, Class>();

            //Add Edit Class --> DatabaseTutor Class
            CreateMap<AddEditStudentQueryResponseDTO, StudentQuery>();
            CreateMap<StudentQuery, AddEditAssignmentResponseDTO>();
            CreateMap<AddEditStudentQueryRequestDTO, StudentQuery>();
            
        }
    }
}
