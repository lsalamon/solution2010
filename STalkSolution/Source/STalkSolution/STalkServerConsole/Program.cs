using System;
using System.Collections.Generic;
using System.Text;
using STalkServer;

namespace STalkServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IMServer server = new IMServer();
            server.Start();
            Console.ReadLine();
        }
    }
}
