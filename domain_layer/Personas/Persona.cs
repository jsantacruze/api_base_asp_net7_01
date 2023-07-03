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
      
        [StringLength(150)]
        public string persona_direccion { get; set; }
        public DateTime persona_fecha_nacimiento { get; set; }

        public int tipo_sangre_id { get; set; }

        [ForeignKey("tipo_sangre_id")]
        public TipoSangre TipoSangre { get; set; }
        [StringLength(255)]
        public string? persona_observaciones { get; set; }

        public int estado_civil_id { get; set; }
        [ForeignKey("estado_civil_id")]
        public EstadoCivil EstadoCivil { get; set; }

        [StringLength(30)]
        public string persona_telefono { get; set; }
        [StringLength(100)]
        public string persona_email { get; set; }

        public int? genero_id { get; set; }
        [ForeignKey("genero_id")]
        public Genero? Genero { get; set; }


    }
}
