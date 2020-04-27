using System.IO;
using System.Threading.Tasks;

namespace Shengtai.Web.Telerik.Editor
{
    public class ContentInitializer
    {
        private readonly string rootFolder;
        private readonly string[] foldersToCopy;
        private readonly string prettyName;

        private readonly IBrowserService browserService;

        public ContentInitializer(string rootFolder, string[] foldersToCopy, string prettyName, IBrowserService browserService)
        {
            this.rootFolder = rootFolder;
            this.foldersToCopy = foldersToCopy;
            this.prettyName = prettyName;

            this.browserService = browserService;
        }

        public async Task<string> CreateUserFolderAsync(System.Web.HttpServerUtilityBase server)
        {
            //var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            //var virtualPath = Path.Combine(rootFolder, Path.Combine("UserFiles", userId), prettyName);
            var virtualPath = Path.Combine(rootFolder, prettyName);

            var path = server.MapPath(virtualPath);
            if (!this.browserService.DirectoryExists(path))
            {
                this.browserService.CreateDirectory(path);
                foreach (var sourceFolder in foldersToCopy)
                    await CopyFolderAsync(server.MapPath(sourceFolder), path);
            }

            return virtualPath;
        }

        private async Task CopyFolderAsync(string source, string destination)
        {
            if (!this.browserService.DirectoryExists(destination))
                this.browserService.CreateDirectory(destination);

            foreach (var file in this.browserService.DirectoryEnumerateFiles(source))
            {
                var dest = Path.Combine(destination, Path.GetFileName(file));
                await this.browserService.FileCopyAsync(file, dest);
            }

            foreach (var folder in this.browserService.DirectoryEnumerateDirectories(source))
            {
                var dest = Path.Combine(destination, Path.GetFileName(folder));
                await CopyFolderAsync(folder, dest);
            }
        }
    }
}