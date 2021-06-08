using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using PwnedPasswords.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public class DefaultErrorDescriber : PwnedPasswordErrorDescriber
    {
        public override IdentityError PwnedPassword()
        {
            return new IdentityError
            {
                Code = nameof(PwnedPassword),
                Description = "The password you entered has appeared in a data breach. Please choose a different password."
            };
        }
    }
}
