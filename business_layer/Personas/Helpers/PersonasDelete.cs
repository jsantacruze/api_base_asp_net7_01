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
    public class PersonasDelete
    {
        public class Request : IRequest<bool>
        {
            public long persona_id { get; set; }
        }

        public class Handler : IRequestHandler<Request, bool>
        {
            private readonly DatabaseContext _context;
            private readonly IMapper _mapper;
            public Handler(DatabaseContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }



            public async Task<bool> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    bool result = false;
                    var persona = await _context.Personas
                        .Where(p => p.persona_id == request.persona_id)
                        .FirstOrDefaultAsync();

                    if (persona == null)
                    {
                        throw new CustomException(HttpStatusCode.BadRequest,
                            new { mensaje = "No existe una persona con el id especificado: " + request.persona_id.ToString() });
                    }
                    _context.Personas.Remove(persona);
                    int resultBBDD = await _context.SaveChangesAsync();
                    result = resultBBDD > 0;   

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
                    throw new CustomException(HttpStatusCode.BadRequest, "No se ha podido elimiar la persona. Errores:" + msg);
                }
            }
        }

    }
}
