using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroMQ;

namespace LBBrokerClient_111520
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new ZContext();
            LBBroker_Client(context, 0);
            Console.Read();
        }

        static void LBBroker_Client(ZContext context, int i)
        {
            // Create a socket
            using (var client = new ZSocket(context, ZSocketType.REQ))
            {
                // Set a printable identity
                client.IdentityString = "CLIENT" + i;

                // Connect
                client.Connect("tcp://127.0.0.1:6101");

                using (var request = new ZMessage())
                {
                    request.Add(new ZFrame("Hellooo"));

                    // Send request
                    client.Send(request);
                }

                // Receive reply
                using (ZMessage reply = client.ReceiveMessage())
                {
                    Console.WriteLine("CLIENT{0}: {1}", i, reply[0].ReadString());
                }
            }
        }
    }
}
