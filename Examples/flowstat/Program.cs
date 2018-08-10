using System;
using System.Diagnostics;
using System.IO;
using Netdx.ConversationTracker;
using PacketDotNet;
using SharpPcap.LibPcap;
using SharpPcap;
using System.Linq;
using System.Threading;
using Cassandra;
using Microsoft.Extensions.CommandLineUtils;

namespace Flowify
{
    /// <summary>
    /// Computes the flow statististics for the given input pcap file.
    /// It also creates a hierarchy of bloom filters to improve the access to individual packets. 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var commandLineApplication = new CommandLineApplication(true);
            commandLineApplication.Command(TrackFlows.Name, TrackFlows.Configuration);
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
