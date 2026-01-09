using System;
using System.IO;
using System.IO.Compression;

internal class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: UpdaterAgent <zipPath> <targetDir>");
            return;
        }

        var zipPath = args[0];
        var targetDir = args[1];

        Console.WriteLine($"Updating from {zipPath} to {targetDir}...");

        // unzip and overwrite
        ZipFile.ExtractToDirectory(zipPath, targetDir, true);

        Console.WriteLine("Update applied successfully.");
    }
}
