using domain_layer.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access
{
    public class UserIdentityDataInicializer
    {
        public static async Task Inicialize(DatabaseContext context, UserManager<SystemUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                //var usuario = new SystemUser { FirstName = "Julio Jhovany", LastName= "Santacruz Espinoza", UserName = "jsantacruze", Email = "jsantacruze@hotmail.com" };
                var usuario = new SystemUser { UserName = "jsantacruze", Email = "jsantacruze@hotmail.com" };
                await userManager.CreateAsync(usuario, "Digitalsoft2023-");
            }
        }
    }
}
