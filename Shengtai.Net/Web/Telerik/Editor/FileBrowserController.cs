using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Shengtai.Web.Telerik.Editor
{
    /*
     * transport.read.url       '@Url.Content("~/ImageBrowser/Read")'
     * transport.destroy.url    '@Url.Content("~/ImageBrowser/DestroyAsync")'
     * transport.create.url     '@Url.Content("~/ImageBrowser/Create")'
     * transport.thumbnailUrl   '@Url.Content("~/ImageBrowser/ThumbnailAsync")'
     * transport.uploadUrl      '@Url.Content("~/ImageBrowser/UploadAsync")'
     * transport.fileUrl        '@Url.Content("~/FileBrowser/FileAsync")' +'?fileName={0}'
     */

    public abstract class FileBrowserController : Controller
    {
        private const string contentFolderRoot = "~/Content/";
        private const string prettyName = "Files/";
        private static readonly string[] foldersToCopy = new[] { "~/Content/editor/" };
        private const string DefaultFilter = "*.txt,*.doc,*.docx,*.xls,*.xlsx,*.ppt,*.pptx,*.zip,*.rar,*.jpg,*.jpeg,*.gif,*.png";

        private readonly DirectoryBrowser directoryBrowser;
        private readonly ContentInitializer contentInitializer;

        private readonly IBrowserService browserService;

        public FileBrowserController(IBrowserService browserService)
        {
            this.browserService = browserService;

            directoryBrowser = new DirectoryBrowser(browserService);
            contentInitializer = new ContentInitializer(contentFolderRoot, foldersToCopy, prettyName, browserService);
        }

        public string ContentPath
        {
            get
            {
                return Extensions.RunSync(() => contentInitializer.CreateUserFolderAsync(Server));
            }
        }

        private string ToAbsolute(string virtualPath)
        {
            return VirtualPathUtility.ToAbsolute(virtualPath);
        }

        private string CombinePaths(string basePath, string relativePath)
        {
            return VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(basePath), relativePath);
        }

        public virtual bool AuthorizeRead(string path)
        {
            return CanAccess(path);
        }

        protected virtual bool CanAccess(string path)
        {
            return path.StartsWith(ToAbsolute(ContentPath), StringComparison.OrdinalIgnoreCase);
        }

        private string NormalizePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return ToAbsolute(ContentPath);
            }

            return CombinePaths(ToAbsolute(ContentPath), path);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual JsonResult Read(string path)
        {
            path = NormalizePath(path);

            if (AuthorizeRead(path))
            {
                try
                {
                    directoryBrowser.Server = Server;

                    var result = directoryBrowser
                        .GetContent(path, DefaultFilter)
                        .Select(f => new
                        {
                            name = f.Name,
                            type = f.Type == EntryType.File ? "f" : "d",
                            size = f.Size
                        });

                    return Json(result);
                }
                catch (DirectoryNotFoundException)
                {
                    throw new HttpException(404, "File Not Found");
                }
            }

            throw new HttpException(403, "Forbidden");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual async Task<ActionResult> DestroyAsync(string path, string name, string type)
        {
            path = NormalizePath(path);

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(type))
            {
                path = CombinePaths(path, name);
                if (type.ToLowerInvariant() == "f")
                    await DeleteFileAsync(path);
                else
                    await DeleteDirectoryAsync(path);

                return Json(new object[0]);
            }
            throw new HttpException(404, "File Not Found");
        }

        public virtual bool AuthorizeDeleteFile(string path)
        {
            return CanAccess(path);
        }

        public virtual bool AuthorizeDeleteDirectory(string path)
        {
            return CanAccess(path);
        }

        protected virtual async Task DeleteFileAsync(string path)
        {
            if (!AuthorizeDeleteFile(path))
                throw new HttpException(403, "Forbidden");

            var physicalPath = Server.MapPath(path);
            if (this.browserService.FileExists(physicalPath))
                await this.browserService.FileDeleteAsync(physicalPath);
        }

        protected virtual async Task DeleteDirectoryAsync(string path)
        {
            if (!AuthorizeDeleteDirectory(path))
                throw new HttpException(403, "Forbidden");

            var physicalPath = Server.MapPath(path);
            if (this.browserService.DirectoryExists(physicalPath))
                await this.browserService.DirectoryDeleteAsync(physicalPath);
        }

        public virtual bool AuthorizeCreateDirectory(string path, string name)
        {
            return CanAccess(path);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Create(string path, FileBrowserEntry entry)
        {
            path = NormalizePath(path);
            var name = entry.Name;

            if (!string.IsNullOrEmpty(name) && AuthorizeCreateDirectory(path, name))
            {
                var physicalPath = Path.Combine(Server.MapPath(path), name);
                if (!this.browserService.DirectoryExists(physicalPath))
                    this.browserService.CreateDirectory(physicalPath);

                return Json(new
                {
                    name = entry.Name,
                    type = "d",
                    size = entry.Size
                });
            }

            throw new HttpException(403, "Forbidden");
        }

        public virtual bool AuthorizeUpload(string path, HttpPostedFileBase file)
        {
            return CanAccess(path) && IsValidFile(file.FileName);
        }

        private bool IsValidFile(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            var allowedExtensions = DefaultFilter.Split(',');

            return allowedExtensions.Any(e => e.EndsWith(extension, StringComparison.InvariantCultureIgnoreCase));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual async Task<ActionResult> UploadAsync(string path, HttpPostedFileBase file)
        {
            path = NormalizePath(path);
            var fileName = Path.GetFileName(file.FileName);

            if (AuthorizeUpload(path, file))
            {
                await this.browserService.SaveAsAsync(file, Path.Combine(Server.MapPath(path), fileName));

                return Json(new
                {
                    size = file.ContentLength,
                    name = fileName,
                    type = "f"
                }, "text/plain");
            }

            throw new HttpException(403, "Forbidden");
        }

        [OutputCache(Duration = 360, VaryByParam = "path")]
        public async Task<ActionResult> FileAsync(string fileName)
        {
            var path = NormalizePath(fileName);

            if (AuthorizeFile(path))
            {
                var physicalPath = Server.MapPath(path);

                if (System.IO.File.Exists(physicalPath))
                {
                    const string contentType = "application/octet-stream";
                    return File(await this.browserService.FileOpenReadAsync(physicalPath), contentType, fileName);
                }
            }

            throw new HttpException(403, "Forbidden");
        }

        public virtual bool AuthorizeFile(string path)
        {
            return CanAccess(path) && IsValidFile(Path.GetExtension(path));
        }
    }
}