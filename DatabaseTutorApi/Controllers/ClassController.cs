using DatabaseTutor.DTOs;
using DatabaseTutor.DTOs.RequestDTOs.Assignment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWork.UnitOfWork;

namespace DatabaseTutorApi.Controllers
{
        public class ClassController : BaseController
        {
            public ClassController(IDatabaseTutorUnitOfWork databaseTutorUnitOfWork) : base(databaseTutorUnitOfWork)
            {

            }
            [HttpGet]
            [Route("GetClasses")]
            public IActionResult GetBrands()
            {
                return Ok(_databaseTutorUOW.ClassRepo.GetAllClasses());
            }
            [HttpGet]
            [Route("GetClassById")]
            public IActionResult GetBrandById(int id)
            {
                return Ok(_databaseTutorUOW.ClassRepo.GetClassById(id));
            }

            [HttpDelete]
            [Route("DeleteClass/{id}")]
            public IActionResult DeleteBrand(int? id)
            {
                return Ok(_databaseTutorUOW.ClassRepo.DeleteClassById(id.Value));
            }

            [HttpPost]
            [Route("AddClass")]
            public async Task<IActionResult> AddBrand(AddEditClassRequestDTO model)
            {
                return Ok(await _databaseTutorUOW.ClassRepo.AddClass(new RequestDTO<AddEditClassRequestDTO>() { Data = model }));
            }

            [HttpPut]
            [Route("UpdateClass/{id}")]
            public IActionResult UpdateBrand(string id, AddEditClassRequestDTO model)
            {
                return Ok(_databaseTutorUOW.ClassRepo.UpdateClass(new RequestDTO<AddEditClassRequestDTO>() { Data = model }));
            }

            [HttpGet]
            [Route("GetClassesAsLookup")]
            public IActionResult GetBrandsAsLookup()
            {
                return Ok(_databaseTutorUOW.ClassRepo.GetClassAsLookup());
            }
        }
}
