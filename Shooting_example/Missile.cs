using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LSD
{
    class Missile
    {
        short ymxc = 0, ymxc_c, rad;
          Point coord;
          Color colour;
        private Rectangle missileRec;//variable for a rectangle to place our image in

         public Missile(Rectangle incoRec)
        {
            rad = 10;
            coord = new Point(incoRec.X - rad, incoRec.Y + (incoRec.Height - rad) / 2);
            colour = Color.FromArgb(255, Program.GetRandomNumber(0, 255), Program.GetRandomNumber(0, 255), Program.GetRandomNumber(0, 255));
            missileRec = new Rectangle(coord.X, coord.Y, rad, rad);
        }
     
        public void draw(Graphics g)
        {
           coord = new Point(coord.X - 2, coord.Y);
           missileRec = new Rectangle(coord.X, coord.Y, rad, rad);
           g.DrawEllipse(new Pen(colour, 4F), coord.X, coord.Y, rad, rad);
        }

        public Rectangle MissileRec
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
