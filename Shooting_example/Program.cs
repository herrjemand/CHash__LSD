using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
namespace LSD
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Contains random generators
        /// </summary>
        /// 

        #region "Random"
        private static readonly Random getrandom = new Random(); private static readonly object syncLock = new object(); public static int rand_num(int min, int max) { lock (syncLock) { return getrandom.Next(min, max); } }
        public static Color rand_colour(){return Color.FromArgb(255, (byte)Program.rand_num(0, 255), (byte)Program.rand_num(0, 255), (byte)Program.rand_num(0, 255));}
        #endregion
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
        }
    }
}
