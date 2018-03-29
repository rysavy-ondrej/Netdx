using System;
using Microsoft.Extensions.CommandLineUtils;
using SharpPcap;

namespace Netdx.Examples.MessageExtract
{
    class Program
    {

        static void Main(string[] args)
        {
            var commandLineApplication = new CommandLineApplication(true);
            commandLineApplication.Command(ExportDlms.Name, ExportDlms.Configuration);
            commandLineApplication.Command(PrintInterfaces.Name, PrintInterfaces.Configuration);

            commandLineApplication.HelpOption("-? | -h | --help");
            commandLineApplication.Name = typeof(Program).Assembly.GetName().Name;
            commandLineApplication.FullName = $"msgxtract tool ({typeof(Program).Assembly.GetName().Version})";

            commandLineApplication.OnExecute(() =>
            {
                commandLineApplication.Error.WriteLine();
                commandLineApplication.ShowHelp();
                return -1;
            });
            try
            {
                commandLineApplication.Execute(args);
            }
            catch (CommandParsingException e)
            {
                commandLineApplication.Error.WriteLine($"ERROR: {e.Message}");
            }
            catch (ArgumentException e)
            {
                commandLineApplication.Error.WriteLine($"ERROR: {e.Message}");
            }
            catch (PcapException e)
            {
                commandLineApplication.Error.WriteLine($"ERROR: {e.Message}");
            }
        }
    }
}
