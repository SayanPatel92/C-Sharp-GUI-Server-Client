using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TcpServerGUI
{
    public partial class Form1 : Form
    {
        private TcpListener listener;
        private ConcurrentDictionary<TcpClient, string> clients = new ConcurrentDictionary<TcpClient, string>();
        private bool isServerRunning = false; // To track server status

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnStartServer_Click(object sender, EventArgs e)
        {
            if (!isServerRunning)
            {
                // Start the server
                listener = new TcpListener(IPAddress.Any, 8888);
                listener.Start();
                txtServerIP.Text = GetLocalIPAddress();
                txtIncomingMessages.AppendText($"Server started. Listening for connections...{Environment.NewLine}");
                isServerRunning = true;
                btnStartServer.Text = "Stop Server";

                // Accept clients asynchronously
                await Task.Run(async () =>
                {
                    while (isServerRunning)
                    {
                        try
                        {
                            TcpClient client = await listener.AcceptTcpClientAsync();
                            if (client != null && isServerRunning)
                            {
                                clients.TryAdd(client, client.Client.RemoteEndPoint.ToString());
                                txtIncomingMessages.Invoke(new Action(() =>
                                    txtIncomingMessages.AppendText($"Client connected: {client.Client.RemoteEndPoint}{Environment.NewLine}")));
                                HandleClient(client);
                            }
                        }
                        catch (ObjectDisposedException)
                        {
                            // Listener was stopped; ignore this exception
                            break;
                        }
                        catch (SocketException)
                        {
                            // Ignore exception when listener is stopped
                            break;
                        }
                    }
                });
            }
            else
            {
                // Stop the server
                isServerRunning = false;

                // Stop the listener
                if (listener != null)
                {
                    try
                    {
                        listener.Stop();
                    }
                    catch (Exception ex)
                    {
                        txtIncomingMessages.AppendText($"Error stopping listener: {ex.Message}{Environment.NewLine}");
                    }
                    txtIncomingMessages.AppendText($"Server stopped.{Environment.NewLine}");
                }

                // Disconnect all connected clients
                foreach (var client in clients.Keys)
                {
                    try
                    {
                        client.GetStream().Close();
                        client.Close();
                    }
                    catch (Exception)
                    {
                        // Ignore any errors during client disconnection
                    }
                }
                clients.Clear();

                btnStartServer.Text = "Start Server";
            }
        }

        private async void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            try
            {
                while (isServerRunning)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; // Client disconnected

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    txtIncomingMessages.Invoke(new Action(() =>
                        txtIncomingMessages.AppendText($"Client: {message}{Environment.NewLine}"))); // Display client message
                }
            }
            catch (ObjectDisposedException)
            {
                // Stream was closed; safely ignore this
            }
            catch (Exception ex)
            {
                txtIncomingMessages.Invoke(new Action(() =>
                    txtIncomingMessages.AppendText($"Error: {ex.Message}{Environment.NewLine}")));
            }
            finally
            {
                if (client != null)
                {
                    client.Close();
                    clients.TryRemove(client, out _);
                    txtIncomingMessages.Invoke(new Action(() =>
                        txtIncomingMessages.AppendText($"Client disconnected: {client.Client.RemoteEndPoint}{Environment.NewLine}")));
                }
            }
        }

        // This is the missing event handler for the 'Send' button (btnSendMessage)
        private async void btnSendMessage_Click(object sender, EventArgs e)
        {
            string message = txtMessageToSend.Text; // Get message from the TextBox
            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Please enter a message to send.");
                return; // Prevent sending empty messages
            }

            byte[] messageBytes = Encoding.UTF8.GetBytes($"Server: {message}");
            foreach (var client in clients.Keys)
            {
                NetworkStream stream = client.GetStream();
                await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
            }
            txtIncomingMessages.AppendText($"Server: {message}{Environment.NewLine}"); // Log message in server window
            txtMessageToSend.Clear(); // Clear the input TextBox after sending
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork) return ip.ToString();
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
