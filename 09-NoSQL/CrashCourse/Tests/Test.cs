using System;
using System.Net;
using System.Threading;
using Client;
using Client.Parameters;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Test
    {
        private ServiceClient client;
        private IPEndPoint ipEndPoint1;
        private IPEndPoint ipEndPoint2;

        [SetUp]
        public void SetUp()
        {
            client = new ServiceClient();
            ipEndPoint1 = new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 5088);
            ipEndPoint2 = new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 5086);
        }

        [Test]
        public void Xxx()
        {
            client.Write("x", new Data{Value = "y", Version = 1}, ipEndPoint1);
            var res = client.Read("x", ipEndPoint1);
            Assert.AreEqual("y", res.Value);
            res = client.Read("x", ipEndPoint2);
            Assert.IsNull(res);
            Thread.Sleep(11000);
            res = client.Read("x", ipEndPoint2);
            Assert.AreEqual("y", res.Value);
        }

        [Test]
        public void Qqq()
        {
            client.Write("a", new Data {Value = "b", Version = 3}, ipEndPoint1);
            var value = client.Read("a", ipEndPoint1);
            Console.Out.WriteLine(value.Version);

            Thread.Sleep(20000);
            Console.Out.WriteLine("Start!!!!");

            Console.Out.WriteLine("Sleep 60");
            Thread.Sleep(60000);

            var anotherValue = client.Read("a", ipEndPoint2);
            Console.Out.WriteLine(anotherValue.Version);
        }
    }
}
