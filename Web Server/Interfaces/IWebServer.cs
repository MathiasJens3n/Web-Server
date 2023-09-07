using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Web_Server.Interfaces
{
    internal interface IWebServer
    {
        public void AcceptClientConnection();
        public string Listen();
    }
}
