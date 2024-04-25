using System.Threading.Tasks;
using CVBuilder.Application.Role.Queries;
using CVBuilder.Web.Contracts.V1;
using CVBuilder.Web.Infrastructure.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Web.Controllers.V1;

public class RoleController : BaseAuthApiController
{
    [HttpGet(ApiRoutes.Role.GetAllRoles)]
    public async Task<ActionResult> GetAllUsers()
    {
        var command = new GetAllRolesQuery();
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}