using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Web_Server.Interfaces;

namespace Web_Server
{
    //Class that represe
    internal class WebServer : IWebServer
    {
        private readonly TcpListener _listener;
        private readonly IPAddress _ipAddress;
        private readonly int _port;

        public WebServer(IPAddress ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
            _listener = new TcpListener(ipAddress, port);
        }

        /// <summary>
        /// Accepts client connections async, and handles them.
        /// </summary>
        public async void AcceptClientConnection()
        {
            try
            {
                TcpClient client = await _listener.AcceptTcpClientAsync();

                HandleClientRequest(client);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Listens for connecting clients using TCPListener.
        /// </summary>
        /// <returns>Listening ip and port</returns>
        public string Listen()
        {
            try
            {
                _listener.Start();
                return $"Server is listening on {_ipAddress}:{_port}";
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return "Socket error";
            }
        }

        /// <summary>
        /// Handles client requests, and sends a "Hello World" response back.
        /// </summary>
        private async Task HandleClientRequest(TcpClient client)
        {
            try
            {
                using (NetworkStream networkStream = client.GetStream())
                {
                    // Buffer to store incoming request
                    byte[] requestBuffer = new byte[1024];
                    // Incoming request represented as bytes
                    int bytesRead = await networkStream.ReadAsync(requestBuffer, 0, requestBuffer.Length);
                    // Convert incoming request to string to print in console
                    string request = Encoding.ASCII.GetString(requestBuffer, 0, bytesRead);
                    Console.WriteLine($"Received request:\n{request}");

                    // Creating a http response to send back to the client, and convert it to bytes
                    string response = "HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\n\r\nHello, World!";
                    byte[] responseBytes = Encoding.ASCII.GetBytes(response);

                    // Send the response back to the client
                    await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);

                    // Close the client connection
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}