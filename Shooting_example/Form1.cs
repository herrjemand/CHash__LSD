using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LSD
{
    public partial class Form1 : Form
    {
        Graphics g; //declare a graphics

        List<Missile> missiles = new List<Missile>();//Missile list
        List<Cube> squares = new List<Cube>();//Cubes List
        List<Bonus> bonus = new List<Bonus>();//Bonus list
        Triangle trinity = new Triangle(); //Declare user unit
        Menu_Ell[] menus_ell = new Menu_Ell[3], levels_ell = new Menu_Ell[5]; //Declares menu items
        ///<variables>
        /// mnpr: Maximum number per row y = n(h+p) + p
        /// lcn: level cube numbers //depends on leve and other factors
        /// pad: Theoretical minimum padding
        /// pad_real: Actual padding
        /// circle_level_r: Circle Level radius
        /// main_radius:1/4 of height
        /// tfow: three fourth of width
        /// menu_height: height of top menu
        /// speed: speed of cubes
        /// c_level_score: Current level score
        /// c_size: 5% of screen size
        /// 
        /// grad: 50% gradient of screen; Used in bonus: y=mx+c
        /// 
        /// name: Name variable
        /// 
        /// testing: cube for calculations
        /// </variables>
        private short mnpr, lcn, pad, pad_real, circle_level_r = 100, main_radius = 0, tfow, menu_height = 40, speeed = 20, c_level_score,c_size;
        private double grad;
        public string name;
        private Cube testing = new Cube();

        public Form1(){InitializeComponent();}

        #region "GUI"
        string[] menus = new string[] { "Scores", "Play", "Help" }, //Menus
            instr = new string[] {
            "Use mouse to move triangle.",
            "Left click shot.", 
            "Right click to navigate back to main menu.",
            "Press Space to pause the game",
            "Shoot circles to navigate.",
            "Shoot cubes to get points.",
            "Catch bonus to get x5 triple shots.",
            "Cubes should not reach you.",
            "Have fun."};//help menu text
            
        private void Form1_Load(object sender, EventArgs e)
        {
            trinity.name = name;//sets username
            #region "ch"
            //Cheat part
            if (new string[] { "ChuckNorris", "chucknorris", "Chuck Norris" }.Contains(trinity.name))
            {
                trinity.bonus = 32000;
                trinity.name = "☠☠☠";
            }
            #endregion
            #region "Fullscreen"
            //Full screen setup
            this.FormBorderStyle = FormBorderStyle.None;//Form Has no borders
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;//Full screen
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;//Position of windows(fullscreen fix)
            #endregion
            #region "Calculations"
            c_size = Convert.ToInt16((((this.Height - menu_height) * 0.1)/2));//5% of screen size
            grad = (short)((((this.Height - menu_height) / 2) / (decimal)this.Width) * 100000); //gradient of 50%height
            tfow = (short)((this.Width / 5) * 3);//Three Fifth of Width
            main_radius = (Int16)((this.Height - menu_height) / 2);
            #endregion
            this.KeyDown += new KeyEventHandler(keypress);//handler for keydown
            this.FormClosed += new FormClosedEventHandler(formclosed);//handler for form close. Bug fix, with closing form but not app
            #region "close_button"
            //setting closing button in the right top corner
            Label close_btn = new Label { Name = "close_button", Text = "X", Visible = true, ForeColor = Color.White, Font = new Font(Font.FontFamily, 10), Location = new Point(this.Width - 15, 0) };
            this.Controls.Add(close_btn);
            close_btn.Click += new EventHandler(this.close_button);
            #endregion
            #region "GUI Init"
            //Initializing GUI, and sets variables
            trinity.delta_coord = new Point(this.Width - trinity.trioRec.Width - 10, trinity.delta_coord.Y);//position player.
            
            for (short i = 0; i < 3; i++) { 
                menus_ell[i] =  new Menu_Ell();
                menus_ell[i].delta_txt = menus[Convert.ToInt32(i)];
                menus_ell[i].delta_loc = new Point(get_coords((short)(2 * i + 1)).X + this.Width / 5, get_coords((short)(2 * i + 1)).Y);
            }
            for (short i = 0; i < 5; i++){
                levels_ell[i] = new Menu_Ell();
                levels_ell[i].delta_font_size = 40;
                levels_ell[i].delta_txt = Convert.ToString(i + 1);
                levels_ell[i].delta_loc = get_coords((short)(i + 1));
                levels_ell[i].gen_colour(i);
            }
            wt_draw = "menu"; 
            #endregion
        }
        private void formclosed(object sender, FormClosedEventArgs e) { Application.Exit(); } //If form closes, application closes too
        private void close_button(object sender, System.EventArgs e) { if (MessageBox.Show("Are you sure?", "Exit LSD?", MessageBoxButtons.YesNo) == DialogResult.Yes) { csv.write(trinity.name,trinity.score); Application.Exit(); } }//exit button
       
        #endregion
        #region "Game Engine"
        #region "Mouse & Keyboard events"
        private void Form1_MouseMove(object sender, MouseEventArgs e) { trinity.delta_coord = new Point(trinity.delta_coord.X, e.Y); }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            switch(e.Button)
            {
                case MouseButtons.Left:
                if (wt_draw == "game" && trinity.bonus > 0)
                {
                        for (short i = 0; i < 2; i++)
                        {
                            missiles.Add(new Missile(trinity.trioRec) { delta_ymxc = (short)Math.Pow(-1, i), delta_ymxc_c = (short)trinity.delta_coord.Y }); //adding 2 new Bonus missiles
                        }
                        trinity.bonus--; //-1 bonus shot
                }
                missiles.Add(new Missile(trinity.trioRec));
                break;
                case MouseButtons.Right:
                if (menus.Contains(char.ToUpper(wt_draw[0]) + wt_draw.Substring(1)))
                {
                    wt_draw = "menu";
                    missiles.Clear();
                }
                break;
            }
            
        }
        private void keypress(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {

                if (wt_draw == "game")
                {
                    wt_draw = "paused";
                }
                else if (wt_draw == "paused")
                {
                    missiles.Clear();
                    wt_draw = "game";
                }
                e.Handled = true;
            }
        }
        #endregion
        #region "Variables"
        private string wt_draw;
        private short rnd_colour_wait = 0,f_init_s, f_init_i = 3, f_init_n; //f_init_* set of variables for pauses and so on. Shared by multiple functions
        private bool fiasko = false;
        SortedDictionary<short, string> local_scores = new SortedDictionary<short, string>();
        private Point get_coords(short i)
        {
            return (
                new Point(
                 (short)(((main_radius + 2 * circle_level_r) / 2 * Math.Cos((i * 1 * Math.PI + 2 * Math.PI) / 5)) + ((this.Width - menu_height - circle_level_r) / 2)),
                 (short)(((main_radius + 2 * circle_level_r) / 2 * Math.Sin((i * 1 * Math.PI + 2 * Math.PI) / 5)) + ((this.Height - menu_height - circle_level_r) / 2))
                 )
                );
        }
        private void tmrShoot_Tick(object sender, EventArgs e) { this.Invalidate(); }  

        #endregion

        private void game_init(short level)
        {
            squares.Clear();//Clear Cube arrat
            pad = 10;//Theoretical minimus padding
            mnpr = (short)((this.Height - menu_height - pad) / (testing.rec.Height + pad));//maximum squares per row y = n(h+p) + p
            pad_real = (short)((this.Height - menu_height - mnpr * testing.rec.Height) / mnpr);//real padding
            lcn = (short)((this.Width * 0.01 * (this.Height - menu_height) * 0.01) * (0.10 + (trinity.lieben)/10) * level * 2);
            //Setting up hardness, depends on screen size and level

            short row = 0, nrow = 0;
            bool up = true;
            trinity.lieben += 2;//add 2 lifes every round. Max is 10
            for (short i = 0; i < lcn; i++)
            {
                squares.Add(new Cube());

            }
            foreach (Cube cu in squares)
            {
                if (Program.rand_num((1 + level), 30)  == 10)
                {
                    cu.delta_bonus = true;
                }
                if (up)
                {
                    cu.delta_loc = new Point(65 * row, cu.delta_loc.Y - (nrow * (cu.rec.Height + pad_real)));
                    cu.delta_up = up;
                    nrow++;
                    if (nrow >= mnpr)
                    {
                        nrow = 0;
                        up = false;
                        row++;
                    }
                }
                else
                {
                    cu.delta_loc = new Point(65 * row, cu.delta_loc.Y + (nrow * (cu.rec.Height + pad_real)));
                    cu.delta_up = up;
                    nrow++;
                    if (nrow >= mnpr)
                    {
                        nrow = 0;
                        up = true;
                        row++;
                    }
                }
            }
            f_init_i = 3; f_init_s = 0; f_init_n = 0; c_level_score = 0;
            wt_draw = "init";
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            bool a_visibles = false;//Is there any vbisible cube
            g = e.Graphics;//Graphics
            switch (wt_draw)//Switch for what to draw
            {
                #region "Drawing_GUI" 
                #region "Menu"
                case "menu":
                    for (short i = 0; i < 3; i++)
                    {
                        rnd_colour_wait++;
                        if (rnd_colour_wait >= 20) { menus_ell[Convert.ToInt32(i)].rnd_colour(); rnd_colour_wait = 0; }
                        menus_ell[Convert.ToInt32(i)].menu_draw(g);
                        foreach (Missile m in missiles)
                        {
                            if (m.rec.IntersectsWith(menus_ell[i].rec))
                            {
                                wt_draw = menus[Convert.ToInt32(i)].ToLower();
                                f_init_s = 0;
                                f_init_n = 0;
                                f_init_i = 0; 
                                missiles.Clear();
                                break;
                            }
                            m.draw(g);
                        }
                    }
                    break;
                #endregion
                #region "Menu Levels"
                case "play":
                    for (short i = 0; i < 5; i++)
                    {
                        levels_ell[i].menu_draw(g);
                        foreach (Missile m in missiles)
                        {
                            if (m.rec.IntersectsWith(levels_ell[i].rec))
                            {
                                game_init((short)(i + 1));
                                missiles.Remove(m);
                                break;
                            }
                            m.draw(g);
                        }
                    }
                    g.DrawString("Level", new Font("Arial", 45), new SolidBrush(Color.White), this.Width / 2, (this.Height - menu_height)/2, new StringFormat(){LineAlignment = StringAlignment.Center,Alignment = StringAlignment.Center}); //Drawing user score.
                    break;
                #endregion
                #region "Init"
                case "init":
                    f_init_s += 1;
                    g.DrawString(Convert.ToString(f_init_i) , new Font("Arial", f_init_s), new SolidBrush(Color.White), 
                        get_coords((short)(2 * 1 + 1)).X + this.Width / 5, get_coords((short)(2 * 1 + 1)).Y, new StringFormat()); //Drawing user score.
                    f_init_s += 3;
                    if (f_init_s >= 100)
                    {
                        f_init_s = 0;
                        f_init_i--;
                    }
                    if (f_init_i <= 0) {
                        f_init_s = 0;
                        f_init_i = 3;
                        wt_draw = "go";
                        missiles.Clear();
                    }

                    break;
                case "go":
                    f_init_s += 3;
                    if (f_init_s <= 100)
                    {
                        g.DrawString("GO!", new Font("Arial", 55), new SolidBrush(Color.White),
                          get_coords((short)(2 * 1 + 1)).X + this.Width / 5, get_coords((short)(2 * 1 + 1)).Y, new StringFormat()); //Drawing user score.
                    }
                    else
                    {
                        f_init_s = 0;
                        f_init_i = 3;
                        wt_draw = "game";
                    }
                    break;
                #endregion
                #region "Pause"
                case "paused":
                    f_init_i++;
                    f_init_s = (short)(125 * Math.Sin(f_init_i * (2 * Math.PI) / 70) + 125);
                    g.DrawString("Paused", new Font("Arial", 55), new SolidBrush(Color.FromArgb(f_init_s, 255,255,255)),
                        this.Width / 2, (this.Height - menu_height)/2, new StringFormat(){LineAlignment = StringAlignment.Center,Alignment = StringAlignment.Center}); //Drawing user score.
                    break;
                #endregion
                #region "Victory"
                case "vic":
                    if (f_init_n < 160)
                    {
                        f_init_n += 3;
                        if (f_init_n < 100) {
                            f_init_s += 3;
                        }
                        g.DrawString("Victory!\n", new Font("Arial", f_init_s / 2), new SolidBrush(Color.White),
                        this.Width / 2, (this.Height - menu_height) / 2, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }); //Drawing user score.

                    }
                    else {
                        if (f_init_s < 300)
                        {
                            f_init_s++;
                        }
                        else { f_init_s = 0; f_init_n = 0; f_init_i = 0; wt_draw = "menu"; }
                        if (f_init_i != c_level_score)
                        {
                            f_init_i++;
                        }
                        g.DrawString("Victory!\n" + "You earned " + f_init_i + " points this round.", new Font("Arial", 55), new SolidBrush(Color.White),
                           this.Width / 2, (this.Height - menu_height) / 2, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }); //Drawing user score.
                    }
                   
                    break;
                #endregion
                #region "Defeat"
                case "defeat":
                    if (f_init_n < 400){f_init_n += 5;}
                    if (f_init_s < 250) { f_init_s += 5; }
                     g.DrawString("Defeat!\n", new Font("Arial", 86), new SolidBrush(Color.FromArgb(f_init_s, 255, 0, 0)),
                     this.Width / 2, (this.Height - menu_height) / 2, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center }); //Drawing user score.
                    if(f_init_n > 395){

                        if (f_init_n < 1200){
                            f_init_n += 5;
                        }else {

                            if (trinity.score != 0)
                            {
                               csv.write(trinity.name,trinity.score);
                            }
                            f_init_s = f_init_n = f_init_i = trinity.score = 0;  
                            wt_draw = "scores"; 
                            trinity.lieben = 3;
                            fiasko = false;
                            speeed = 20;
                        }

                        if (f_init_i < 250){f_init_i += 5;}
                        short disp_y = Convert.ToInt16(g.MeasureString("Defeat!\n", new Font("Arial", 86)).Height);
                        g.DrawString("Your score " + trinity.score,
                            new Font("Arial", 75), //Font
                            new SolidBrush(Color.FromArgb(f_init_i, 255, 0, 0)),//Colour
                           this.Width / 2, ((this.Height - menu_height) / 2) + disp_y/2, //Location
                           new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center //Position
                           }); //Drawing user score.
                    }

                    break;
                #endregion
                #region "Scores"
                case "scores":
                    if (f_init_n != 239)
                    {
                        local_scores = csv.read();
                        f_init_n = 239;
                    }
                    string draw_scores = "";
                    short index = 0;
                    if (local_scores.Count != 0)
                    {
                        foreach (KeyValuePair<short, string> item in local_scores.Reverse())
                        {
                            
                            f_init_i++;
                            draw_scores = index + 1 + " : " + item.ToString().Replace("[", "").Replace("]", "").Replace(",", " ") + "\n";
                            g.DrawString(draw_scores, new Font("Arial", 18), new SolidBrush(Color.FromArgb(255, Program.rand_num(0, 255), Program.rand_num(0, 255), Program.rand_num(0, 255))),
                                20 + 170*index ,
                                (short)((((this.Height - c_size) - (menu_height + c_size)) / 2) *  (double)Math.Sin(((2 * Math.PI) / 170) * (20 + 150 * index + (f_init_i/13))) + (((this.Height - c_size) + (menu_height + c_size)) / 2)),
                                new StringFormat()); //Drawing user score.
                            index++;
                        }  
                    }
                    else
                    {
                        g.DrawString("No scores were added yet.", new Font("Arial", 60), new SolidBrush(Color.White),
                            this.Width / 2, (this.Height - menu_height) / 2, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
                    }
                    break;
                #endregion
                #region "Help"
                case "help":
                    string draw_text = "Instructions: \n\n";
                    for (short i = 0; i < instr.Length;i++ ){draw_text += + i + 1 + ": " + instr[i] + "\n";}
                    g.DrawString(draw_text, new Font("Arial", 40), new SolidBrush(Color.White),
                        20, 20 +(this.Height - menu_height  - Convert.ToInt16(g.MeasureString(draw_text, new Font("Arial", 40)).Height))/2, new StringFormat()); //Drawing user score.
          
                    break;
                #endregion
                #endregion
                #region "Drawing_Game"
                case "game":
                
                        foreach (Cube cu in squares)
                        { 
                            if (cu.delta_visible)
                            {
                                a_visibles = true;
                                #region "Missiles"
                                if (!fiasko)
                                {
                                    foreach (Missile m in missiles)
                                    {
                                        if (m.rec.IntersectsWith(cu.rec))
                                        {
                                            if (cu.delta_bonus)
                                            {
                                                bonus.Add(new Bonus(cu.rec));
                                            }
                                            trinity.score++;
                                            c_level_score++;
                                            cu.delta_visible = false;
                                            missiles.Remove(m);

                                            break;
                                        }
                                    }
                                }
                                #endregion
                                #region "Cubes X Location"
                                if (cu.delta_loc.X >= tfow) { rnd_colour_wait++; if (rnd_colour_wait >= 10) { cu.rand_colour(); rnd_colour_wait = 0; } }

                                if (cu.delta_loc.X >= this.Width - trinity.trioRec.Width - 15)
                                {
                                    cu.delta_visible = false;
                                    trinity.lieben--;
                                    if (trinity.lieben <= 0) {
                                        fiasko = true;
                                        speeed = 80;
                                        trinity.lieben = 0;
                                    }
                                }
                                #endregion
                                if (cu.delta_up)
                                {
                                    cu.delta_loc = new Point(cu.delta_loc.X, cu.delta_loc.Y - speeed);

                                    if (cu.delta_loc.Y <= menu_height)
                                    {

                                        cu.delta_loc = new Point(cu.delta_loc.X + 65, cu.delta_loc.Y);
                                        cu.delta_up = false;
                                    }

                                }
                                else
                                {
                                    cu.delta_loc = new Point(cu.delta_loc.X, cu.delta_loc.Y + speeed);

                                    if (cu.delta_loc.Y + cu.rec.Height >= this.Height)
                                    {
                                        cu.delta_loc = new Point(cu.delta_loc.X + 65, cu.delta_loc.Y);
                                        cu.delta_up = true;
                                    }

                                }

                                cu.cube_draw(g);

                            }
                    }
                    #region "Round finish"
                    if (!a_visibles) {
                        squares.Clear();
                        bonus.Clear();
                        f_init_i = 0; f_init_s = 0; f_init_n = 0;
                        if (trinity.lieben == 0)
                        {
                            trinity.score -= c_level_score;
                            wt_draw = "defeat";
                        }
                        else {
                            wt_draw = "vic";
                        }
                    }
                    #endregion
                    break;

                #endregion
            }
            trinity.draw(g);//Drawind user
            #region "Upper Menu"
            g.DrawLine(new Pen(new SolidBrush(Color.White),3F), new Point(0, menu_height - 7), new Point(this.Width, menu_height - 7));//Drawind menu line
            g.DrawString(trinity.name + " : " + Convert.ToString(trinity.score).PadLeft(5, '0') + "   ☥x " + trinity.lieben + "   ♆x " + trinity.bonus, 
                new Font("Arial", 25), new SolidBrush(Color.White), 0, 0, new StringFormat()); //Drawing user score.
            #endregion

            #region "Missile & Bonus Drawing"
            #region "Bonus"
            foreach (Bonus b in bonus) {
                b.gen_colour();
                    b.draw(g);
                if (b.rec.IntersectsWith(trinity.trioRec)) {
                    trinity.bonus += 5;
                    trinity.score += 2;
                    c_level_score += 2;
                    bonus.Remove(b);
                    break;
                }
                if (b.rec.X <= 0)
                {
                    bonus.Remove(b);
                    break;
                }
            }
            #endregion
            #region "Missiles"
            foreach (Missile m in missiles) //Drawing missiles
            {
                #region "Bonus Missile"
                if (m.delta_ymxc != 0)
                {
                    for (short i = 0; i < 25; i++)
                    {
                        m.missile_point = new Point(m.missile_point.X,
                            (short)(m.delta_ymxc * (decimal)((decimal)grad / 100000) * (m.missile_point.X - this.Width) + (m.delta_ymxc_c + (trinity.trioRec.Height + m.delta_ymxc * trinity.trioRec.Height / 2) / 2)));
                        m.draw(g);

                    }
                }
                #endregion
                #region "Normal Missile"
                else {
                    for (short i = 0; i < 25; i++)
                    {
                        m.draw(g);                       
                    }
                }
                #endregion
                if (m.rec.X <= 0)
                {
                    missiles.Remove(m);
                    break;
                }
            }
            #endregion
            #endregion
        }

        #endregion

        #region "OLD CODE"
        /* public Point get_coords(double i)
        {
            //Console.WriteLine((short)(a * Math.Cos(2 * (double)i * Math.PI) + a / 2) + missileRec.X + " : " + (short)(a * Math.Sin(2 * (double)i * Math.PI) + a / 2) + missileRec.Y);
            return new Point(,(short)(a * Math.Sin(2 * (double)i * Math.PI) + a / 2) + missileRec.Y );
        }
        ai = ai + 0.1;
           g.DrawPolygon(new Pen(Brushes.White, 10F), new Point[]{ p1, p2, p3 });
           Console.WriteLine(get_coords(ai + 3)  + ":" + get_coords(ai + 2)  + ":" + get_coords(ai + 1));
           g.FillRectangle(new SolidBrush(colour), missileRec.X, missileRec.Y, a, a);*/
        /*
        private void level_button(object sender, System.EventArgs e) { game_init((short)(((Label)sender).Text)); } //EventHandler for dynamicly created labels, and parsing text as a level.

         private void items_resize() { //Fixes issue with label position , due to resize of form
           // main_radius = (short)(this.Height / 2);
        for (int i = 0; i < 3; i++) {
        menu_items[i].Location = new Point((this.Width / 2), (this.Width / 2));
         }
         }

        if (Levels[i-1] == null) {
                        //Console.WriteLine("Machishen");
                        Label bttn = new Label
                        {
                            AutoSize = true,
                            Name = "lvl_button_" + i,
                            Text = Convert.ToString(i),
                            Tag = i,
                            Visible = true,
                            TextAlign = ContentAlignment.MiddleCenter,
                            ForeColor = Color.White,
                            Font = new Font(Font.FontFamily, 40)
                        };
                        Levels[i - 1] = bttn;
                        this.Controls.Add(bttn);
                        bttn.Click += new EventHandler(level_button);
                    }
    
            g.DrawBezier(new Pen(Color.Yellow), new Point(m.missile_point.X, m.missile_point.Y),
                        new Point(m.missile_point.X + 50/3, m.missile_point.Y + m.MissileRec.Height*2),
                        new Point(m.missile_point.X + (50 * 2) / 3, m.missile_point.Y), 
                        new Point(m.missile_point.X + 50, m.missile_point.Y + m.MissileRec.Height*2));
         * 
         * Rectangle drawing inside circle
        
         g.DrawEllipse(new Pen(Color.FromArgb(255, 42 * i, 255 - 42 * i, 0), 4F), coord_x, coord_y, circle_level_r, circle_level_r);
         g.DrawRectangle(new Pen(Color.FromArgb(255, 42 * i, 255 - 42 * i, 0), 4F), 
                        coord_x + (short)(circle_level_r - (circle_level_r / 2) * Math.Sqrt(2))/2,
                        coord_y + (short)(circle_level_r - (circle_level_r / 2) * Math.Sqrt(2)) / 2,
                        (short)((circle_level_r / 2) * Math.Sqrt(2)),
                        (short)((circle_level_r / 2) * Math.Sqrt(2)));
         */

        #endregion

    }
}
