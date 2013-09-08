using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LSD
{
    class Cube //The enemy class
    {

        private short a; //Size of a size
        private Point loc;//Position
        private bool up, visible, bonus;//Is going up? Is visible? Is this cube contains bonus?
        private Color colour;//Colour container

        private Rectangle CubeRec;//Cube rectangle
        
        public Cube()//default setup
        {
            up = true;
            visible = true;
            bonus = false;
            a = 45;
            rand_colour();//Generate random colour of cube
            loc = new Point(10, 10);
            CubeRec = new Rectangle(loc.X, loc.Y, a, a);
        }

        //Methods for cube drawing
        public void cube_draw(Graphics g)
        {
            CubeRec = new Rectangle(loc.X, loc.Y, a, a);//Sets rectangle
            g.FillRectangle(new SolidBrush(colour), loc.X, loc.Y, a, a);//Draw cube
        }
        public void rand_colour(){//gets random colour
            colour = Program.rand_colour();
        }
        #region "Delta"
        //Gets and sets values.
        public Point delta_loc{
            get{
                return loc;
            }set{
                loc = value;
            }
        }//sdas
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
        public Rectangle rec{
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
