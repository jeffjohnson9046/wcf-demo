using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;


namespace WCF.Service.Demo.WindowsService
{
    /// <summary>
    /// Use a Windows Service to host the <c>WCF.Service.Demo</c> service.
    /// </summary>
    /// <see cref="http://msdn.microsoft.com/en-us/library/ms733069.aspx"/>
    public partial class WCFDemoServiceHost : ServiceBase
    {
        private ServiceHost _wcfServiceDemoHost = null;
        private const string SERVICE_IDENTIFIER = "WCF.Service.Demo";

        public WCFDemoServiceHost()
        {
            this.ServiceName = SERVICE_IDENTIFIER;
            this.EventLog.Source = SERVICE_IDENTIFIER;
        }

        /// <summary>
        /// Stand up a new instance of the <c>WCF.Service.Demo.PaymentService</c> when the Windows service starts.
        /// </summary>
        /// <param name="args">Any arguments that should be passed to the start routine.  This program doesn't need
        /// any, so we don't do anything with them.</param>
        protected override void OnStart(string[] args)
        {
            // Write to Application Event Log that the service is starting.
            if (!Environment.UserInteractive)
            {
                this.EventLog.WriteEntry(string.Format("Starting {0}...", this.ServiceName), EventLogEntryType.Information);
            }

            // START THE SERVICE
            _wcfServiceDemoHost = new ServiceHost(typeof(PaymentService));
            _wcfServiceDemoHost.Faulted += new EventHandler(WcfServiceDemoHostFaulted);
            _wcfServiceDemoHost.Open();

            // Build the message
            var logMessage = new StringBuilder();
            logMessage.AppendFormat("{0} contains the following Service Hosts:\n\n", this.ServiceName);
            logMessage.AppendFormat("  {0}:\n", _wcfServiceDemoHost.Description.Name);
            foreach (Uri address in _wcfServiceDemoHost.BaseAddresses)
            {
                logMessage.AppendFormat("    Listening on {0}\n", address.AbsoluteUri);
            }

            // Write to the Application Event Log that the service has started.
            if (!Environment.UserInteractive)
            {
                this.EventLog.WriteEntry(logMessage.ToString(), EventLogEntryType.Information);
            }
        }

        /// <summary>
        /// Write to the Windows Application Event Log that the service has stopped.
        /// </summary>
        protected override void OnStop()
        {
            if (!Environment.UserInteractive)
            {
                this.EventLog.WriteEntry(string.Format("Stopping {0}...", this.ServiceName), EventLogEntryType.Information);
            }

            if (null != _wcfServiceDemoHost)
            {
                _wcfServiceDemoHost.Close();
            }

            if (!Environment.UserInteractive)
            {
                this.EventLog.WriteEntry(string.Format("{0} stopped.", this.ServiceName), EventLogEntryType.Information);
            }
        }

        /// <summary>
        /// If the Service has faulted, try to restart it once.  If the service won't restart, then log the failure to the Application Event Log.
        /// </summary>
        /// <remarks>
        /// There is definitely room for improvement in the exception handling for this service.  Generally, when the WCF service encounters a
        /// fatal error, you'll see some sort of entry that the service entered a "faulted state", and that's about it.  Hopefully the logging
        /// elsewhere in your application will provide some sort of indication of what went wrong. 
        /// </remarks>
        void WcfServiceDemoHostFaulted(object sender, EventArgs e)
        {
            if (!Environment.UserInteractive)
            {
                this.EventLog.WriteEntry(string.Format("The {0} Service has entered a faulted state.", this.ServiceName), EventLogEntryType.Error);
            }

            try
            {
                _wcfServiceDemoHost.Abort();
                _wcfServiceDemoHost = new ServiceHost(typeof(PaymentService));
                _wcfServiceDemoHost.Open();
            }
            catch (Exception ex)
            {
                var faultMessage = string.Format("Attempting to restart the {0} Service failed.  Exception Message: {1}\n\nStack Trace: {2}", this.ServiceName, ex.Message, ex.StackTrace);

                if (!Environment.UserInteractive)
                {
                    this.EventLog.WriteEntry(faultMessage, EventLogEntryType.Error);
                }
            }
        }

        #region Methods for Running Service as a Console Application

        /// <summary>
        /// Call the service's <c>OnStart</c> method when running the service as a console application.
        /// </summary>
        public void StartAsConsoleApp(string[] args)
        {
            this.OnStart(args);
        }

        /// <summary>
        /// Call the service's <c>OnStop</c> method when running the service as a console application.
        /// </summary>
        public void StopAsConsoleApp()
        {
            this.OnStop();
        }

        #endregion
    }
}