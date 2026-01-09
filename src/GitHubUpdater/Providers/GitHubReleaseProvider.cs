using System.Text.Json;

namespace GitHubUpdater.Providers;

public class GitHubReleaseProvider
{
    private readonly string _repo;
    private static readonly HttpClient _httpClient = new();

    public GitHubReleaseProvider(string repo)
    {
        _repo = repo;
    }

    public async Task<string> GetLatestReleaseAsync()
    {
        var url = $"https://api.github.com/repos/{_repo}/releases/latest";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("User-Agent", "GitHubUpdater");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("tag_name").GetString() ?? "0.0.0";
    }

    public async Task<string> GetAssetUrlAsync(string assetName)
    {
        var url = $"https://api.github.com/repos/{_repo}/releases/latest";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("User-Agent", "GitHubUpdater");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var assets = doc.RootElement.GetProperty("assets").EnumerateArray();

        foreach (var asset in assets)
        {
            if (asset.GetProperty("name").GetString() == assetName)
                return asset.GetProperty("browser_download_url").GetString()!;
        }

        throw new Exception($"Asset {assetName} not found in latest release");
    }
}
