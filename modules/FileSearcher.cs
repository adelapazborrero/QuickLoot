using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QuickLoot
{
    internal class FileSearcher
    {
        public static string EnumerateFiles()
        {
            string outputFilePath = $"{Environment.MachineName}_files.txt";
            string searchPath = @"C:\Users\";

            string[] fileExtensions = { "*.txt", "*.pdf", "*.xls", "*.xlsx", "*.doc", "*.docx", "*.kdbx" };

            try
            {
                using (StreamWriter writer = new StreamWriter(outputFilePath, false))
                {
                    writer.WriteLine($"[+] Enumerating files in {searchPath}\n");
                    foreach (string ext in fileExtensions)
                    {
                        writer.WriteLine($"[+] Searching for {ext} files...\n");
                        int fileCount = 0;

                        foreach (string file in GetFilesRecursive(searchPath, ext))
                        {
                            writer.WriteLine(file);
                            fileCount++;
                        }

                        writer.WriteLine($"[+] Found {fileCount} files with extension {ext}\n");
                        writer.WriteLine(new string('-', 50));
                    }
                }

                Console.WriteLine($"File enumeration saved to {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[!] Error during file enumeration: {ex.Message}");
            }

            return outputFilePath;
        }

        public static IEnumerable<string> GetFilesRecursive(string root, string searchPattern)
        {
            var files = new List<string>();

            try
            {
                files.AddRange(Directory.GetFiles(root, searchPattern));

                foreach (var dir in Directory.GetDirectories(root))
                {
                    files.AddRange(GetFilesRecursive(dir, searchPattern));
                }
            }
            catch (UnauthorizedAccessException)
            {
                //do nothing
            }
            catch (Exception ex)
            {
                //do nothing
            }

            return files;
        }
    }
}
