using GitHubUpdater.Providers;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubUpdater;

public class Updater
{
    public UpdaterOptions Options { get; private set; } = new();

    private GitHubReleaseProvider _provider = null!;
    private static readonly HttpClient _httpClient = new();

    public static Updater Configure(Action<UpdaterOptions> configure)
    {
        var updater = new Updater();
        var options = new UpdaterOptions();
        configure(options);
        updater.Options = options;
        updater._provider = new GitHubReleaseProvider(options.Repository);
        return updater;
    }

    public async Task CheckAndUpdateAsync()
    {
        var latest = await _provider.GetLatestReleaseAsync();

        if (latest == Options.CurrentVersion)
        {
            Console.WriteLine("Already up to date!");
            return;
        }

        Console.WriteLine($"New version available: {latest}");
        var zipUrl = await _provider.GetAssetUrlAsync("app-win-x64.zip");

        var tempPath = Path.Combine(Path.GetTempPath(), "update.zip");
        using (var response = await _httpClient.GetAsync(zipUrl))
        {
            response.EnsureSuccessStatusCode();
            using var fs = File.Create(tempPath);
            await response.Content.CopyToAsync(fs);
        }

        // Uruchom updater agent
        var agentPath = Path.Combine(AppContext.BaseDirectory, "UpdaterAgent.exe");
        var startInfo = new ProcessStartInfo(agentPath)
        {
            Arguments = $"\"{tempPath}\" \"{AppContext.BaseDirectory}\"",
            UseShellExecute = false,
        };
        Process.Start(startInfo);

        Console.WriteLine("Updater agent started. Exiting app...");
        Environment.Exit(0);
    }
}

public class UpdaterOptions
{
    public string Repository { get; set; } = string.Empty;
    public string CurrentVersion { get; set; } = "0.0.0";
}
