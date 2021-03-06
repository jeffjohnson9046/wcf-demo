::------------------------------------------------------------------------------------------------
:: This script will shut down and uninstall your Windows Service, removing it from the 
:: Windows Services pane (accessible from the control panel or by typing services.msc at the 
:: command line).
::
:: Just change the path below to where-ever the .exe for your Windows Service lives.
::
:: NOTE:  This script must be run from the Visual Studio 20[xx] command line, just running the
:: script from the normal Windows command line won't work**.
::
:: ** = unless you have the .NET stuff in your PATH or you're already in the directory where
:: the installutil executable lives.
::------------------------------------------------------------------------------------------------

net stop "ResortCom Prototype Service"
installutil /u "C:\Development\GitHub\WCF.Service.Demo\src\WCF.Service.Demo.WindowsService\bin\Debug\WCF.Service.Demo.WindowsService.exe"
pause