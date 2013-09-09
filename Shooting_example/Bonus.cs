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
        private double delta_i = 0;
        private Point trin_coord(short i) {
            return (new Point(
               BonusRec.X + (short)((a*4)/5 * Math.Sin((double)delta_i + ((i * Math.PI) / 3))) + (a/2),
               BonusRec.Y + (short)((a*4)/5 * Math.Cos((double)delta_i + ((i * Math.PI) / 3))) + (a/2)
               ));
        }
        public void draw(Graphics g)//drawing
        {
            for (short i = 0; i < 10; i++)
            {
                delta_i += 0.02;
                BonusRec = new Rectangle(BonusRec.X + 4, BonusRec.Y, a, a);//sets rectangle
                g.DrawPolygon(new Pen(colour), new Point[] { trin_coord(2), trin_coord(4), trin_coord(6) });//Drawing triangle
                g.DrawString("♆", new Font("Arial", 24), new SolidBrush(Color.White), BonusRec.X - 2, BonusRec.Y + a / 5, new StringFormat());//drawing icon inside
            }
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
