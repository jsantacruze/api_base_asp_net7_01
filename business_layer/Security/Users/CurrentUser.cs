using AutoMapper;
using business_layer.Admin.DTO;
using business_layer.Contracts;
using data_access;
using domain_layer.Admin;
using domain_layer.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Security.Users
{
    public class CurrentUser
    {
        public class Request : IRequest<LoggedUser> { }

        public class Handler : IRequestHandler<Request, LoggedUser>
        {
            private readonly UserManager<SystemUser> _userManager;
            private readonly IJwtGenerator _jwtGenerador;
            private readonly IUserSession _usuarioSesion;
            private readonly IMapper _mapper;

            private readonly DatabaseContext _context;
            public Handler(UserManager<SystemUser> userManager, IJwtGenerator jwtGenerador, IUserSession usuarioSesion, DatabaseContext context, IMapper mapper)
            {
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
                _usuarioSesion = usuarioSesion;
                _context = context;
                _mapper = mapper;
            }
            public async Task<LoggedUser> Handle(Request request, CancellationToken cancellationToken)
            {
                var userNameSession = _usuarioSesion.getUserSession().ToUpper().Normalize(); 
                var usuario = await _userManager.FindByNameAsync(userNameSession);
                var resultadoRoles = await _userManager.GetRolesAsync(usuario);
                var listaRoles = new List<string>(resultadoRoles);

                var empleado = await this._context.Empleados
                        .SingleAsync(e => e.persona_id == usuario.empleado_id);
                usuario.Empleado = empleado;

                return new LoggedUser
                {
                    //FirstName = usuario.FirstName,
                    //LastName = usuario.LastName,
                    Username = usuario.UserName,
                    Token = _jwtGenerador.CreateToken(usuario, listaRoles),
                    Email = usuario.Email,
                    Roles = listaRoles,
                    ProfilePicture = usuario.ProfilePicture,
                    Empleado = _mapper.Map<Empleado, EmpleadoDTO>(usuario.Empleado)
                };
            }
        }

    }
}
