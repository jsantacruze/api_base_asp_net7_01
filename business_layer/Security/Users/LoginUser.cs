using AutoMapper;
using business_layer.Admin.DTO;
using business_layer.Contracts;
using business_layer.ExceptionsManager;
using data_access;
using domain_layer.Admin;
using domain_layer.Security;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Security.Users
{
    public class LoginUser
    {
        public class Request : IRequest<LoggedUser>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class RequestValidator : AbstractValidator<Request>
        {
            public RequestValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Request, LoggedUser>
        {

            private readonly UserManager<SystemUser> _userManager;
            private readonly SignInManager<SystemUser> _signInManager;
            private readonly IJwtGenerator _jwtGenerador;
            private readonly IMapper _mapper;


            private readonly DatabaseContext _context;
            public Handler(UserManager<SystemUser> userManager, SignInManager<SystemUser> signInManager, IJwtGenerator jwtGenerador, DatabaseContext context
                , IMapper mapper)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerador = jwtGenerador;
                _context = context;
                _mapper = mapper;
            }
            public async Task<LoggedUser> Handle(Request request, CancellationToken cancellationToken)
            {
                var usuario = await
                    _userManager.FindByEmailAsync(request.Email);

                if (usuario == null)
                {
                    throw new CustomException(HttpStatusCode.Unauthorized, new { mensaje = "No exste un usuario con el email proporcionado" });
                }
               
                var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);
                var resultadoRoles = await _userManager.GetRolesAsync(usuario);
                var listaRoles = new List<string>(resultadoRoles);


                if (resultado.Succeeded)
                {
                    var empleado = await this._context.Empleados
                                            .SingleAsync(e => e.persona_id == usuario.empleado_id);

                    usuario.Empleado = empleado;
                        return new LoggedUser
                        {
                            user_id = usuario.Id,
                            Token = _jwtGenerador.CreateToken(usuario, listaRoles),
                            Username = usuario.UserName,
                            Email = usuario.Email, 
                            Roles = listaRoles,
                            ProfilePicture = usuario.ProfilePicture,
                            empleado_id = usuario.empleado_id,
                            Empleado = _mapper.Map<Empleado, EmpleadoDTO>(usuario.Empleado)
                        };
                }
                throw new CustomException(HttpStatusCode.Unauthorized, new { mensaje = "Password inválido" });
            }
        }
    }
}
