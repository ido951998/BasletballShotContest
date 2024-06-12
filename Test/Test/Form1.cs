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
using Test;

namespace BasketballShotContest
{
    public partial class Form1 : Form
    {
        Game1 game;
        string path = "localhost";
        public string Username { get { return textBox1.Text; } }

        public Form1(Game1 game)
        {
            InitializeComponent();
            this.game = game;
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
         //   new Thread(() =>
         //   {
                TcpClient client = new TcpClient(path, 5002);
                NetworkStream n = client.GetStream();
                string ch = textBox1.Text;
                byte[] messege = Encoding.Unicode.GetBytes(ch);
                n.Write(messege, 0, messege.Length);
                //game.SetConnection(n, client);
                new Thread(() => { game.Run(); }).Start();
     //       }).Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectUser();
            //MessageBox.Show("Hiding");
            this.Hide();
        }
    }
}
