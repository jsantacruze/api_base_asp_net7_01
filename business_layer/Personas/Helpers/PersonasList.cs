using AutoMapper;
using business_layer.Personas.DTO;
using data_access;
using domain_layer.Personas;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Personas.Helpers
{
    public class PersonasList
    {
        public class Request : IRequest<List<PersonaDTO>>
        {
            public string filtro { get; set; }
        }

        public class Handler : IRequestHandler<Request, List<PersonaDTO>>
        {
            private readonly DatabaseContext _context;
            private readonly IMapper _mapper;
            public Handler(DatabaseContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<PersonaDTO>> Handle(Request request, CancellationToken cancellationToken)
            {
                var lista = await _context.Personas
                    .Include(p => p.Genero)
                    .Include(p => p.EstadoCivil)
                    .Include(p => p.TipoSangre)
                    .Where( p => p.persona_apellidos.Contains(request.filtro) || 
                                    p.persona_nombres.Contains(request.filtro)).ToListAsync();

                var result = _mapper.Map<List<Persona>, List<PersonaDTO>>(lista);
                return result;
            }
        }

    }
}
