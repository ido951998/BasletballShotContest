using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace BasketballShotContest
{
#if WINDOWS || XBOX
    static class Program
    {
        public const string Host = "LocalHost";
        public static readonly TcpClient Socket = new TcpClient();
        public static   NetworkStream Stream;

        public static Game1 game ;
       // private static readonly Form1 Form = new Form1(game);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Control.CheckForIllegalCrossThreadCalls = false;

            try
            {
                Socket.Connect(Host, 5003);
                Stream = Socket.GetStream();
            }
            catch
            {
                MessageBox.Show("Server is unreachable.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        
         //   }).Start();
       //    new Thread(() => Application.Run(Form)).Start();
         //  var thread =  new Thread(() => { (new Form1()).Show(); });
         

            Application.Run(new Form1());
         
       
            // new Thread(() => { Program.game.Run(); }).Start();
        }


        public static void SendByte(byte b)
        {
             
            Debug.WriteLine("Sending byte...");
            Stream.WriteByte(b);
        }
    }
#endif
}

