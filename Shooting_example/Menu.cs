using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LSD
{
    class Menu_Ell
    {
        private short rad, f_size;//Radius and font size
        private string txxt;//txt holder
        private Point loc;//position
        private Rectangle MenuRec;//rectangle
        private Color colour;//colour holder

        public Menu_Ell()//default setups
        {
            loc = new Point(10, 10);
            rad = 150;
            f_size = 35;
            MenuRec = new Rectangle(loc.X, loc.Y, rad, rad);
        }
        public void menu_draw(Graphics g)
        {
            MenuRec = new Rectangle(loc.X, loc.Y, rad, rad);//sets rectangle
            g.DrawEllipse(new Pen(colour, 4F), loc.X, loc.Y, rad, rad);//draw circle
            g.DrawString(txxt, new Font("Arial", f_size), new SolidBrush(Color.White), 
                loc.X + rad / 2, 
                loc.Y + rad / 2, 
                new StringFormat() {
                    LineAlignment = StringAlignment.Center, 
                    Alignment = StringAlignment.Center 
                });//draw text
        }
        public void gen_colour(short i) {//generates colour green -> red
           colour = Color.FromArgb(255, 51 * i, 255 - 51 * i, 0);
        }
        public void rnd_colour(){//gets random colour
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
        public Rectangle rec{
            get{
                return MenuRec;
            }
        }
        #endregion
    }
}
