using System;
using System.ServiceProcess;

namespace WCF.Service.Demo.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <remarks>
        /// <para>
        /// NOTE:  The <c>Environment.UserInteractive</c> stuff is somewhat obsolete.  When you 
        /// run a WCF service from the IDE (i.e. by pressing F5 or clicking "Run"), Visual Studio
        /// will launch a WCF test client.  Therefore, there's really no need to run the service as
        /// a console application.  However, if the Visual Studio test harness bothers you or you 
        /// REALLY wanna run the service as a console app, there's a way to turn off the WCF test
        /// client that Visual Studio launches:  
        /// http://stackoverflow.com/questions/8441708/wcf-how-to-disable-wcf-test-client
        /// </para>
        /// <para>
        /// If you run the service from the IDE (i.e. by pressing F5), the service will
        /// start up as a console application.  Otherwise, the service will run as a normal
        /// Windows service.
        /// </para>
        /// </remarks>
        static void Main()
        {
            var servicesToRun = new ServiceBase[] { new WCFDemoServiceHost() };

            if (Environment.UserInteractive)
            {
                var service = new WCFDemoServiceHost();
                var emptyArgs = new string[1];
                emptyArgs[0] = string.Empty;

                service.StartAsConsoleApp(emptyArgs);
                Console.WriteLine("Press any key to terminate the program.");
                Console.ReadKey();
                service.Stop();
            }
            else
            {
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
