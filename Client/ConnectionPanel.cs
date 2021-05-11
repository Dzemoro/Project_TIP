using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
using ClientClassLib;

namespace ClientApp
{
    public partial class ConnectionPanel : Form
    {
        public ConnectionPanel()
        {
            InitializeComponent();
        }
        TcpClient tcpClient;
        Client client = new Client();
        ClientForm clientForm;
        private void connectBtn_Click(object sender, EventArgs e)
        {
            
            if (addressBox.Text.Length != 0 && addressBox.Text.Length <= 15)
            {
                if ((new Regex(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")).IsMatch(addressBox.Text))
                {
                        if (userBox.Text.Length == 0)
                        {
                            MessageBox.Show("Invalid Username");
                            return;
                        }

                        addressBox.ForeColor = Color.Green;
                        string temp = addressBox.Text;
                        Console.WriteLine(temp);
                        string msg = "HELL:" + getIPAddress() + ":8001:" + userBox.Text;
                    // IPAddress ipAddress = IPAddress.Parse(temp);
                    try
                    {
                        tcpClient = new TcpClient(temp, 8001);
                        msg = "HELL:" + getIPAddress() + ":8001:" + userBox.Text;
                        Console.WriteLine(msg);
      
                        Byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);
                        try
                        {
                            NetworkStream stream = tcpClient.GetStream();
       
                            stream.Write(data, 0, data.Length);
                            Console.WriteLine("Sent: {0}", msg);
                            //ODPOWIEDŹ
                            data = new Byte[256];
                            String responseData = String.Empty;
                            Int32 bytes = stream.Read(data, 0, data.Length);
                            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                            Console.WriteLine("Response:"+responseData);
                            string[] words = responseData.Split(':');

                            if(words[0]=="PORT" && Int32.Parse(words[1])>=1024 && Int32.Parse(words[1])<=65535)
                            {
                                msg = "OKAY:" + words[1];
                                data = System.Text.Encoding.ASCII.GetBytes(msg);
                                stream.Write(data, 0, data.Length);
                                clientForm = new ClientForm(Int32.Parse(words[1]), temp, userBox.Text,tcpClient);
                                //stream.Close();
                                //tcpClient.Close();
                                clientForm.Show();
                                this.Hide();

                            }
                            else
                            {
                                msg = "NACK";
                                Console.WriteLine("OOPS");
                                data = System.Text.Encoding.ASCII.GetBytes(msg);
                                stream.Write(data, 0, data.Length);
                                stream.Close();
                                tcpClient.Close();

                            }
                    
                            // ZAMYKANIE I OTWARCIE NA NOWYM PORCIE
                          
                       
                        }
                        catch (System.NullReferenceException exception)
                        {
                            Console.WriteLine("NULLReferenceException: {0}", exception);
                            MessageBox.Show("Nie znaleziono serwera o podanym adresie");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Another exception: {0}", ex);
                        }


                        /*
                        TcpListener l = new TcpListener(IPAddress.Loopback, 0);
                        l.Start();
                        int port = ((IPEndPoint)l.LocalEndpoint).Port;
                        Console.WriteLine(port);
                        l.Stop();

                        */
                    }
                    catch (SocketException se)
                    {
                        //Console.WriteLine("NULLReferenceException: {0}", se);
                        MessageBox.Show("Unable to connect to the server");
                    }


                }
                else
                {
                    addressBox.ForeColor = Color.Red;
                    MessageBox.Show("Invalid address");
                }
            }
            else
            {
                MessageBox.Show("Invalid address");
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void miniButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private IPAddress getIPAddress()
        {
            String strHostName = string.Empty;
            strHostName = Dns.GetHostName();
            IPHostEntry ipHostEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] address = ipHostEntry.AddressList;
            return address[4];
        }
    }
}
