using DatabaseTutor.DTOs;
using DatabaseTutor.DTOs.RequestDTOs.StudentQuery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWork.UnitOfWork;

namespace DatabaseTutorApi.Controllers
{
    public class QueryBuilderController : BaseController
    {
        public QueryBuilderController(IDatabaseTutorUnitOfWork databaseTutorUnitOfWork) : base(databaseTutorUnitOfWork)
        {

        }

        [HttpGet]
        [Route("GetAllDataBases")]
        public IActionResult GetAllDataBases()
        {
            return Ok(_databaseTutorUOW.QueryBuilderRepo.GetDatabaseAsLookup());
        }

        [HttpGet]
        [Route("GetTablesAsLookup")]
        public IActionResult GetTablesAsLookup(string databaseName)
        {
            return Ok(_databaseTutorUOW.QueryBuilderRepo.GetTableAsLookup(databaseName));
        }

        [HttpGet]
        [Route("GetColumnsAsLookup")]
        public IActionResult GetColumnsAsLookup(string databaseName, string tableName)
            {
            return Ok(_databaseTutorUOW.QueryBuilderRepo.GetTableColumnsAsLookup(databaseName, tableName));
        }

        [HttpGet]
        [Route("GetStudentQuries")]
        public IActionResult GetStudentQuries()
        {
            return Ok(_databaseTutorUOW.QueryRepo.GetAllStudentQueries());
        }
        [HttpGet]
        [Route("GetStudentQueryById")]
        public IActionResult GetStudentQueryById(int id)
        {
            return Ok(_databaseTutorUOW.QueryRepo.GetStudentQueryById(id));
        }

        [HttpDelete]
        [Route("DeleteStudentQuery/{id}")]
        public IActionResult DeleteAssignment(int? id)
        {
            return Ok(_databaseTutorUOW.QueryRepo.DeleteStudentQueryById(id.Value));
        }

        [HttpPost]
        [Route("AddStudentQuery")]
        public async Task<IActionResult> AddStudentQuery(AddEditStudentQueryRequestDTO model)
        {
            return Ok(await _databaseTutorUOW.QueryRepo.AddStudentQuery(new RequestDTO<AddEditStudentQueryRequestDTO>() { Data = model }));
        }

        [HttpPut]
        [Route("UpdateStudentQuery/{id}")]
        public IActionResult UpdateStudentQuery(string id, AddEditStudentQueryRequestDTO model)
        {
            return Ok(_databaseTutorUOW.QueryRepo.UpdateStudentQuery(new RequestDTO<AddEditStudentQueryRequestDTO>() { Data = model }));
        }

        [HttpPost]
        [Route("ExecuteQuery")]
        public IActionResult ExecuteQuery(ExecuteQueryRequestDTO executeQueryRequestDTO)
        {
            return Ok(_databaseTutorUOW.QueryBuilderRepo.ExecuteQuery(executeQueryRequestDTO));
        }
    }
}
