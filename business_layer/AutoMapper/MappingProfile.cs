using AutoMapper;
using business_layer.Admin.DTO;
using business_layer.Personas;
using business_layer.Security.Users.DTO;
using domain_layer.Admin;
using domain_layer.Personas;
using domain_layer.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EstadoCivil, EstadoCivilDTO>();
            CreateMap<TipoSangre, TipoSangreDTO>();
            CreateMap<Genero, GeneroDTO>();
            CreateMap<Persona, PersonaDTO>();
            CreateMap<Empleado, EmpleadoDTO>();
            CreateMap<SystemUser, SystemUserDTO>();

        }
    }
}
