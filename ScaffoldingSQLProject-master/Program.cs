using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CapstoneIdeas
{
    public class Program
    {
        public static void Main(string[] args)
        {
			bool debuggerAttached = Debugger.IsAttached;
			Task.Run(async () =>
			{
				await Task.Delay(5000);
				// If not in debug mode, open the browser
				if (!debuggerAttached)
				{
					// It should automagically ridirect to https port 5001.
					const string HTTP = "http://localhost:5000";

					if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
					{
						Console.WriteLine($"Windows Operting System detected. Opening browser on {HTTP}");
						Process.Start(new ProcessStartInfo(HTTP) { UseShellExecute = true });
					}
					else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
					{
						// TODO: Test
						Console.WriteLine($"OSX (MacOS) Operating System Detected.  Opening browser on {HTTP}");
						Process.Start("open", HTTP);
					}
					else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
					{
						Console.WriteLine($"Linux based Operating System Detected. Attmpting to open browser on {HTTP}. Should this fail, please open the browser to this address manually.");
						Process.Start("xdg-open", HTTP);
					}
					else
					{
						throw new InvalidProgramException("Unsupported operating system detected. Please use a diffrent operating system or run this application in a Virtual Machine. Shutting Down.");
					}
				}
				else
				{
					Console.WriteLine(
						"Debugger detected. Not opening browser to avoid issues. " +
						"You may manually connect to the needed port manually via http://localhost:5000 or https://localhost:5001."
					);
				}
			});
			CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseIISIntegration().UseStartup<Startup>();
                });
    }
}
