using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LSD
{
    class Cube
    {
        // declare fields to use in the class

        private short a;//variables for the rectangle
        //private Image planetImage;//variable for the planet's image
        private Point loc;
        private bool up, visible, bonus;
        private Rectangle CubeRec;//variable for a rectangle to place our image in
        private Color colour;

        //Create a constructor (initialises the values of the fields)
        public Cube()
        {
            up = true;
            visible = true;
            bonus = false;
            a = 45;
            rand_colour();
            loc = new Point(10, 10);
            CubeRec = new Rectangle(loc.X, loc.Y, a, a);
        }

        // Methods for the Planet class
        public void cube_draw(Graphics g)
        {
            CubeRec = new Rectangle(loc.X, loc.Y, a, a);

           // g.DrawImage(planetImage, planetRec);
            g.FillRectangle(new SolidBrush(colour), loc.X, loc.Y, a, a);
        }
        public void rand_colour(){
            colour = Color.FromArgb(255, (byte)Program.GetRandomNumber(0, 255), (byte)Program.GetRandomNumber(0, 255), (byte)Program.GetRandomNumber(0, 255));
        }
        #region Delta
        public Point delta_loc{
            get{
                return loc;
            }set{
                loc = value;
            }
        }

        public bool delta_visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }
        public bool delta_up{
            get{
                return up;
            }set{
                up = value;
            }
        }
        #endregion
        //property to read planetRec from the form to check for
        // a collision with the space rectangle in the tmrPlanet event
        #region Gamma
        public Rectangle gamma_cube_rec{
            get{
                return CubeRec;
            }
        }
        public bool delta_bonus {
            get {
                return bonus;

            }
            set {
                bonus = value;
            }
        }
        #endregion
    }
}
