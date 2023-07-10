using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Personas.DTO
{
    public class EstadoCivilDTO
    {
        public int estado_civil_id { get; set; }
        public string estado_civil_nombre { get; set; }
    }
}
