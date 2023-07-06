using business_layer.Admin.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Security.Users.DTO
{
    public class SystemUserDTO : IdentityUserDTO
    {
        public byte[]? ProfilePicture { get; set; }
        public long? empleado_id { get; set; }
        public EmpleadoDTO? Empleado { get; set; }
    }
}
