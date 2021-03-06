﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using NUnit.Framework;

using dlech.SshAgentLib;

namespace dlech.SshAgentLibTests
{
    [TestFixture()]
    [Platform(Include = "Win")]
    public class PageantClientTest
    {

        [StructLayout(LayoutKind.Sequential)]
        struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;
        }

        [Test()]
        public void SendMessageTest()
        {
            // TODO: Need to modify this test so that it does not use PageantAgent
            const string messageValue = "junk";

            var builder = new BlobBuilder ();
            builder.AddStringBlob(messageValue);
            var messageBytes = builder.GetBlob();

            using (var agent = new PageantAgent()) {
                var client = new PageantClient();
                var reply = client.SendMessage(messageBytes);
                var replyParser = new BlobParser(reply);
                var replyHeader = replyParser.ReadHeader();
                Assert.That(replyHeader.Message, Is.EqualTo(Agent.Message.SSH_AGENT_FAILURE));
            }
        }
    }
}
