﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Security.Role.DTO
{
    public class RoleDTO
    {
        public string Id { get; set; }
        public string? Name { get; set; }

        public string NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
    }
}
