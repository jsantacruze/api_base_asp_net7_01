using business_layer.Personas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Admin.DTO
{
    public class EmpleadoDTO : PersonaDTO
    {
        public string empleado_cargo { get; set; }
        public string empleado_feha_ingreso { get; set; }
        public DateTime? empleado_fecha_ultima_modificacion { get; set; }

    }
}
