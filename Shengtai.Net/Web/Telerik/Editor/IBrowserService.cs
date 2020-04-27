using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Shengtai.Web.Telerik.Editor
{
    public interface IBrowserService
    {
        IEnumerable<FileBrowserEntry> GetFiles(string serverMapPath, string[] extensions);

        IEnumerable<FileBrowserEntry> GetDirectories(string serverMapPath);

        bool DirectoryExists(string path);

        void CreateDirectory(string path);

        IEnumerable<string> DirectoryEnumerateFiles(string path);

        IEnumerable<string> DirectoryEnumerateDirectories(string path);

        Task DirectoryDeleteAsync(string path);

        Task FileCopyAsync(string sourceFileName, string destFileName);

        Task FileDeleteAsync(string path);

        bool FileExists(string path);

        Task<Stream> FileOpenReadAsync(string path);

        Task SaveAsAsync(System.Web.HttpPostedFileBase file, string filename);
    }
}