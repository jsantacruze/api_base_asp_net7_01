using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain_layer.Personas
{
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int persona_id { get; set; }
        public string persona_nro_identifacion { get; set; }
        public string persona_apellidos { get; set; }
        public string persona_nombres { get; set; }
        public DateTime persona_fecha_nacimiento { get; set; }

        public int tipo_sangre_id { get; set; }

        [ForeignKey("tipo_sangre_id")]
        public TipoSangre TipoSangre { get; set; }
        [StringLength(255)]
        public string? persona_observaciones { get; set; }
    }
}
