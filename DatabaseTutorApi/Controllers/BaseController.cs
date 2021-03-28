using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWork.UnitOfWork;

namespace DatabaseTutorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [EnableCors]
    public class BaseController : ControllerBase
    {
        protected readonly IDatabaseTutorUnitOfWork _databaseTutorUOW;

        public BaseController(IDatabaseTutorUnitOfWork databaseTutorUnitOfWork)
        {
            _databaseTutorUOW = databaseTutorUnitOfWork;
        }
    }
}
