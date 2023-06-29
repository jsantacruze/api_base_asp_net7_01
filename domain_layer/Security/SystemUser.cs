using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain_layer.Security
{
    public class SystemUser : IdentityUser
    {
        public byte[]? ProfilePicture { get; set; }
        public long? empleado_id { get; set; }
        //[ForeignKey("empleado_id ")]
        //public Empleado? Empleado { get; set; }
    }
}
