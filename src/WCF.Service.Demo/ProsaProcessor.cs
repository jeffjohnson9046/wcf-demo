using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace WCF.Service.Demo
{
    internal class ProsaProcessor
    {
        private readonly ConcurrentQueue<string> _queue;
        private readonly Semaphore _worker;
 
        internal ProsaProcessor(ConcurrentQueue<string> queue, Semaphore worker)
        {
            _queue = queue;
            _worker = worker;
        }

        /// <summary>
        /// Look in the queue to see if there are any requests that need to be sent to the Prosa
        /// component.  If there are, ask the worker <c>Semaphore</c> for a thread to handle the
        /// processing.  Then go through each item in the queue and hand it off to Prosa for
        /// processing.
        /// </summary>
        /// <param name="stateData">Any state information (parameters, data, etc) that needs to be
        /// passed to the <c>ProcessRequest</c> method.</param>
        /// <remarks>
        /// Strictly speaking, the <c>ProcessRequests</c> method doesn't need any input arguments.
        /// This method is called from <c>PaymentService.Process</c> by means of 
        /// <c>ThreadPool.QueueUserWorkItem</c>.  The <c>QueueUserWorkItem</c> accepts a single
        /// argument of type <c>WaitCallback</c>.  The <c>WaitCallback</c> *also* takes a parameter -
        /// the method that you want to process on a background thread.  That method needs to have
        /// a signature that takes in a single argument of type <c>object</c>.  That's why the
        /// <c>ProcessRequests</c> method has the <c>stateData</c> argument but never does
        /// anything with it.
        /// </remarks>
        internal void ProcessRequests(object stateData)
        {
            try
            {
                _worker.WaitOne();

                // Because we're using a ConcurrentQueue, this read *should* be synchronized...
                if (!_queue.Any())
                {
                    return;
                }

                var clientToProcess = string.Empty;
                while (_queue.TryDequeue(out clientToProcess))
                {
                    this.HandleRequest(clientToProcess);
                }
            }
            catch (Exception)
            {
                // Do something ridiculously awesome to deal with the failure/exception.
                throw;
            }
            finally
            {
                _worker.Release();
            }

        }

        /// <summary>
        /// Call into ResortCom's Prosa component to authorize/settle a credit card
        /// request.
        /// </summary>
        /// <param name="clientToProcess">The data required to process a credit card
        /// authorization/setllement.</param>
        private void HandleRequest(string clientToProcess)
        {
            // Fill in the blanks:  do the Prosa request processing here.
            //throw new NotImplementedException();
            Console.WriteLine("I am from client {0}", clientToProcess);
        }
    }
}
