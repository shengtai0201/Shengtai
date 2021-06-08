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
    public static class IdentityServerExtensions
    {
        /// <summary>
        /// Checks if the redirect URI is for a native client.
        /// </summary>
        /// <param name="context">Represents contextual information about a authorization request.</param>
        /// <returns></returns>
        public static bool IsNativeClient(this IdentityServer4.Models.AuthorizationRequest context)
        {
            return !context.RedirectUri.StartsWith("https", StringComparison.Ordinal) && !context.RedirectUri.StartsWith("http", StringComparison.Ordinal);
        }

        public static void LoadingPage(this PageModel model, string actionName, string redirectUrl)
        {
            model.HttpContext.Response.StatusCode = 200;
            model.HttpContext.Response.Headers["Location"] = string.Empty;

            model.RedirectToAction(actionName, new Models.Account.RedirectViewModel { RedirectUrl = redirectUrl });
        }

        public static IActionResult LoadingPage(this Controller controller, string viewName, string redirectUri)
        {
            controller.HttpContext.Response.StatusCode = 200;
            controller.HttpContext.Response.Headers["Location"] = "";

            return controller.View(viewName, new Models.Account.RedirectViewModel { RedirectUrl = redirectUri });
        }

        public static async Task<Models.Account.ApplicationUser> ChangeTypeAsync<TUser>(this IUserService userService, Models.Account.ApplicationUser user) where TUser : Models.Account.ApplicationUser
        {
            if (user.GetType() == typeof(TUser))
                return user;

            return await userService.FindByIdAsync(user.Id);
        }
    }
}
