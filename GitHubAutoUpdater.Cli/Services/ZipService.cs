namespace GitHubAutoUpdater.Cli.Services;

internal class ZipService
{
    public string CreateZip(string appPath, string outputDir)
    {
        // Zip aplikacji i zwrócenie ścieżki do zip
        return System.IO.Path.Combine(outputDir, "app.zip");
    }
}
