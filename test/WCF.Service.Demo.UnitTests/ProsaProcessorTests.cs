using System;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WCF.Service.Demo.UnitTests
{
    [TestClass]
    public class ProsaProcessorTests
    {
        [TestMethod]
        public void Can_Process_a_Single_Request()
        {
            var s = new Semaphore(0, 1);
            var q = new ConcurrentQueue<string>();
            q.Enqueue("Client 1");

            var processor = new ProsaProcessor(q, s);

            bool foo = ThreadPool.QueueUserWorkItem(processor.ProcessRequests);

            Assert.AreEqual(true, foo);
        }
    }
}
