using System;
using System.ComponentModel;
using System.ServiceProcess;

namespace WCF.Service.Demo.WindowsService
{
    [RunInstaller(true)]
    public partial class WCFDemoServiceInstaller : System.Configuration.Install.Installer
    {
        public WCFDemoServiceInstaller()
        {
            var processInstaller = new ServiceProcessInstaller();
            var serviceInstaller = new ServiceInstaller();

            processInstaller.Account = ServiceAccount.NetworkService;
            serviceInstaller.DisplayName = "ResortCom Prototype Service";
            serviceInstaller.Description = "Allows callers to make a call to the Prosa component for credit card authorization/settlement.";
            serviceInstaller.ServiceName = "WCF.Service.Demo";
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }

       private void WCFDemoServiceInstaller_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e) { }
    }
}
