using domain_layer.Personas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Personas
{
    public class PersonaDTO
    {
        public int persona_id { get; set; }
        public string persona_nro_identifacion { get; set; }
        public string persona_apellidos { get; set; }
        public string persona_nombres { get; set; }
        public string persona_direccion { get; set; }
        public DateTime persona_fecha_nacimiento { get; set; }
        public int tipo_sangre_id { get; set; }
        public TipoSangreDTO TipoSangre { get; set; }
        public string? persona_observaciones { get; set; }
        public int estado_civil_id { get; set; }
        public EstadoCivilDTO EstadoCivil { get; set; }
        public string persona_telefono { get; set; }
        public string persona_email { get; set; }
        public int? genero_id { get; set; }
        public GeneroDTO? Genero { get; set; }
    }
}
