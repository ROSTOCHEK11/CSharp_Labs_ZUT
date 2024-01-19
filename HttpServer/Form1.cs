using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace HttpServer
{
    public partial class Form1 : Form
    {
        private FolderBrowserDialog folderBrowserDialog;

        private TcpListener httpServer;
        private Thread serverThread;
        private string servingDirectory;
        private int port;

        public Form1()
        {
            InitializeComponent();
            stop_btn.Enabled = false;

            folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            serverLogs_txt.Text = "";

            try
            {
                port = int.Parse(port_txt.Text);
                servingDirectory = file_txt.Text;

                if (!Directory.Exists(servingDirectory))
                {
                    throw new Exception("The specified directory does not exist.");
                }

                httpServer = new TcpListener(IPAddress.Any, port);
                serverThread = new Thread(new ThreadStart(StartServer));
                serverThread.Start();

                start_btn.Enabled = false;
                stop_btn.Enabled = true;

                LogToServerLogs("Server started successfully on port " + port);
            }
            catch (Exception ex)
            {
                LogToServerLogs("Error starting server: " + ex.Message);
            }
        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (httpServer != null)
                {
                    httpServer.Stop();
                    serverThread?.Abort();

                    start_btn.Enabled = true;
                    stop_btn.Enabled = false;

                    LogToServerLogs("Server stopped successfully.");
                }

            }
            catch (Exception ex)
            {
                LogToServerLogs("Error stopping server: " + ex.Message);
            }
        }

        private void StartServer()
        {
            try
            {
                httpServer.Start();

                while (true)
                {
                    TcpClient client = httpServer.AcceptTcpClient();
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                    clientThread.Start(client);
                }
            }
            catch (Exception ex)
            {
                Invoke(new Action(() => serverLogs_txt.Text += $"Server error: {ex.Message}\n"));
            }
        }

        private void HandleClient(object obj)
        {
            TcpClient client = obj as TcpClient;
            if (client == null)
            {
                // Log error or handle the case where client is null
                return;
            }

            NetworkStream stream = null;

            try
            {

                stream = client.GetStream();

                if (stream == null)
                {
                    // Handle null stream
                    return;
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    string request = reader.ReadLine();

                    // Check if the request is null or empty
                    if (string.IsNullOrEmpty(request))
                    {
                        // Handle the case where request is null or empty
                        return;
                    }

                    // Process the request
                    if (request.StartsWith("GET"))
                    {
                        string[] tokens = request.Split(' ');
                        string requestedFile = tokens[1].TrimStart('/');

                        string filePath = Path.Combine(servingDirectory, string.IsNullOrEmpty(requestedFile) ? "index.html" : requestedFile);

                        if (File.Exists(filePath))
                        {
                            SendResponse(stream, File.ReadAllText(filePath), "200 OK");
                        }
                        else
                        {
                            SendResponse(stream, "<html><body><h2>404 Not Found</h2></body></html>", "404 Not Found");
                        }

                        LogToServerLogs("Received request: " + request);
                    }

                    LogToServerLogs("Sent response for request: " + request);
                }


            }
            catch (Exception ex)
            {
                LogToServerLogs("Error in HandleClient: " + ex.Message);

                if (stream != null)
                {
                    SendResponse(stream, "<html><body><h2>500 Internal Server Error</h2><p>An error occurred while processing your request.</p></body></html>", "500 Internal Server Error");
                }
            }
            finally
            {
                stream.Close();
                client.Close();
            }
        }

        private void SendResponse(NetworkStream stream, string body, string statusCode)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine($"HTTP/1.1 {statusCode}");
            writer.WriteLine("Content-Type: text/html; charset=utf-8");
            writer.WriteLine("Connection: close");
            writer.WriteLine("");
            writer.WriteLine(body);
            writer.Flush();
        }


        private void LogToServerLogs(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(LogToServerLogs), new object[] { message });
            }
            else
            {
                serverLogs_txt.AppendText(message + Environment.NewLine);
            }
        }

        private void file_btn_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected folder's path
                string folderPath = folderBrowserDialog.SelectedPath;

                // Set the directory path to the selected folder
                file_txt.Text = folderPath;
            }
        }
    }
}
