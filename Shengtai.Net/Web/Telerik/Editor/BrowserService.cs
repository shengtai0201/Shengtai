using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Shengtai.Web.Telerik.Editor
{
    public class BrowserService : IBrowserService
    {
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public IEnumerable<FileBrowserEntry> GetDirectories(string serverMapPath)
        {
            var directory = new DirectoryInfo(serverMapPath);

            return directory.GetDirectories()
                .Select(subDirectory => new FileBrowserEntry
                {
                    Name = subDirectory.Name,
                    Type = EntryType.Directory
                });
        }

        public IEnumerable<FileBrowserEntry> GetFiles(string serverMapPath, string[] extensions)
        {
            var directory = new DirectoryInfo(serverMapPath);

            return extensions.SelectMany(directory.GetFiles)
                .Select(file => new FileBrowserEntry
                {
                    Name = file.Name,
                    Size = file.Length,
                    Type = EntryType.File
                });
        }

        public IEnumerable<string> DirectoryEnumerateFiles(string path)
        {
            return Directory.EnumerateFiles(path);
        }

        public async Task FileCopyAsync(string sourceFileName, string destFileName)
        {
            FileStream reader = new FileStream(sourceFileName, FileMode.Open);
            FileStream writer = new FileStream(sourceFileName, FileMode.CreateNew);

            byte[] buffer = new byte[0x1000];
            int read;
            while ((read = await reader.ReadAsync(buffer, 0, buffer.Length)) != 0)
                await writer.WriteAsync(buffer, 0, read);
        }

        public IEnumerable<string> DirectoryEnumerateDirectories(string path)
        {
            return Directory.EnumerateDirectories(path);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public async Task<Stream> FileOpenReadAsync(string path)
        {
            FileStream reader = new FileStream(path, FileMode.Open);
            MemoryStream stream = new MemoryStream();

            byte[] buffer = new byte[0x1000];
            int read;
            while ((read = await reader.ReadAsync(buffer, 0, buffer.Length)) != 0)
                await stream.WriteAsync(buffer, 0, read);

            return stream;
        }

        public Task FileDeleteAsync(string path)
        {
            return Task.Factory.StartNew(() => File.Delete(path));
        }

        public Task DirectoryDeleteAsync(string path)
        {
            return Task.Factory.StartNew(() => Directory.Delete(path, true));
        }

        public Task SaveAsAsync(HttpPostedFileBase file, string filename)
        {
            return Task.Factory.StartNew(() => file.SaveAs(filename));
        }
    }
}