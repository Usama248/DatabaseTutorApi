using DatabaseTutor.DTOs;
using DatabaseTutor.DTOs.RequestDtos.User;
using DatabaseTutor.DTOs.RequestDTOs.StudentQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWork.UnitOfWork;

namespace DatabaseTutorApi.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(IDatabaseTutorUnitOfWork databaseTutorUnitOfWork) : base(databaseTutorUnitOfWork)
        {

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("PortalUserRegister")]
        public async Task<IActionResult> PortalUserRegister([FromBody] RegisterRequestDTO model)
        {
            return Ok(await _databaseTutorUOW.UserRepository.Register(new RequestDTO<RegisterRequestDTO>() { Data = model }));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("PortalLogin")]
        public async Task<IActionResult> PortalLogin([FromBody] LoginRequestDTO model)
        {
            return Ok(await _databaseTutorUOW.UserRepository.ProcessLogin(new RequestDTO<LoginRequestDTO>() { Data = model }));
        }

        [HttpGet("CurrentPortalUserDetails")]
        public async Task<IActionResult> CurrentPortalUserDetails()
        {
            return Ok(await _databaseTutorUOW.UserRepository.GetUserDetails());
        }
    }
}
