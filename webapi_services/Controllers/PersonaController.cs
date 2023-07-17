using business_layer.Personas.DTO;
using business_layer.Personas.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi_services.Controllers
{
    public class PersonaController : BaseController
    {
        [HttpPost("list")]
        public async Task<List<PersonaDTO>> getList(PersonasList.Request request)
        { 
            return await Mediator.Send(request);
        }

        [HttpPost("get-by-id")]
        public async Task<PersonaDTO> getByID(PersonasFind.Request request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("create")]
        public async Task<PersonaDTO> create(PersonasCreate.Request request)
        {
            return await Mediator.Send(request);
        }
    }
}
