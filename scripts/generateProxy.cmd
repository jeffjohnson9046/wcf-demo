::------------------------------------------------------------------------------------------------
:: This script will generate a .cs file containing the proxy/client classes to consume the WCF
:: Service Demo.  It will also generate a config file containing all the necessary configuration
:: information for the consumer to communicate with the service.
::
:: This script is not required by the service itself.  This script should be run by any .NET 
:: applications that plan on *consuming* the service (e.g. an MVC web app, a windows forms app, etc.)
::
:: Just change the uri below to the address configured in the service's app.config file.
::
:: NOTE:  This script must be run from the Visual Studio 20[xx] command line, just running the
:: script from the normal Windows command line won't work**.
::
:: ** = unless you have the .NET stuff in your PATH or you're already in the directory where
:: the installutil executable lives.
::------------------------------------------------------------------------------------------------

svcutil net.tcp://localhost:13001 /out:wcfdemo.cs /language:cs