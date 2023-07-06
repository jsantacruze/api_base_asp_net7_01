using AutoMapper;
using business_layer.Security.Users.DTO;
using data_access;
using domain_layer.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace business_layer.Security.Users
{
    public class ListUsers
    {
        public class Request : IRequest<List<SystemUserDTO>>
        {
        }

        public class Handler : IRequestHandler<Request, List<SystemUserDTO>>
        {

            private readonly DatabaseContext _context;
            private readonly IMapper _mapper;

            public Handler(DatabaseContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<SystemUserDTO>> Handle(Request request, CancellationToken cancellationToken)
            {
                var users = await _context.Users
                    .Include(u => u.Empleado)
                    .ToListAsync();
                var result = _mapper.Map<List<SystemUser>, List<SystemUserDTO>>(users);

                return result;
            }
        }

    }
}
