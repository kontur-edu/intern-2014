using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Client;
using Client.Parameters;
using HighLevelClient;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Test
    {
        private IPEndPoint ipEndPoint1;
        private IPEndPoint ipEndPoint2;
        private IPEndPoint ipEndPoint3;
        private EnterpriseClient enterpriseClient;

        [SetUp]
        public void SetUp()
        {
            ipEndPoint1 = new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 15087);
            ipEndPoint2 = new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 15088);
            ipEndPoint3 = new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 15089);
            enterpriseClient = new EnterpriseClient(new[] {ipEndPoint1, ipEndPoint2, ipEndPoint3});
        }

        [Test]
        public void ClientWritesAndImmidietlyReads()
        {
            for(var i = 0; i < 10; ++i)
            {
                var key = Guid.NewGuid().ToString();
                var value = Guid.NewGuid().ToString();
                enterpriseClient.Write(key, value);
                var result = enterpriseClient.Read(key);
                Assert.AreEqual(value, result);
            }
        }

        [Test]
        public void ClientWritesAllAndReads()
        {
            var values = new List<string>();

            for (var i = 0; i < 10; ++i)
            {
                var key = Guid.NewGuid().ToString();
                var value = Guid.NewGuid().ToString();
                enterpriseClient.Write(key, value);
                values.Add(String.Join(" ", key, value));
                var result = enterpriseClient.Read(key);
                Assert.AreEqual(value, result);
            }

            for (var i = 0; i < 10; i++)
            {
                var key = values[i].Split(' ')[0];
                var value = values[i].Split(' ')[1];
                var result = enterpriseClient.Read(key);
                Assert.AreEqual(value, result);
            }

        }
    }
}
