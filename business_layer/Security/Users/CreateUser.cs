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
    public class CreateUser
    {
        public class Request : IRequest<LoggedUser>
        {
            //public string FirstName { get; set; }
            //public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }

            public string Username { get; set; }
            public long EmpleadoID { get; set; }
            public byte[]? ProfilePicture { get; set; }
        }

        public class RequestValidator : AbstractValidator<Request>
        {
            public RequestValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.EmpleadoID).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Request, LoggedUser>
        {
            private readonly DatabaseContext _context;
            private readonly UserManager<SystemUser> _userManager;
            private readonly IJwtGenerator _jwtGenerador;
            private readonly IMapper _mapper;

            public Handler(DatabaseContext context, UserManager<SystemUser> userManager, IJwtGenerator jwtGenerador, IMapper mapper)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
                _mapper = mapper;
            }

            public async Task<LoggedUser> Handle(Request request, CancellationToken cancellationToken)
            {
                var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
                List<string> messages = new List<string>();
                if (existe)
                {
                    messages.Add("Existe ya un usuario registrado con este email");
                }

                var existeUserName = await _context.Users.Where(x => x.UserName == request.Username).AnyAsync();
                if (existeUserName)
                {
                    messages.Add("Existe ya un usuario con este username");
                }
                Empleado empleado = null;
               
                var existe_empleado = await _context.Empleados.Where(x => x.persona_id == request.EmpleadoID).AnyAsync();
                if (!existe_empleado)
                {
                    messages.Add("El empleado no existe");
                }
                else
                {
                    empleado = await this._context.Empleados
                        .SingleAsync(e => e.persona_id == request.EmpleadoID);
                }


                var empleado_asignado = await _context.Users.Where(x => x.empleado_id == request.EmpleadoID).AnyAsync();
                if (empleado_asignado)
                {
                    messages.Add("El empleado ya tiene una cuenta de usuario asignada");
                }


                if (!messages.Any())
                {
                    var usuario = new SystemUser
                    {
                        Email = request.Email,
                        UserName = request.Username,
                        empleado_id = request.EmpleadoID,
                        Empleado = empleado,
                        ProfilePicture = request.ProfilePicture
                    };

                    var resultado = await _userManager.CreateAsync(usuario, request.Password);
                    if (resultado.Succeeded)
                    {
                        return new LoggedUser
                        {
                            Token = _jwtGenerador.CreateToken(usuario, null),
                            Username = usuario.UserName,
                            Email = usuario.Email,
                            Roles = null,
                            ProfilePicture = usuario.ProfilePicture,
                            Empleado = _mapper.Map<Empleado, EmpleadoDTO>(usuario.Empleado)
                        };
                    }
                    else
                    {
                        throw new CustomException(HttpStatusCode.BadRequest, resultado.Errors);
                    }
                }
                throw new CustomException(HttpStatusCode.BadRequest, messages);
            }
        }
    }
}
