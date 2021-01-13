using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Shengtai
{
    public static partial class Extensions
    {
        public static Process ProcessStart(string fileName, string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = fileName,
                UseShellExecute = true,
                CreateNoWindow = false,
                //RedirectStandardOutput = true,
                Arguments = arguments
            };

            Process process = Process.Start(startInfo);

            //StreamReader reader = process.StandardOutput;
            //string line = reader.ReadLine();
            //while (!reader.EndOfStream)
            //{
            //    if (!string.IsNullOrEmpty(line))
            //        Console.WriteLine(line);

            //    line = reader.ReadLine();
            //}
            //string line = reader.ReadLine();

            //string line = null;
            //while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            //    Console.WriteLine(line);

            //reader.Close();
            //reader.Dispose();

            process.WaitForExit();
            process.Close();
            process.Dispose();

            return process;
        }

        public static async Task<int> ProcessStartAsync(string fileName, string arguments)
        {
            var taskCompletionSource = new TaskCompletionSource<int>();

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = fileName,
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardOutput = true,
                Arguments = arguments
            };

            Process process = Process.Start(startInfo);
            process.Exited += (sender, args) =>
            {
                taskCompletionSource.SetResult(process.ExitCode);
                process.Dispose();
            };

            StreamReader reader = process.StandardOutput;
            string line = null;
            while (!string.IsNullOrEmpty(line = await reader.ReadLineAsync()))
                Console.WriteLine(line);

            reader.Close();
            reader.Dispose();

            process.WaitForExit();

            return await taskCompletionSource.Task;
        }
    }
}