using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TcpClientGUI
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private bool isClientConnected = false; // To track client connection status

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (!isClientConnected)
            {
                // Connect to the server
                string serverIP = txtServerIP.Text;
                try
                {
                    client = new TcpClient();
                    await client.ConnectAsync(serverIP, 8888); // Use ConnectAsync for async connection
                    stream = client.GetStream();
                    txtConnectionStatus.Text = "Connected to server!";
                    isClientConnected = true;
                    btnConnect.Text = "Disconnect";
                    ReceiveMessages(); // Start listening for messages from the server
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                // Disconnect from the server
                DisconnectClient();
            }
        }

        private void DisconnectClient()
        {
            try
            {
                if (stream != null)
                {
                    stream.Close();
                }
                if (client != null)
                {
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during disconnection: {ex.Message}");
            }
            finally
            {
                txtConnectionStatus.Text = "Disconnected from server.";
                isClientConnected = false;
                btnConnect.Text = "Connect";
            }
        }

        private async void ReceiveMessages()
        {
            byte[] buffer = new byte[1024];

            while (true)
            {
                try
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break; // Server disconnected

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    txtIncomingMessages.AppendText($"Server: {message}{Environment.NewLine}"); // Display server message
                }
                catch (ObjectDisposedException)
                {
                    // Stream was closed; safely ignore this
                    break;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection closed: {ex.Message}");
                    break;
                }
            }

            DisconnectClient();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (client == null || !client.Connected) return;

            string message = txtMessageToSend.Text; // Get message from the TextBox
            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Please enter a message to send.");
                return; // Prevent sending empty messages
            }

            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
            txtIncomingMessages.AppendText($"You: {message}{Environment.NewLine}"); // Show sent message
            txtMessageToSend.Clear(); // Clear the input TextBox after sending
        }
    }
}
