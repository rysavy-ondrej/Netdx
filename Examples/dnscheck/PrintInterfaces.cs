using System;
using Microsoft.Extensions.CommandLineUtils;
using SharpPcap;

namespace Netdx.Examples.DnsCheck
{
    internal class PrintInterfaces
    {
        public static string Name => "print-interfaces";

        public static Action<CommandLineApplication> Configuration =>
            (CommandLineApplication target) =>
            {
                target.Description = "Print a list of the interfaces on which the tool can capture, and exit. For each network interface, a number and an interface name, possibly followed by a text description of the interface, is printed. The interface name or the number can be supplied to the -i option to specify an interface on which to capture.";
                target.OnExecute(() =>
                {
                    int i = 0;
                    /* Scan the list printing every entry */
                    foreach (var dev in CaptureDeviceList.Instance)
                    {
                        /* Description */
                        Console.WriteLine("{0}) {1} {2}", i, dev.Name, dev.Description);
                        i++;
                    }
                    return 0;
                });
            };
    }
}