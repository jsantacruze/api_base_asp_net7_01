using AutoMapper;
using business_layer.ExceptionsManager;
using business_layer.Personas.DTO;
using data_access;
using domain_layer.Personas;
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
    public class PersonasEdit
    {
        public class Request : IRequest<PersonaDTO>
        {
            public long persona_id { get; set; }
            public string persona_nro_identifacion { get; set; }
            public string persona_apellidos { get; set; }
            public string persona_nombres { get; set; }
            public string persona_direccion { get; set; }
            public DateTime persona_fecha_nacimiento { get; set; }
            public int tipo_sangre_id { get; set; }
            public string? persona_observaciones { get; set; }
            public int estado_civil_id { get; set; }
            public string persona_telefono { get; set; }
            public string persona_email { get; set; }
            public int? genero_id { get; set; }
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

            //public class RequestValidator : AbstractValidator<Request>
            //{
            //    public RequestValidator()
            //    {
            //        RuleFor(x => x.id).NotNull().WithMessage("El campo id es requerido para la búsqueda de una persona");
            //    }
            //}

            public async Task<PersonaDTO> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    var persona = await _context.Personas
                        .Where(p => p.persona_id == request.persona_id)
                        .FirstOrDefaultAsync();

                    if (persona == null)
                    {
                        throw new CustomException(HttpStatusCode.BadRequest,
                            new { mensaje = "No existe una persona con el id especificado: " + request.persona_nro_identifacion });
                    }

                    persona.persona_nro_identifacion = request.persona_nro_identifacion;
                    persona.persona_apellidos = request.persona_apellidos;
                    persona.persona_nombres = request.persona_nombres;
                    persona.persona_direccion = request.persona_direccion;
                    persona.persona_fecha_nacimiento = request.persona_fecha_nacimiento;
                    persona.tipo_sangre_id = request.tipo_sangre_id;
                    persona.persona_observaciones = request.persona_observaciones;
                    persona.estado_civil_id = request.estado_civil_id;
                    persona.persona_telefono = request.persona_telefono;
                    persona.persona_email = request.persona_email;
                    persona.genero_id = request.genero_id;

                    int resultBBDD = await _context.SaveChangesAsync();

                    PersonaDTO result = new PersonaDTO();

                    if (resultBBDD > 0)
                    {
                        var nueva_persona = await _context.Personas
                        .Include(p => p.Genero)
                        .Include(p => p.EstadoCivil)
                        .Include(p => p.TipoSangre)
                        .Where(p => p.persona_id == request.persona_id)
                        .FirstOrDefaultAsync();

                        if (nueva_persona != null)
                        {
                            result = _mapper.Map<Persona, PersonaDTO>(nueva_persona);
                        }
                    }

                    return result;
                }
                catch (CustomException cex)
                {
                    throw cex;
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    if (ex.InnerException != null)
                    {
                        msg += ex.InnerException.Message;
                    }
                    throw new CustomException(HttpStatusCode.BadRequest, "No se ha podido editar la persona. Errores:" + msg);
                }
            }
        }
    }
}
