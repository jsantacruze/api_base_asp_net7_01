using domain_layer.Personas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain_layer.Admin
{
    public class Empleado : Persona
    {
        [Required]
        [StringLength(50)]
        public string empleado_cargo { get; set; }

        [Required]
        [StringLength(15)]
        public string empleado_feha_ingreso { get; set; }

        public DateTime? empleado_fecha_ultima_modificacion { get; set; }
    }
}
