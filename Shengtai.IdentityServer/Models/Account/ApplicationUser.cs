using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shengtai.IdentityServer.Models.Account
{
    [Index(propertyNames: nameof(Account), IsUnique = true)]
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(256)]
        public string Account { get; set; }
    }
}
