using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Models.Account
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(256)]
        public string Account { get; set; }
    }
}
