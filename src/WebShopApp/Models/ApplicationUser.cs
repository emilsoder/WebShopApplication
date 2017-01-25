using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace WebShopApp.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
    }
}
