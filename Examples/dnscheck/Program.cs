using System;
using System.Linq;
using Microsoft.Extensions.CommandLineUtils;
using SharpPcap;

namespace Netdx.Examples.DnsCheck
{
    enum OutputFormat { Csv, Json, Text };
    class Program
    {

        static void Main(string[] args)
        {
            var commandLineApplication = new CommandLineApplication(true);
            commandLineApplication.Command(DumpDnsEvents.Name, DumpDnsEvents.Configuration);
            commandLineApplication.Command(PrintInterfaces.Name, PrintInterfaces.Configuration);

            commandLineApplication.HelpOption("-? | -h | --help");
            commandLineApplication.Name = typeof(Program).Assembly.GetName().Name;
            commandLineApplication.FullName = $"Lwm2mDump Tool ({typeof(Program).Assembly.GetName().Version})";

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
