using Spectre.Console;
using Spectre.Console.Cli;

public class PublishCommand : Command<PublishCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandOption("--version <VERSION>")]
        public string Version { get; set; } = default!;

        [CommandOption("--app <PATH>")]
        public string AppPath { get; set; } = default!;
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        AnsiConsole.MarkupLine("[green]Publishing update...[/]");

        // 1. zip build
        // 2. generate manifest.json
        // 3. upload GitHub Release

        AnsiConsole.MarkupLine($"[blue]Version:[/] {settings.Version}");
        AnsiConsole.MarkupLine("[green]Done![/]");

        return 0;
    }
}