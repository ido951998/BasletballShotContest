using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace BasketballShotContest
{
    public partial class Form1 : Form
    {
      //  string path = "localhost";
        public string Username { get { return textBox1.Text; } }

        public Form1()
        {
            InitializeComponent();
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void ConnectUser()
        {
            //    TcpClient client = new TcpClient();
            //    client.Connect("localhost", 5002); 
            //   NetworkStream n = client.GetStream();
            string ch = textBox1.Text;
            byte[] messege = Encoding.Unicode.GetBytes(ch);
            Program.Stream.Write(messege, 0, messege.Length);

            Debug.WriteLine("Sent Username");

          //  MessageBox.Show("Sent!");


            //   game.Run();
            ////   new Thread(() =>
            ////   {
            //       TcpClient client = new TcpClient(path, 5002);
            //       NetworkStream n = client.GetStream();
            //       string ch = textBox1.Text;
            //       byte[] messege = Encoding.Unicode.GetBytes(ch);
            //       n.Write(messege, 0, messege.Length);
            //       game.SetConnection(n, client);
            //       new Thread(() => { game.Run(); }).Start();
            //game.Run();
            //       }).Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectUser();


            new Thread(() =>
            {
                Program.game = new Game1();
                Program.game.Run();
            }).Start();       
            //   MessageBox.Show("Hiding");
            this.Hide();
        }
    }
}
