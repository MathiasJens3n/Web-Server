using System.Net;
using System.Net.Sockets;
using Web_Server;
using Web_Server.Interfaces;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // Define the IP address and port to listen on
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); // Use your desired IP address
        int port = 5000;

        WebServer server = new(ipAddress, port);
        // Listens for client connection to server
        await Console.Out.WriteLineAsync(server.Listen());

        while (true)
        {
            // Accept incoming client connections
            server.AcceptClientConnection();
        }
    }
}