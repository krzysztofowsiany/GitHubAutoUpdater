using Spectre.Console;
using Spectre.Console.Cli;

namespace GitHubAutoUpdater.Cli.Commands;
internal class DoctorCommand : Command<CommandSettings>
{
    public override int Execute(CommandContext context, CommandSettings settings)
    {
        AnsiConsole.MarkupLine("[yellow]Validating updater configuration...[/]");
        // Walidacja configu i GitHub tokena
        return 0;
    }
}
{
}
