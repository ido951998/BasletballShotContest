using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BasketballShotContest
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region data
        string s = "";
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        Texture2D ballTex;
        Texture2D basketTex;
        Texture2D arrowTex;
        Texture2D strength;
        Texture2D bg;
        Vector2 pos1 = new Vector2(1500, 700);
        Vector2 pos2 = new Vector2(100, 700);
        SpriteFont font;
        int screenWidth;
        int screenHeight;
        float rotation2 = -0.785f;
        float rotation1 = -2.356f;
        int strength1 = 0;
        int strength2 = 0;
        int score1 = 0;
        int score2 = 0;
        bool isfly1 = false;
        bool isfly2 = false;
        static public KeyboardState keyboardState;
        static public KeyboardState previousState;
        Random rnd = new Random();
        string usrername1;
        string username2;
        //private NetworkStream conn;
        //private TcpClient client;

        #endregion

        static class NativeMethods
        {

            [DllImport("USER32.dll")]
         public    static extern short GetKeyState(VirtualKeyStates nVirtKey);

        public     enum VirtualKeyStates : int
            {
                VK_LBUTTON = 0x01,
                VK_RBUTTON = 0x02,
                VK_CANCEL = 0x03,
                VK_MBUTTON = 0x04,
                //
                VK_XBUTTON1 = 0x05,
                VK_XBUTTON2 = 0x06,
                //
                VK_BACK = 0x08,
                VK_TAB = 0x09,
                //
                VK_CLEAR = 0x0C,
                VK_RETURN = 0x0D,
                //
                VK_SHIFT = 0x10,
                VK_CONTROL = 0x11,
                VK_MENU = 0x12,
                VK_PAUSE = 0x13,
                VK_CAPITAL = 0x14,
                //
                VK_KANA = 0x15,
                VK_HANGEUL = 0x15,  /* old name - should be here for compatibility */
                VK_HANGUL = 0x15,
                VK_JUNJA = 0x17,
                VK_FINAL = 0x18,
                VK_HANJA = 0x19,
                VK_KANJI = 0x19,
                //
                VK_ESCAPE = 0x1B,
                //
                VK_CONVERT = 0x1C,
                VK_NONCONVERT = 0x1D,
                VK_ACCEPT = 0x1E,
                VK_MODECHANGE = 0x1F,
                //
                VK_SPACE = 0x20,
                VK_PRIOR = 0x21,
                VK_NEXT = 0x22,
                VK_END = 0x23,
                VK_HOME = 0x24,
                VK_LEFT = 0x25,
                VK_UP = 0x26,
                VK_RIGHT = 0x27,
                VK_DOWN = 0x28,
                VK_SELECT = 0x29,
                VK_PRINT = 0x2A,
                VK_EXECUTE = 0x2B,
                VK_SNAPSHOT = 0x2C,
                VK_INSERT = 0x2D,
                VK_DELETE = 0x2E,
                VK_HELP = 0x2F,
                //
                VK_LWIN = 0x5B,
                VK_RWIN = 0x5C,
                VK_APPS = 0x5D,
                //
                VK_SLEEP = 0x5F,
                //
                VK_NUMPAD0 = 0x60,
                VK_NUMPAD1 = 0x61,
                VK_NUMPAD2 = 0x62,
                VK_NUMPAD3 = 0x63,
                VK_NUMPAD4 = 0x64,
                VK_NUMPAD5 = 0x65,
                VK_NUMPAD6 = 0x66,
                VK_NUMPAD7 = 0x67,
                VK_NUMPAD8 = 0x68,
                VK_NUMPAD9 = 0x69,
                VK_MULTIPLY = 0x6A,
                VK_ADD = 0x6B,
                VK_SEPARATOR = 0x6C,
                VK_SUBTRACT = 0x6D,
                VK_DECIMAL = 0x6E,
                VK_DIVIDE = 0x6F,
                VK_F1 = 0x70,
                VK_F2 = 0x71,
                VK_F3 = 0x72,
                VK_F4 = 0x73,
                VK_F5 = 0x74,
                VK_F6 = 0x75,
                VK_F7 = 0x76,
                VK_F8 = 0x77,
                VK_F9 = 0x78,
                VK_F10 = 0x79,
                VK_F11 = 0x7A,
                VK_F12 = 0x7B,
                VK_F13 = 0x7C,
                VK_F14 = 0x7D,
                VK_F15 = 0x7E,
                VK_F16 = 0x7F,
                VK_F17 = 0x80,
                VK_F18 = 0x81,
                VK_F19 = 0x82,
                VK_F20 = 0x83,
                VK_F21 = 0x84,
                VK_F22 = 0x85,
                VK_F23 = 0x86,
                VK_F24 = 0x87,
                //
                VK_NUMLOCK = 0x90,
                VK_SCROLL = 0x91,
                //
                VK_OEM_NEC_EQUAL = 0x92,   // '=' key on numpad
                //
                VK_OEM_FJ_JISHO = 0x92,   // 'Dictionary' key
                VK_OEM_FJ_MASSHOU = 0x93,   // 'Unregister word' key
                VK_OEM_FJ_TOUROKU = 0x94,   // 'Register word' key
                VK_OEM_FJ_LOYA = 0x95,   // 'Left OYAYUBI' key
                VK_OEM_FJ_ROYA = 0x96,   // 'Right OYAYUBI' key
                //
                VK_LSHIFT = 0xA0,
                VK_RSHIFT = 0xA1,
                VK_LCONTROL = 0xA2,
                VK_RCONTROL = 0xA3,
                VK_LMENU = 0xA4,
                VK_RMENU = 0xA5,
                //
                VK_BROWSER_BACK = 0xA6,
                VK_BROWSER_FORWARD = 0xA7,
                VK_BROWSER_REFRESH = 0xA8,
                VK_BROWSER_STOP = 0xA9,
                VK_BROWSER_SEARCH = 0xAA,
                VK_BROWSER_FAVORITES = 0xAB,
                VK_BROWSER_HOME = 0xAC,
                //
                VK_VOLUME_MUTE = 0xAD,
                VK_VOLUME_DOWN = 0xAE,
                VK_VOLUME_UP = 0xAF,
                VK_MEDIA_NEXT_TRACK = 0xB0,
                VK_MEDIA_PREV_TRACK = 0xB1,
                VK_MEDIA_STOP = 0xB2,
                VK_MEDIA_PLAY_PAUSE = 0xB3,
                VK_LAUNCH_MAIL = 0xB4,
                VK_LAUNCH_MEDIA_SELECT = 0xB5,
                VK_LAUNCH_APP1 = 0xB6,
                VK_LAUNCH_APP2 = 0xB7,
                //
                VK_OEM_1 = 0xBA,   // ';:' for US
                VK_OEM_PLUS = 0xBB,   // '+' any country
                VK_OEM_COMMA = 0xBC,   // ',' any country
                VK_OEM_MINUS = 0xBD,   // '-' any country
                VK_OEM_PERIOD = 0xBE,   // '.' any country
                VK_OEM_2 = 0xBF,   // '/?' for US
                VK_OEM_3 = 0xC0,   // '`~' for US
                //
                VK_OEM_4 = 0xDB,  //  '[{' for US
                VK_OEM_5 = 0xDC,  //  '\|' for US
                VK_OEM_6 = 0xDD,  //  ']}' for US
                VK_OEM_7 = 0xDE,  //  ''"' for US
                VK_OEM_8 = 0xDF,
                //
                VK_OEM_AX = 0xE1,  //  'AX' key on Japanese AX kbd
                VK_OEM_102 = 0xE2,  //  "<>" or "\|" on RT 102-key kbd.
                VK_ICO_HELP = 0xE3,  //  Help key on ICO
                VK_ICO_00 = 0xE4,  //  00 key on ICO
                //
                VK_PROCESSKEY = 0xE5,
                //
                VK_ICO_CLEAR = 0xE6,
                //
                VK_PACKET = 0xE7,
                //
                VK_OEM_RESET = 0xE9,
                VK_OEM_JUMP = 0xEA,
                VK_OEM_PA1 = 0xEB,
                VK_OEM_PA2 = 0xEC,
                VK_OEM_PA3 = 0xED,
                VK_OEM_WSCTRL = 0xEE,
                VK_OEM_CUSEL = 0xEF,
                VK_OEM_ATTN = 0xF0,
                VK_OEM_FINISH = 0xF1,
                VK_OEM_COPY = 0xF2,
                VK_OEM_AUTO = 0xF3,
                VK_OEM_ENLW = 0xF4,
                VK_OEM_BACKTAB = 0xF5,
                //
                VK_ATTN = 0xF6,
                VK_CRSEL = 0xF7,
                VK_EXSEL = 0xF8,
                VK_EREOF = 0xF9,
                VK_PLAY = 0xFA,
                VK_ZOOM = 0xFB,
                VK_NONAME = 0xFC,
                VK_PA1 = 0xFD,
                VK_OEM_CLEAR = 0xFE
            }

        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


            //client = new TcpClient();
            //client.Connect(Program.Host, 5002);
            //conn = client.GetStream();

            new Thread(Listen).Start();
        //    new Thread(Key).Start();
        }
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1700;
            graphics.PreferredBackBufferHeight = 900;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);


            device = graphics.GraphicsDevice;
            font = Content.Load<SpriteFont>("font");

            ballTex = Content.Load<Texture2D>("Images/ball");
            basketTex = Content.Load<Texture2D>("Images/basket");
            arrowTex = Content.Load<Texture2D>("Images/arrow");
            strength = Content.Load<Texture2D>("Images/strength");
            bg = Content.Load<Texture2D>("Images/background");
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;
        }
        public void Listen()
        {
            while (true)
            {
                try {
                    // System.Windows.Forms.MessageBox.Show("Listening...........");
                    Debug.WriteLine("Listening....");
                    byte[] buffer = new byte[Program.Socket.ReceiveBufferSize];
                    //var conn = Program.Socket.GetStream();
                    int data = Program.Stream.Read(buffer, 0, Program.Socket.ReceiveBufferSize);
                    string ch = Encoding.Unicode.GetString(buffer, 0, data);

                    //   System.Windows.Forms.MessageBox.Show("Received: " + ch);

                    char[] c = { ',' };
                    string[] vars = ch.Split(c);
                    pos1.X = float.Parse(vars[0]);
                    pos2.X = float.Parse(vars[1]);
                    pos1.Y = float.Parse(vars[2]);
                    pos2.Y = float.Parse(vars[3]);
                    rotation1 = float.Parse(vars[4]);
                    rotation2 = float.Parse(vars[5]);
                    strength1 = int.Parse(vars[6]);
                    strength2 = int.Parse(vars[7]);
                    score1 = int.Parse(vars[8]);
                    score2 = int.Parse(vars[9]);
                    usrername1 = vars[10];
                    username2 = vars[11];
                    isfly1 = bool.Parse(vars[12]);
                    isfly2 = bool.Parse(vars[13]);

                }
                catch { }
            }
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
             Key();
            //Window.Title = keyboardState.ToString();
            base.Update(gameTime);
        }


        bool IsKeyPressed(NativeMethods.VirtualKeyStates key)
        {
            var r = NativeMethods.GetKeyState(key);
            return r != 1 && r != 0;
        }


        public void Key()
        {
            // while (true)

            // System.Windows.Forms.MessageBox.Show(((int)NativeMethods.VirtualKeyStates.VK_SPACE).ToString());
            //   var key = NativeMethods.GetKeyState(NativeMethods.VirtualKeyStates.VK_NUMPAD6);
            // Debug.WriteLine(key.ToString());
            if (IsKeyPressed(NativeMethods.VirtualKeyStates.VK_NUMPAD2))
            {
                Program.SendByte((byte)2);
            }
            else
                if (IsKeyPressed(NativeMethods.VirtualKeyStates.VK_NUMPAD6))
            {
                Program.SendByte((byte)6);
            }

            else
                    if (IsKeyPressed(NativeMethods.VirtualKeyStates.VK_NUMPAD4))
            {
                Program.SendByte((byte)4);
            }
            else
                if (IsKeyPressed(NativeMethods.VirtualKeyStates.VK_NUMPAD8))
            {
                Program.SendByte((byte)8);
            }
            else
                if (IsKeyPressed(NativeMethods.VirtualKeyStates.VK_NUMPAD5))
            {
                Program.SendByte((byte)5);
            }



            //   //var conn = Program.Socket.GetStream();
            //   previousState = keyboardState;
            //   keyboardState = Keyboard.GetState();


            ////   Debug.WriteLine("check");
            // //  var pressedKeys = keyboardState.GetPressedKeys();
            ////   Debug.WriteLine("pass 1");
            // //  if (pressedKeys.Length == 0) return;
            // //  Debug.WriteLine("pass 2");
            //   //if (keyboardState.GetPressedKeys().Length!=0)
            //   //{
            //   //    string a = keyboardState.ToString();
            //   //    Debug.WriteLine("Key: " + a);

            //   //}

            // //  Debug.WriteLine("Something is pressed");



            //   if (keyboardState.IsKeyDown(Keys.Space))
            //   {
            //       Debug.WriteLine("Space Detected!");
            //       Program.SendByte((byte)2);
            //   }
            //   if (keyboardState.IsKeyDown(Keys.NumPad4))
            //       Program.SendByte((byte)4);
            //   if (keyboardState.IsKeyDown(Keys.NumPad6))
            //       Program.SendByte((byte)6);
            //   if (keyboardState.IsKeyDown(Keys.NumPad8))
            //       Program.SendByte((byte)8);
            //   if (keyboardState.IsKeyDown(Keys.NumPad5))
            //       Program.SendByte((byte)5);

            //   // Thread.Sleep(10);

            // System.Windows.Forms.MessageBox.Show("Sent"); 
        }
      
        protected override void Draw(GameTime gameTime)
        {
          
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            Rectangle rec1 = new Rectangle((int)pos2.X+50, (int)pos2.Y+50, 250, 50);
            Rectangle rec2 = new Rectangle((int)pos1.X + 50, (int)pos1.Y + 50, 250, 50);
            Rectangle rec3 = new Rectangle((int)pos2.X + 50, (int)pos2.Y + 50, 250 * strength2 / 299, 50);
            Rectangle rec4 = new Rectangle((int)pos1.X + 50, (int)pos1.Y + 50, 250 * strength1 / 299, 50);
            spriteBatch.Draw(bg, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            spriteBatch.DrawString(font, usrername1 + ": " + score1, new Vector2(900, 50), Color.Red, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, username2 + ": " + score2, new Vector2(700, 50), Color.Red, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0f);
            //spriteBatch.DrawString(font, s , new Vector2(0, 20), Color.Blue, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0f);
            spriteBatch.Draw(ballTex, new Rectangle((int)pos2.X, (int)pos2.Y, 100, 100), Color.White);
            spriteBatch.Draw(ballTex, new Rectangle((int)pos1.X, (int)pos1.Y, 100, 100), Color.White);
            spriteBatch.Draw(basketTex, new Rectangle(screenWidth / 2 - 125, 100, 250, 250), Color.White);

            if (!isfly2)
            {
                spriteBatch.Draw(arrowTex, rec1, new Rectangle(0, 0, 299, 228), Color.White, rotation2, new Vector2(-50, 50), SpriteEffects.None, 0f);
                spriteBatch.Draw(strength, rec3, new Rectangle(0, 0, strength2, 228), Color.White, rotation2, new Vector2(-50, 50), SpriteEffects.None, 0f);
            }
            if (!isfly1)
            {
                spriteBatch.Draw(arrowTex, rec2, new Rectangle(0, 0, 299, 228), Color.White, rotation1, new Vector2(-50, 50), SpriteEffects.None, 0f);
                spriteBatch.Draw(strength, rec4, new Rectangle(0, 0, strength1, 228), Color.White, rotation1, new Vector2(-50, 50), SpriteEffects.None, 0f);
            }
            spriteBatch.End();

            base.Draw(gameTime);

        }
    }
}
