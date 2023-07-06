using business_layer.Security.Users;
using business_layer.Security.Users.DTO;
using domain_layer.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace webapi_services.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet("list")]
        public async Task<ActionResult<List<SystemUserDTO>>> getList()
        {
            return await Mediator.Send(new ListUsers.Request
            {
            });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoggedUser>> Login(LoginUser.Request request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("create")]
        public async Task<ActionResult<LoggedUser>> Create(CreateUser.Request request)
        {
            return await Mediator.Send(request);
        }

        [AllowAnonymous]
        [HttpGet("current")]
        public async Task<ActionResult<LoggedUser>> getCurrentUser()
        {
            return await Mediator.Send(new CurrentUser.Request());
        }

        [HttpPut("edit")]
        public async Task<ActionResult<LoggedUser>> Actualizar(EditUser.Request request)
        {
            return await Mediator.Send(request);
        }
    }
}
