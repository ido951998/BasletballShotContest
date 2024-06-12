using System;
using System.Windows.Forms;
using BasketballShotContest;
using System.Threading;

namespace Test
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [STAThread]
        static void Main(string[] args)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Application.EnableVisualStyles();
            Application.Run(new Form1(new Game1()));
          //  new Form1(new Game1()).ShowDialog();
            //new Thread(() => new Form1(new Game1()).Show());
            //using (Game1 game = new Game1())
            //{
            //    game.Run();
            //}
        }
    }
#endif
}

