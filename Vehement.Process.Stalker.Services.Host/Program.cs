using System;
using System.ServiceProcess;
using System.Threading;
using Vehement.Process.Stalker.Services.Host.ServiceStack;

namespace Vehement.Process.Stalker.Services.Host
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += MyHandler;

            var serviceManager = new ServiceStackManager(new ServiceStackHostFactory());

            if (args.Length > 0 && args[0].ToLower() == "debug")
            {
                serviceManager.Start();

                Console.WriteLine("Press <CTRL>+C to stop.");
                Thread.Sleep(Timeout.Infinite);
            }

            var servicesToRun = new ServiceBase[]
            {
                new ApiHostService(serviceManager)
            };

            ServiceBase.Run(servicesToRun);
        }


        private static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            var e = (Exception) args.ExceptionObject;
        }
    }
}

