using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
namespace LSD
{
    class Bonus//Bonus class
    {
        short a;//Bonus size
        Color colour;//Colour container
        private Rectangle BonusRec;//variable for a rectangle to place our image in

        public Bonus(Rectangle ParentRec)//Gets rec of bonus cube
        {
            a = 45;//triangle size
            BonusRec = new Rectangle(ParentRec.X, ParentRec.Y, a, a);//sets rectangle
            gen_colour();
        }
        public void draw(Graphics g)//drawing
        {
            BonusRec = new Rectangle(BonusRec.X + 2, BonusRec.Y, a, a);//sets rectangle
            g.DrawPolygon(new Pen(colour), 
                new Point[] { new Point(BonusRec.X + a / 2, BonusRec.Y), 
                    new Point(BonusRec.X, BonusRec.Y + a), 
                    new Point(BonusRec.X + a, BonusRec.Y + a) 
                });//Drawing triangle
            g.DrawString("♆", new Font("Arial", 24), new SolidBrush(Color.White), BonusRec.X - 2, BonusRec.Y + a / 5, new StringFormat());//drawing icon inside
        }

        public void gen_colour() {//gets random colour
            colour = Program.rand_colour();
        }
        public Rectangle rec
        {
            get
            {
                return BonusRec;
            }
        }
    }
}
