using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WCF.Service.Demo.UnitTests
{
    [TestClass]
    public class ConcurrentQueueTests
    {
        [TestMethod]
        public void Can_Use_Any_Method_to_Determine_if_Items_Are_In_the_Queue()
        {
            var queue = new ConcurrentQueue<string>();

            for (int i = 0; i <= 4; i++)
            {
                var msg = string.Format("I am item {0}", i);
                queue.Enqueue(msg);
            }

            Assert.AreEqual(true, queue.Any());
        }
    }
}
