using AutoMapper;
using business_layer.ExceptionsManager;
using business_layer.Personas.DTO;
using data_access;
using domain_layer.Personas;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Personas.Helpers
{
    public class PersonasFind
    {
        public class Request : IRequest<PersonaDTO>
        {
            public long id { get; set; }
        }

        public class Handler : IRequestHandler<Request, PersonaDTO>
        {   
            private readonly DatabaseContext _context;
            private readonly IMapper _mapper;
            public Handler(DatabaseContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public class RequestValidator : AbstractValidator<Request>
            {
                public RequestValidator()
                {
                    RuleFor(x => x.id).NotNull().WithMessage("El campo id es requerido para la búsqueda de una persona");
                }
            }

            public async Task<PersonaDTO> Handle(Request request, CancellationToken cancellationToken)
            {

                var persona = await _context.Personas
                    .Include(p => p.Genero)
                    .Include(p => p.EstadoCivil)
                    .Include(p => p.TipoSangre)
                    .Where(p => p.persona_id == request.id).FirstOrDefaultAsync();

                if (persona == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, 
                        new { mensaje = "No se encontro la persona con el ID: " + request.id.ToString() });
                }

                var result = _mapper.Map<Persona, PersonaDTO>(persona);
                return result;
            }
        }
    }
}
