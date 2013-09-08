using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LSD
{
    class Missile//Missile class
    {
        private short ymxc = 0, ymxc_c, rad;//ymxc sets special shot direction, -1,1; ymxc_c set c in y=mx+c; radius
        private Point coord;//location
        private Color colour;//colour
        private Rectangle missileRec;//Rectangle

         public Missile(Rectangle incoRec)//default settings
        {
            rad = 10;
            coord = new Point(incoRec.X - rad, incoRec.Y + (incoRec.Height - rad) / 2);
            colour = Program.rand_colour();
            missileRec = new Rectangle(coord.X, coord.Y, rad, rad);
        }
     
        public void draw(Graphics g)//drawing
        {
           coord = new Point(coord.X - 2, coord.Y);
           missileRec = new Rectangle(coord.X, coord.Y, rad, rad);
           g.DrawEllipse(new Pen(colour, 4F), coord.X, coord.Y, rad, rad);
        }

        public Rectangle rec
        {
            get
            {
                return missileRec;
            }
        }
        public Point missile_point {
            get {
                return coord;
            }
            set {
                coord = value;
            }
        }
        public short delta_ymxc {
            get {
                return ymxc;
            }
            set {
                ymxc = value;
            }
        
        }
        public short delta_ymxc_c
        {
            get
            {
                return ymxc_c;
            }
            set
            {
                ymxc_c = value;
            }

        }
    }
}
