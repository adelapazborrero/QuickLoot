using System;
using System.IO;

namespace QuickLoot
{
    internal class HistorySearcher
    {
        public static string GetHistory()
        {
            const string powershellHistoryPath = "AppData\\Roaming\\Microsoft\\Windows\\PowerShell\\PSReadLine\\ConsoleHost_history.txt";
            string outputFilePath = $"{Environment.MachineName}_history.txt";

            try
            {
                string[] dirs = Directory.GetDirectories(@"C:\Users\", "*", SearchOption.TopDirectoryOnly);
                using (StreamWriter writer = new StreamWriter(outputFilePath, false))
                {
                    foreach (string dir in dirs)
                    {
                        string username = new DirectoryInfo(dir).Name;
                        string historyPath = Path.Combine(dir, powershellHistoryPath);

                        writer.WriteLine($"[+] {username} history\n");

                        if (File.Exists(historyPath))
                        {
                            string readText = File.ReadAllText(historyPath);
                            writer.WriteLine(readText);
                        }
                        else
                        {
                            writer.WriteLine("[-] No PowerShell history found.\n");
                        }

                        writer.WriteLine(new string('-', 50));
                    }
                }

                Console.WriteLine($"[+] History has been saved to {outputFilePath}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"[!] Permission error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[!] Error: {ex.Message}");
            }

            return outputFilePath;
        }
    }
}
