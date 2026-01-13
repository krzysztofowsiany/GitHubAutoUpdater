using Spectre.Console;
using Spectre.Console.Cli;

namespace GitHubAutoUpdater.Cli.Commands;

public class InitCommand : Command<CommandSettings>
{
    public override int Execute(CommandContext context, CommandSettings settings)
    {
        AnsiConsole.MarkupLine("[green]Initializing GitHubUpdater...[/]");
        // Tutaj generowanie configu, np. updaterconfig.json
        return 0;
    }
}
