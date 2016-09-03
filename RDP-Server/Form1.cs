using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDP_Server
{
    public partial class Form1 : Form
    {
        private readonly TcpClient TC = new TcpClient();
        private NetworkStream NS;
        private int port;

        private static Image DesktopImage()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap screenShot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
            Graphics graphic = Graphics.FromImage(screenShot);
            graphic.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            return screenShot;
        }

        private void SendImage()
        {
            BinaryFormatter BF = new BinaryFormatter();
            NS = TC.GetStream();
            BF.Serialize(NS, DesktopImage());
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            port = int.Parse(textBox2.Text);
            try
            {
                TC.Connect(textBox1.Text, port);
                MessageBox.Show("Connected");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendImage();
        }
    }
}
