using business_layer.Admin.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Security.Users
{
    public class LoggedUser
    {
        public string user_id { get; set; }
        public long? empleado_id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
        public byte[]? ProfilePicture { get; set; }

        public EmpleadoDTO? Empleado { get; set; }
    }
}
