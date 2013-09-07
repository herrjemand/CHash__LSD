using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LSD
{
    class Triangle
    {
         private short width, height,bonus_shots,lifes,points;//variables for the rectangle
         private Point coord;
         private string user_name = "Yuriy";//DEBUG

        //private Image spaceship;//variable for the spaceship's image

        private Rectangle trinityRec;//variable for a rectangle to place our image in
       
        //Create a constructor (initialises the values of the fields)
        public Triangle()
        {
            width = 60;
            height = 60;
            lifes = 3;
            trinityRec = new Rectangle(coord.X, coord.Y, width, height);
        }
        public void trinity_draw(Graphics g)
        {
            trinityRec = new Rectangle(coord.X, coord.Y, width, height);
            g.FillPolygon(Brushes.Purple, new Point[] { new Point(coord.X, coord.Y + trinityRec.Height / 2), new Point(coord.X + trinityRec.Width, coord.Y), new Point(coord.X + trinityRec.Width, coord.Y + trinityRec.Height) });
        }

         public Rectangle trioRec
         {
             get
             {
                 return trinityRec;
             }
         }
         public Point delta_coord {
             get {
                 return coord;
             }
             set {
                 coord = value;
             }
         }
         public short bonus {
             get {
                 return bonus_shots;
             }
             set {
                 bonus_shots = value;
             }
         }
         public short lieben
         {
             get
             {
                 return lifes;
             }
             set
             {
                     lifes = value;
                     if (lifes > 10) { lifes = 10; }
              }
         }
         public short score
         {
             get
             {
                 return points;
             }
             set
             {
                 points = value;
             }
         }
         public string name
         {
             get
             {
                 return user_name;
             }
             set
             {
                 user_name = value;
             }
         }

    }
 }


