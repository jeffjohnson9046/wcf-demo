wcf-demo
========

A simple demo app of a WCF service hosted as a Windows Service.

Solution Structure
------------------
* **src:**  This folder contains all the source code for the WCF service.
* **test:**  This folder contains the unit test project.
* scripts:**  This folder contains the .cmd files to install/uninstall the WCF service on a Windows machine as a Windows Service.

Projects
--------
* **WCF.Service.Demo:**  This project contains all the source code for the WCF service.  If any changes to features /functionality need to occur, make them here.
* **WCF.Service.Demo.WindowsService:**  This project contains the classes necessary to host the WCF service as a Windows service.  Generally speaking, you won't need to make any changes to this project.  
* **WCF.Service.Demo.UnitTests:**  A unit test project for testing the WCF service functionality.

Scripts
-------
* **installService.cmd:**  installs/registers the WCF.Service.Demo.WindowsService.exe as a Windows Service.  That means you can interact with it from the "Services" pane in Windows.  By default, the Windows service will run as NETWORK_SERVICE, a low-privileged account.  These credentials can be changed in the Services pane.
* **UNinstallService.cmd:**  Removes WCF.Service.Demo.WindowsService.exe from the list of Windows services.
* **generateProxy.cmd:**  This script is meant for any .NET program that wants to consume/call our WCF service.  It generates a Client class and some configuration information that can be referenced by the calling program.
