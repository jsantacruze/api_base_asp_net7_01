using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain_layer.Personas
{
    public class EstadoCivil
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int estado_civil_id { get; set; }
        [Required]
        [StringLength(60)]
        public string estado_civil_nombre { get; set; }
        public virtual ICollection<Persona> lista_personas { get; set; }
    }
}
