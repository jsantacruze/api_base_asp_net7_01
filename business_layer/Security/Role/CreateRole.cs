using business_layer.ExceptionsManager;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Mysqlx.Prepare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Security.Role
{
    public class CreateRole
    {
        public class Request : IRequest<bool>
        {
            public string RoleName { get; set; }
        }

        public class RequestValidator : AbstractValidator<Request>
        {
            public RequestValidator()
            {
                RuleFor(x => x.RoleName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Request, bool>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            public Handler(RoleManager<IdentityRole> roleManager)
            {
                _roleManager = roleManager;
            }

            public async Task<bool> Handle(Request request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.RoleName);
                if (role != null)
                {
                    throw new CustomException(HttpStatusCode.BadRequest, new { mensaje = "Ya existe el rol" });
                }

                var resultado = await _roleManager.CreateAsync(new IdentityRole(request.RoleName));
                if (resultado.Succeeded)
                {
                    return true;
                }

                throw new Exception("No se pudo guardar el rol");
            }
        }


    }
}
