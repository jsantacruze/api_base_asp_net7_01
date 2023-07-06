using business_layer.Security.Role.DTO;
using data_access;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Security.Role
{
    public class ListRoles
    {
        public class Request : IRequest<List<RoleDTO>>
        {
        }

        public class Handler : IRequestHandler<Request, List<RoleDTO>>
        {

            private readonly DatabaseContext _context;
            public Handler(DatabaseContext context)
            {
                _context = context;
            }
            public async Task<List<RoleDTO>> Handle(Request request, CancellationToken cancellationToken)
            {
                List<RoleDTO> result = new List<RoleDTO>();  
                var roles = await _context.Roles.ToListAsync();
                roles.ForEach(role =>
                {
                    result.Add(new RoleDTO
                    {
                        Id = role.Id,
                        Name = role.Name,
                        NormalizedName = role.NormalizedName,
                        ConcurrencyStamp = role.ConcurrencyStamp
                    });
                });
                return result;
            }
        }

    }
}
