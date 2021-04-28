using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public static class IdentityServerExtensions
    {
        public static void AddIdentityServer<TUser>(this IServiceCollection services, string connectionString, string assemblyName) where TUser : Models.Account.ApplicationUser
        {
            #region default service
            services.AddScoped<Service.IdentityServerService<TUser>>();
            services.AddScoped<ISignInService>(x => x.GetRequiredService<Service.IdentityServerService<TUser>>());
            services.AddScoped<IUserService>(x => x.GetRequiredService<Service.IdentityServerService<TUser>>());
            services.AddScoped<IEmailService>(x => x.GetRequiredService<Service.IdentityServerService<TUser>>());
            #endregion

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            }).AddConfigurationStore(options =>     // this adds the config data from DB (clients, resources, CORS)
            {
                options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString, builder => builder.MigrationsAssembly(assemblyName));
            }).AddOperationalStore(options =>       // this adds the operational data from DB (codes, tokens, consents)
            {
                options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString, builder => builder.MigrationsAssembly(assemblyName));

                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
            }).AddAspNetIdentity<TUser>();

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
        }

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

        #region menus
        // 回傳所新增之物件
        public static Models.Shared.INavHeader AddHeader(this IList<Models.Shared.INavHeader> menus)
        {
            var header = new Models.Shared.Menu { Menus = new List<Models.Shared.INavTreeView>() };
            menus.Add(header);
            
            return header;
        }

        // 回傳所新增之物件
        public static Models.Shared.INavHeader AddHeader(this IList<Models.Shared.INavHeader> menus, string text)
        {
            var header = new Models.Shared.Menu { Menus = new List<Models.Shared.INavTreeView>(), Text = text };
            menus.Add(header);

            return header;
        }

        // 回傳所新增之物件
        public static Models.Shared.INavTreeView AddTreeView(this Models.Shared.INavHeader header, string text, string icon)
        {
            var tv = new Models.Shared.Menu { Paragraph = new Models.Shared.Paragraph { Text = text }, Icon = icon, Menus = new List<Models.Shared.INavTreeView>(), Parent = (header, null) };
            header.Menus.Add(tv);

            return tv;
        }

        // 回傳所新增之物件
        public static Models.Shared.INavTreeView AddTreeView(this Models.Shared.INavHeader header, string text, string icon, string badgeClass, string badgeText)
        {
            var tv = new Models.Shared.Menu { Paragraph = new Models.Shared.Paragraph { Text = text, Badge = new Models.Shared.Badge { Class = badgeClass, Text = badgeText } }, Icon = icon, Menus = new List<Models.Shared.INavTreeView>(), Parent = (header, null) };
            header.Menus.Add(tv);

            return tv;
        }

        // 回傳所新增之物件
        public static Models.Shared.INavTreeView AddTreeView(this Models.Shared.INavTreeView treeView, string text, string icon)
        {
            var tv = new Models.Shared.Menu { Paragraph = new Models.Shared.Paragraph { Text = text }, Menus = new List<Models.Shared.INavTreeView>(), Parent = (null, treeView), Icon = icon };
            treeView.Menus.Add(tv);

            return tv;
        }

        private static void SetActive(Models.Shared.INavTreeView treeView)
        {
            if (treeView.Parent.TreeView != null)
                SetActive(treeView.Parent.TreeView);

            treeView.Active = true;
        }

        // 回傳父物件
        public static Models.Shared.INavTreeView AddItem(this Models.Shared.INavTreeView treeView, string text, string icon, string url, bool active = false)
        {
            var item = new Models.Shared.Menu { Paragraph = new Models.Shared.Paragraph { Text = text }, Url = url, Active = active, Parent = (null, treeView), Icon = icon };
            treeView.Menus.Add(item);

            if (active)
                SetActive(treeView);

            return treeView;
        }

        // 回傳父物件
        public static Models.Shared.INavTreeView AddItem(this Models.Shared.INavTreeView treeView, string text, string icon, string url, string small, bool active = false)
        {
            var item = new Models.Shared.Menu { Paragraph = new Models.Shared.Paragraph { Text = text, Small = small }, Url = url, Active = active, Parent = (null, treeView), Icon = icon };
            treeView.Menus.Add(item);

            if (active)
                SetActive(treeView);

            return treeView;
        }

        // 回傳父物件
        public static Models.Shared.INavHeader AddItem(this Models.Shared.INavHeader header, string text, string icon, string url, bool active = false)
        {
            var item = new Models.Shared.Menu { Paragraph = new Models.Shared.Paragraph { Text = text }, Url = url, Icon = icon, Active = active, Parent = (header, null) };
            header.Menus.Add(item);

            return header;
        }

        // 回傳父物件
        public static Models.Shared.INavHeader AddItem(this Models.Shared.INavHeader header, string text, string icon, string url, string badgeClass, string badgeText, bool active = false)
        {
            var item = new Models.Shared.Menu { Paragraph = new Models.Shared.Paragraph { Text = text, Badge = new Models.Shared.Badge { Class = badgeClass, Text = badgeText } }, Url = url, Icon = icon, Active = active, Parent = (header, null) };
            header.Menus.Add(item);

            return header;
        }
        #endregion

        public static IList<Models.Shared.INavHeader> GetMenus()
        {
            var menus = new List<Models.Shared.INavHeader>();

            var header1 = menus.AddHeader();
            header1.AddTreeView("Dashboard", "fa-tachometer-alt")
                .AddItem("Dashboard v1", "fa-circle", "../../index.html")
                .AddItem("Dashboard v2", "fa-circle", "../../index2.html")
                .AddItem("Dashboard v3", "fa-circle", "../../index3.html");
            header1.AddItem("Widgets", "fa-th", "../widgets.html", Models.Shared.Badge.Appearance.Danger, "New");
            header1.AddTreeView("Layout Options", "fa-copy", Models.Shared.Badge.Appearance.Info, "6")
                .AddItem("Top Navigation", "fa-circle", "../layout/top-nav.html")
                .AddItem("Top Navigation + Sidebar", "fa-circle", "../layout/top-nav-sidebar.html")
                .AddItem("Boxed", "fa-circle", "../layout/boxed.html")
                .AddItem("Fixed Sidebar", "fa-circle", "../layout/fixed-sidebar.html", true)
                .AddItem("Fixed Sidebar", "fa-circle", "../layout/fixed-sidebar-custom.html", "+ Custom Area")
                .AddItem("Fixed Navbar", "fa-circle", "../layout/fixed-topnav.html")
                .AddItem("Fixed Footer", "fa-circle", "../layout/fixed-footer.html")
                .AddItem("Collapsed Sidebar", "fa-circle", "../layout/collapsed-sidebar.html");
            header1.AddTreeView("Charts", "fa-chart-pie")
                .AddItem("ChartJS", "fa-circle", "../charts/chartjs.html")
                .AddItem("Flot", "fa-circle", "../charts/flot.html")
                .AddItem("Inline", "fa-circle", "../charts/inline.html")
                .AddItem("uPlot", "fa-circle", "../charts/uplot.html");
            header1.AddTreeView("UI Elements", "fa-tree")
                .AddItem("General", "fa-circle", "../UI/general.html")
                .AddItem("Icons", "fa-circle", "../UI/icons.html")
                .AddItem("Buttons", "fa-circle", "../UI/buttons.html")
                .AddItem("Sliders", "fa-circle", "../UI/sliders.html")
                .AddItem("Modals & Alerts", "fa-circle", "../UI/modals.html")
                .AddItem("Navbar & Tabs", "fa-circle", "../UI/navbar.html")
                .AddItem("Timeline", "fa-circle", "../UI/timeline.html")
                .AddItem("Ribbons", "fa-circle", "../UI/ribbons.html");
            header1.AddTreeView("Forms", "fa-edit")
                .AddItem("General Elements", "fa-circle", "../forms/general.html")
                .AddItem("Advanced Elements", "fa-circle", "../forms/advanced.html")
                .AddItem("Editors", "fa-circle", "../forms/editors.html")
                .AddItem("Validation", "fa-circle", "../forms/validation.html");
            header1.AddTreeView("Tables", "fa-table")
                .AddItem("Simple Tables", "fa-circle", "../tables/simple.html")
                .AddItem("DataTables", "fa-circle", "../tables/data.html")
                .AddItem("jsGrid", "fa-circle", "../tables/jsgrid.html");

            var header2 = menus.AddHeader("EXAMPLES");
            header2.AddItem("Calendar", "fa-calendar-alt", "../calendar.html", Models.Shared.Badge.Appearance.Info, "2");
            header2.AddItem("Gallery", "fa-image", "../gallery.html");
            header2.AddItem("Kanban Board", "fa-columns", "../kanban.html");
            header2.AddTreeView("Mailbox", "fa-envelope")
                .AddItem("Inbox", "fa-circle", "../mailbox/mailbox.html")
                .AddItem("Compose", "fa-circle", "../mailbox/compose.html")
                .AddItem("Read", "fa-circle", "../mailbox/read-mail.html");
            header2.AddTreeView("Pages", "fa-book")
                .AddItem("Invoice", "fa-circle", "../examples/invoice.html")
                .AddItem("Profile", "fa-circle", "../examples/profile.html")
                .AddItem("E-commerce", "fa-circle", "../examples/e-commerce.html")
                .AddItem("Projects", "fa-circle", "../examples/projects.html")
                .AddItem("Project Add", "fa-circle", "../examples/project-add.html")
                .AddItem("Project Edit", "fa-circle", "../examples/project-edit.html")
                .AddItem("Project Detail", "fa-circle", "../examples/project-detail.html")
                .AddItem("Contacts", "fa-circle", "../examples/contacts.html")
                .AddItem("FAQ", "fa-circle", "../examples/faq.html")
                .AddItem("Contact us", "fa-circle", "../examples/contact-us.html");
            var treeView1 = header2.AddTreeView("Extras", "fa-plus-square");
            treeView1.AddTreeView("Login & Register v1", "fa-circle")
                .AddItem("Login v1", "fa-circle", "../examples/login.html")
                .AddItem("Register v1", "fa-circle", "../examples/register.html")
                .AddItem("Forgot Password v1", "fa-circle", "../examples/forgot-password.html")
                .AddItem("Recover Password v1", "fa-circle", "../examples/recover-password.html");
            treeView1.AddTreeView("Login & Register v2", "fa-circle")
                .AddItem("Login v2", "fa-circle", "../examples/login-v2.html")
                .AddItem("Register v2", "fa-circle", "../examples/register-v2.html")
                .AddItem("Forgot Password v2", "fa-circle", "../examples/forgot-password-v2.html")
                .AddItem("Recover Password v2", "fa-circle", "../examples/recover-password-v2.html");
            treeView1
                .AddItem("Lockscreen", "fa-circle", "../examples/lockscreen.html")
                .AddItem("Legacy User Menu", "fa-circle", "../examples/legacy-user-menu.html")
                .AddItem("Language Menu", "fa-circle", "../examples/language-menu.html")
                .AddItem("Error 404", "fa-circle", "../examples/404.html")
                .AddItem("Error 500", "fa-circle", "../examples/500.html")
                .AddItem("Pace", "fa-circle", "../examples/pace.html")
                .AddItem("Blank Page", "fa-circle", "../examples/blank.html")
                .AddItem("Starter Page", "fa-circle", "../../starter.html");
            header2.AddTreeView("Search", "fa-search")
                .AddItem("Simple Search", "fa-circle", "../search/simple.html")
                .AddItem("Enhanced", "fa-circle", "../search/enhanced.html");

            var header3 = menus.AddHeader("MISCELLANEOUS");
            header3.AddItem("Tabbed IFrame Plugin", "fa-ellipsis-h", "../../iframe.html");
            header3.AddItem("Documentation", "fa-file", "https://adminlte.io/docs/3.1/");

            var header4 = menus.AddHeader("MULTI LEVEL EXAMPLE");
            header4.AddItem("Level 1", "fa-circle", "#");
            var treeView2 = header4.AddTreeView("Level 1", "fa-circle");
            treeView2.AddItem("Level 2", "fa-circle", "#");
            treeView2.AddTreeView("Level 2", "fa-circle")
                .AddItem("Level 3", "fa-dot-circle", "#")
                .AddItem("Level 3", "fa-dot-circle", "#")
                .AddItem("Level 3", "fa-dot-circle", "#");
            treeView2.AddItem("Level 2", "fa-circle", "#");
            header4.AddItem("Level 1", "fa-circle", "#");

            var header5 = menus.AddHeader("LABELS");
            header5.AddItem("Important", "fa-circle text-danger", "#");
            header5.AddItem("Warning", "fa-circle text-warning", "#");
            header5.AddItem("Informational", "fa-circle text-info", "#");

            return menus;
        }
    }
}
