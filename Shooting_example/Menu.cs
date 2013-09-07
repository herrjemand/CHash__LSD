using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LSD
{
    class Menu_Ell
    {
        // declare fields to use in the class
        private short rad, f_size;//variables for the rectangle
        private string txxt;
        private Point loc;
        private Rectangle MenuRec;
        private Color colour = Color.White;

        public Menu_Ell()
        {
            #region "default configs"
            loc = new Point(10, 10);
            rad = 150;
            f_size = 35;
            MenuRec = new Rectangle(loc.X, loc.Y, rad, rad);
            #endregion
        }

        // Methods for the Planet class
        public void menu_draw(Graphics g)
        {
            MenuRec = new Rectangle(loc.X, loc.Y, rad, rad);
            g.DrawEllipse(new Pen(colour, 4F), loc.X, loc.Y, rad, rad);
            g.DrawString(txxt, new Font("Arial", f_size), new SolidBrush(Color.White), loc.X + rad / 2, loc.Y + rad / 2, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
        }
        public void gen_colour(short i) {
           colour = Color.FromArgb(255, 51 * i, 255 - 51 * i, 0);
        }
        public void rnd_colour(){
            colour = Program.rand_colour();
        }
        #region Delta
        public Point delta_loc {
            get { 
                return loc;
            }
            set {
                loc = value;
            }
        }
        public string delta_txt
        {
            get {
                return txxt;
            }
            set {
                txxt = value;
            }
        }
        public short delta_font_size
        {
            get
            {
                return f_size;
            }
            set
            {
                f_size = value;
            }
        }
        #endregion
        #region Gamma
        public Rectangle gamma_menu_elips{
            get{
                return MenuRec;
            }
        }
        #endregion
    }
}
