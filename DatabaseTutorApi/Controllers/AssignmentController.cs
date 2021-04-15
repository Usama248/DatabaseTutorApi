using DatabaseTutor.DTOs;
using DatabaseTutor.DTOs.RequestDTOs.Assignment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWork.UnitOfWork;

namespace DatabaseTutorApi.Controllers
{
    public class AssignmentController : BaseController
    {
        public AssignmentController(IDatabaseTutorUnitOfWork databaseTutorUnitOfWork) : base(databaseTutorUnitOfWork)
        {

        }
        [HttpGet]
        [Route("GetAssignments")]
        public IActionResult GetAssignments()
        {
            return Ok(_databaseTutorUOW.AssignmentRepo.GetAllAssignments());
        }
        [HttpGet]
        [Route("GetAssignmentById")]
        public IActionResult GetAssignmentById(int id)
        {
            return Ok(_databaseTutorUOW.AssignmentRepo.GetAssignmentById(id));
        }

        [HttpDelete]
        [Route("DeleteAssignment/{id}")]
        public IActionResult DeleteAssignment(int? id)
        {
            return Ok(_databaseTutorUOW.AssignmentRepo.DeleteAssignmentById(id.Value));
        }

        [HttpPost]
        [Route("AddAssignment")]
        public async Task<IActionResult> AddAssignment(AddEditAssignmentRequestDTO model)
        {
            return Ok(await _databaseTutorUOW.AssignmentRepo.AddAssignment(new RequestDTO<AddEditAssignmentRequestDTO>() { Data = model }));
        }

        [HttpPut]
        [Route("UpdateAssignment/{id}")]
        public IActionResult UpdateAssignment(string id, AddEditAssignmentRequestDTO model)
        {
            return Ok(_databaseTutorUOW.AssignmentRepo.UpdateAssignment(new RequestDTO<AddEditAssignmentRequestDTO>() { Data = model }));
        }
    }
}
