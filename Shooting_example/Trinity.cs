using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LSD
{
    class Triangle
    {
         private short a,bonus_shots,lifes,points;//size of triangle,emount of bonus shots,amount of lifes(not more then 10),point;
         private Point coord;//location
         private string user_name;//username hoslder

        private Rectangle trinityRec;//Rectangle
       
        public Triangle()//default configs
        {
            a = 60;
            lifes = 3;
            trinityRec = new Rectangle(coord.X, coord.Y, a, a);
        }
        public void draw(Graphics g)//drawing function
        {
            trinityRec = new Rectangle(coord.X, coord.Y, a, a);//sets rectangle 
            g.FillPolygon(Brushes.Purple, new Point[] { new Point(coord.X, coord.Y + trinityRec.Height / 2),
                new Point(coord.X + trinityRec.Width, coord.Y),
                new Point(coord.X + trinityRec.Width, coord.Y + trinityRec.Height) }); //drawing user triangle
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


