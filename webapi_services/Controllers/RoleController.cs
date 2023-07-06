using business_layer.Security.Role;
using business_layer.Security.Role.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace webapi_services.Controllers
{
    public class RoleController : BaseController
    {
        [HttpGet]
        [Route("list")]
        public async Task<ActionResult<List<RoleDTO>>> List()
        {
            return await Mediator.Send(new ListRoles.Request());
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<string>> Create(CreateRole.Request request)
        {
            var result = await Mediator.Send(request);
            return result ? "true" : "false";
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult<string>> Delete(DeleteRole.Request request)
        {
            var result = await Mediator.Send(request);
            return result ? "true" : "false";
        }

        [HttpPost]
        [Route("add-rol-to-user")]
        public async Task<ActionResult<string>> AddRoleToUser(AddRoleToUser.Request request)
        {
            var result = await Mediator.Send(request);
            return result ? "true" : "false";
        }

        [HttpPost]
        [Route("del-rol-to-user")]
        public async Task<ActionResult<string>> DeleteRoleToUser(DeleteRoleToUser.Request request)
        {
            var result = await Mediator.Send(request);
            return result ? "true" : "false";
        }

        [HttpGet("get-roles/{username}")]
        public async Task<ActionResult<List<string>>> GetRolesByUser(string username)
        {
            return await Mediator.Send(new ListRolesByUser.Request { Username = username });
        }
    }
}
