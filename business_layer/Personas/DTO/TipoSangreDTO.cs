using domain_layer.Personas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Personas.DTO
{
    public class TipoSangreDTO
    {
        public int tipo_id { get; set; }
        public string tipo_nombre { get; set; }
    }
}
