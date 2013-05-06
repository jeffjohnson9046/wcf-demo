using System;
using System.Collections.Concurrent;
using System.ServiceModel;
using System.Threading;

namespace WCF.Service.Demo
{
    /// <summary>
    /// The service that handles incoming payment authorization/settlement requests from customers. 
    /// </summary>
    /// <remarks>
    /// The <c>ServiceBehavior</c> attribute specifies how WCF should run the service:
    /// -- <c>InstanceContextMode.Single</c>:  WCF will maintain a single instance of the <c>PaymentService</c> class
    ///     to handle ALL incoming requests (effectively acting like a singleton).  While not very scalable, this 
    ///     method makes managing the queue of requests pretty simple.
    /// 
    /// -- <c>ConcurrencyMode.Multiple</c>:  WCF will handle multiple requests simultaneously, using one thread per
    ///     request.
    /// </remarks>
    /// <see cref="http://www.codeproject.com/Articles/89858/WCF-Concurrency-Single-Multiple-and-Reentrant-and"/>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class PaymentService : IPaymentService
    {
        private readonly ConcurrentQueue<string> _queue = new ConcurrentQueue<string>(); 
        private readonly Semaphore _worker = new Semaphore(0, 1);

        /// <summary>
        /// Handle an incoming Prosa request from a customer.
        /// </summary>
        /// <param name="client">The data from the customer required to make a successful call to the Prosa
        /// component.</param>
        public void Process(string client)
        {
            try
            {
                var prosa = new ProsaProcessor(_queue, _worker);

                _queue.Enqueue(client);

                // Start request processor on a background thread.
                ThreadPool.QueueUserWorkItem(prosa.ProcessRequests);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
