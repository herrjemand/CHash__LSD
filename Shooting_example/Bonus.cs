using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
namespace LSD
{
    class Bonus
    {
        short a;
        Color colour;
        private Rectangle missileRec;//variable for a rectangle to place our image in

        public Bonus(Rectangle incoRec)
        {
            a = 45;
            colour = Color.FromArgb(255, Program.GetRandomNumber(0, 255), Program.GetRandomNumber(0, 255), Program.GetRandomNumber(0, 255));
            missileRec = new Rectangle(incoRec.X, incoRec.Y, a, a);
            gen_colour();
        }
        public void draw(Graphics g)
        {
           
            missileRec = new Rectangle(missileRec.X + 2, missileRec.Y, a, a);
            g.DrawPolygon(new Pen(colour), new Point[] { new Point(missileRec.X + a / 2, missileRec.Y), new Point(missileRec.X, missileRec.Y + a), new Point(missileRec.X + a, missileRec.Y + a) });
            g.DrawString("♆", new Font("Arial", 24), new SolidBrush(Color.White), missileRec.X - 2, missileRec.Y + a / 5, new StringFormat());
        }
        public void gen_colour() {
            colour = Program.rand_colour();
        }
        public Rectangle MissileRec
        {
            get
            {
                return missileRec;
            }
        }
    }
}
