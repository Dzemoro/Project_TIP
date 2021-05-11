using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class SessionForm : Form
    {
        int port,listenPort;
        string nickname,udpaddress;
        
        public SessionForm()
        {
            InitializeComponent();
        }
        public SessionForm(int port, string nickname, string udpaddress,int listenPort)
        {
            InitializeComponent();
            this.port = port;
            this.nickname = nickname;
            this.udpaddress = udpaddress;
            this.label1.Text = "Talking to:" + nickname;
            this.listenPort = listenPort;

        }
    }
}
