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
        private void connectBtn_Click(object sender, EventArgs e)
        {
            //tcpClient = new 
            if (addressBox.Text.Length != 0 && addressBox.Text.Length <= 15)
            {
                if ((new Regex(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")).IsMatch(addressBox.Text))
                {
                    addressBox.ForeColor = Color.Green;
                    String temp = addressBox.Text;
                    IPAddress ipAddress = IPAddress.Parse(temp);
                    int tcpPort = 11110;
                    tcpClient = new TcpClient(temp, tcpPort);
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
    }
}
