using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.ApI.Base;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Data.APPMetaData;

namespace SchoolProject.ApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : AppControllerBase
    {
        [HttpGet(Router.DepartmentRouting.GetByID)]
        public async Task<IActionResult> GetDepartmentById([FromRoute] int id)
        {
            return NewResult(await Mediator.Send(new GetDepartmentByIDQuery(id)));
        }
    }
}
