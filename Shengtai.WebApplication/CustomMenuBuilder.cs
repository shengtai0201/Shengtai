using Shengtai.IdentityServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shengtai.WebApplication
{
    public class CustomMenuBuilder : MenuBuilder
    {
        private static int _key = 0;
        private static int Key { get => _key++; }

        public CustomMenuBuilder() : base() { }

        public override void Initialize()
        {
            var header1 = this.AddHeader();
            header1.AddTreeView("Dashboard", "fa-tachometer-alt")
                .AddItem(Key, Roles.Anonymous, "Dashboard v1", "fa-circle", "../../index.html")
                .AddItem(Key, Roles.Anonymous, "Dashboard v2", "fa-circle", "../../index2.html")
                .AddItem(Key, Roles.Anonymous, "Dashboard v3", "fa-circle", "../../index3.html");
            header1.AddItem(Key, Roles.Anonymous, "Widgets", "fa-th", "../widgets.html", IdentityServer.Models.Shared.Badge.Appearance.Danger, "New");
            header1.AddTreeView("Layout Options", "fa-copy", IdentityServer.Models.Shared.Badge.Appearance.Info, "6")
                .AddItem(Key, Roles.Anonymous, "Top Navigation", "fa-circle", "../layout/top-nav.html")
                .AddItem(Key, Roles.Anonymous, "Top Navigation + Sidebar", "fa-circle", "../layout/top-nav-sidebar.html")
                .AddItem(Key, Roles.Anonymous, "Boxed", "fa-circle", "../layout/boxed.html")
                .AddItem(Key, Roles.Anonymous, "Fixed Sidebar", "fa-circle", "../layout/fixed-sidebar.html", true)
                .AddItem(Key, Roles.Anonymous, "Fixed Sidebar", "fa-circle", "../layout/fixed-sidebar-custom.html", "+ Custom Area")
                .AddItem(Key, Roles.Anonymous, "Fixed Navbar", "fa-circle", "../layout/fixed-topnav.html")
                .AddItem(Key, Roles.Anonymous, "Fixed Footer", "fa-circle", "../layout/fixed-footer.html")
                .AddItem(Key, Roles.Anonymous, "Collapsed Sidebar", "fa-circle", "../layout/collapsed-sidebar.html");
            header1.AddTreeView("Charts", "fa-chart-pie")
                .AddItem(Key, Roles.Anonymous, "ChartJS", "fa-circle", "../charts/chartjs.html")
                .AddItem(Key, Roles.Anonymous, "Flot", "fa-circle", "../charts/flot.html")
                .AddItem(Key, Roles.Anonymous, "Inline", "fa-circle", "../charts/inline.html")
                .AddItem(Key, Roles.Anonymous, "uPlot", "fa-circle", "../charts/uplot.html");
            header1.AddTreeView("UI Elements", "fa-tree")
                .AddItem(Key, Roles.Anonymous, "General", "fa-circle", "../UI/general.html")
                .AddItem(Key, Roles.Anonymous, "Icons", "fa-circle", "../UI/icons.html")
                .AddItem(Key, Roles.Anonymous, "Buttons", "fa-circle", "../UI/buttons.html")
                .AddItem(Key, Roles.Anonymous, "Sliders", "fa-circle", "../UI/sliders.html")
                .AddItem(Key, Roles.Anonymous, "Modals & Alerts", "fa-circle", "../UI/modals.html")
                .AddItem(Key, Roles.Anonymous, "Navbar & Tabs", "fa-circle", "../UI/navbar.html")
                .AddItem(Key, Roles.Anonymous, "Timeline", "fa-circle", "../UI/timeline.html")
                .AddItem(Key, Roles.Anonymous, "Ribbons", "fa-circle", "../UI/ribbons.html");
            header1.AddTreeView("Forms", "fa-edit")
                .AddItem(Key, Roles.Anonymous, "General Elements", "fa-circle", "../forms/general.html")
                .AddItem(Key, Roles.Anonymous, "Advanced Elements", "fa-circle", "../forms/advanced.html")
                .AddItem(Key, Roles.Anonymous, "Editors", "fa-circle", "../forms/editors.html")
                .AddItem(Key, Roles.Anonymous, "Validation", "fa-circle", "../forms/validation.html");
            header1.AddTreeView("Tables", "fa-table")
                .AddItem(Key, Roles.Anonymous, "Simple Tables", "fa-circle", "../tables/simple.html")
                .AddItem(Key, Roles.Anonymous, "DataTables", "fa-circle", "../tables/data.html")
                .AddItem(Key, Roles.Anonymous, "jsGrid", "fa-circle", "../tables/jsgrid.html");

            var header2 = this.AddHeader("EXAMPLES");
            header2.AddItem(Key, Roles.System, "Calendar", "fa-calendar-alt", "../calendar.html", IdentityServer.Models.Shared.Badge.Appearance.Info, "2");
            header2.AddItem(Key, Roles.System, "Gallery", "fa-image", "../gallery.html");
            header2.AddItem(Key, Roles.System, "Kanban Board", "fa-columns", "../kanban.html");
            header2.AddTreeView("Mailbox", "fa-envelope")
                .AddItem(Key, Roles.System, "Inbox", "fa-circle", "../mailbox/mailbox.html")
                .AddItem(Key, Roles.System, "Compose", "fa-circle", "../mailbox/compose.html")
                .AddItem(Key, Roles.System, "Read", "fa-circle", "../mailbox/read-mail.html");
            header2.AddTreeView("Pages", "fa-book")
                .AddItem(Key, Roles.System, "Invoice", "fa-circle", "../examples/invoice.html")
                .AddItem(Key, Roles.System, "Profile", "fa-circle", "../examples/profile.html")
                .AddItem(Key, Roles.System, "E-commerce", "fa-circle", "../examples/e-commerce.html")
                .AddItem(Key, Roles.System, "Projects", "fa-circle", "../examples/projects.html")
                .AddItem(Key, Roles.System, "Project Add", "fa-circle", "../examples/project-add.html")
                .AddItem(Key, Roles.System, "Project Edit", "fa-circle", "../examples/project-edit.html")
                .AddItem(Key, Roles.System, "Project Detail", "fa-circle", "../examples/project-detail.html")
                .AddItem(Key, Roles.System, "Contacts", "fa-circle", "../examples/contacts.html")
                .AddItem(Key, Roles.System, "FAQ", "fa-circle", "../examples/faq.html")
                .AddItem(Key, Roles.System, "Contact us", "fa-circle", "../examples/contact-us.html");
            var treeView1 = header2.AddTreeView("Extras", "fa-plus-square");
            treeView1.AddTreeView("Login & Register v1", "fa-circle")
                .AddItem(Key, Roles.System, "Login v1", "fa-circle", "../examples/login.html")
                .AddItem(Key, Roles.System, "Register v1", "fa-circle", "../examples/register.html")
                .AddItem(Key, Roles.System, "Forgot Password v1", "fa-circle", "../examples/forgot-password.html")
                .AddItem(Key, Roles.System, "Recover Password v1", "fa-circle", "../examples/recover-password.html");
            treeView1.AddTreeView("Login & Register v2", "fa-circle")
                .AddItem(Key, Roles.System, "Login v2", "fa-circle", "../examples/login-v2.html")
                .AddItem(Key, Roles.System, "Register v2", "fa-circle", "../examples/register-v2.html")
                .AddItem(Key, Roles.System, "Forgot Password v2", "fa-circle", "../examples/forgot-password-v2.html")
                .AddItem(Key, Roles.System, "Recover Password v2", "fa-circle", "../examples/recover-password-v2.html");
            treeView1
                .AddItem(Key, Roles.System, "Lockscreen", "fa-circle", "../examples/lockscreen.html")
                .AddItem(Key, Roles.System, "Legacy User Menu", "fa-circle", "../examples/legacy-user-menu.html")
                .AddItem(Key, Roles.System, "Language Menu", "fa-circle", "../examples/language-menu.html")
                .AddItem(Key, Roles.System, "Error 404", "fa-circle", "../examples/404.html")
                .AddItem(Key, Roles.System, "Error 500", "fa-circle", "../examples/500.html")
                .AddItem(Key, Roles.System, "Pace", "fa-circle", "../examples/pace.html")
                .AddItem(Key, Roles.System, "Blank Page", "fa-circle", "../examples/blank.html")
                .AddItem(Key, Roles.System, "Starter Page", "fa-circle", "../../starter.html");
            header2.AddTreeView("Search", "fa-search")
                .AddItem(Key, Roles.System, "Simple Search", "fa-circle", "../search/simple.html")
                .AddItem(Key, Roles.System, "Enhanced", "fa-circle", "../search/enhanced.html");

            var header3 = this.AddHeader("MISCELLANEOUS");
            header3.AddItem(Key, Roles.Enterprise, "Tabbed IFrame Plugin", "fa-ellipsis-h", "../../iframe.html");
            header3.AddItem(Key, Roles.Enterprise, "Documentation", "fa-file", "https://adminlte.io/docs/3.1/");

            var header4 = this.AddHeader("MULTI LEVEL EXAMPLE");
            header4.AddItem(Key, Roles.Administrator, "Level 1", "fa-circle", "#");
            var treeView2 = header4.AddTreeView("Level 1", "fa-circle");
            treeView2.AddItem(Key, Roles.Administrator, "Level 2", "fa-circle", "#");
            treeView2.AddTreeView("Level 2", "fa-circle")
                .AddItem(Key, Roles.Administrator, "Level 3", "fa-dot-circle", "#")
                .AddItem(Key, Roles.Administrator, "Level 3", "fa-dot-circle", "#")
                .AddItem(Key, Roles.Administrator, "Level 3", "fa-dot-circle", "#");
            treeView2.AddItem(Key, Roles.Administrator, "Level 2", "fa-circle", "#");
            header4.AddItem(Key, Roles.Administrator, "Level 1", "fa-circle", "#");

            var header5 = this.AddHeader("LABELS");
            header5.AddItem(Key, Roles.Anonymous | Roles.Enterprise, "Important", "fa-circle text-danger", "#");
            header5.AddItem(Key, Roles.Anonymous | Roles.Enterprise, "Warning", "fa-circle text-warning", "#");
            header5.AddItem(Key, Roles.Anonymous | Roles.Enterprise, "Informational", "fa-circle text-info", "#");
        }

        private Roles GetHierarchicalRole(ICollection<IdentityServer.Models.Shared.INavTreeView> menus)
        {
            Roles? r = null;

            foreach (var menu in menus)
            {
                if (menu.Type == IdentityServer.Models.Shared.MenuTypes.TreeView)
                {
                    if (r.HasValue)
                        r |= GetHierarchicalRole(menu.Menus);
                    else
                        r = GetHierarchicalRole(menu.Menus);
                }
                else
                {
                    if(r.HasValue)
                        r |= (Roles)menu.Role;
                    else
                        r = (Roles)menu.Role;
                }
            }

            if (!r.HasValue)
                throw new ArgumentException("請檢查選單設定");
            return r.Value;
        }

        public override bool ShowStrategy(IdentityServer.Models.Shared.Menu sender, IList<string> roles)
        {
            Roles role;
            if (sender.Type == IdentityServer.Models.Shared.MenuTypes.Item)
                role = (Roles)sender.Role;
            else
                role = this.GetHierarchicalRole(sender.Menus);

            if(roles == null || roles.Count == 0)
                return (role & Roles.Anonymous) == Roles.Anonymous;
            else
            {
                var r = Roles.Anonymous;
                foreach(var value in roles)
                {
                    if (Enum.TryParse(value, out Roles result))
                        r |= result;
                }

                return (role & r) == r;
            }
        }
    }
}
