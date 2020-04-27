using System.Collections.Generic;
using System.Linq;

namespace Shengtai.Web.Telerik.Editor
{
    public class DirectoryBrowser
    {
        private readonly IBrowserService browserService;

        public DirectoryBrowser(IBrowserService browserService)
        {
            this.browserService = browserService;
        }

        public IEnumerable<FileBrowserEntry> GetContent(string path, string filter)
        {
            //var test1 = GetFiles(path, filter).ToList();    // pass
            //var test2 = GetDirectories(path).ToList();      // error
            //var test3 = test1.Concat(test2);
            //return test3;
            return GetFiles(path, filter).Concat(GetDirectories(path));
        }

        private IEnumerable<FileBrowserEntry> GetFiles(string path, string filter)
        {
            var extensions = (filter ?? "*").Split(",|;".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);

            return this.browserService.GetFiles(Server.MapPath(path), extensions);
        }

        private IEnumerable<FileBrowserEntry> GetDirectories(string path)
        {
            return this.browserService.GetDirectories(Server.MapPath(path));
        }

        public System.Web.HttpServerUtilityBase Server { get; set; }
    }
}