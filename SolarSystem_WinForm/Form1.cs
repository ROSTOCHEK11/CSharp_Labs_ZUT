using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using Timer = System.Windows.Forms.Timer;

namespace lab7_planets
{
    public partial class Form1 : Form
    {

        private const int numberOfPlanets = 8;
        private RoundButton[] planets = new RoundButton[numberOfPlanets];
        private Timer timer;
        private RoundButton sunButton;

        public Form1()
        {
            InitializeComponent();
            InitializeSun();
            InitializePlanets();
            InitializeTimer();
            this.Paint += Form1_Paint;


            label1.Text = DateTime.Now.ToString("dd.MM.yyyy");

            Timer timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;
            timer1.Start();

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            label2.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void InitializeSun()
        {
            sunButton = new RoundButton();
            sunButton.Size = new Size(100, 100); 
            sunButton.BackColor = Color.Yellow; 
            sunButton.Location = new Point(750 - 100 / 2, 400 - 100 /2); // Set the position of the sun (static position)
            this.Controls.Add(sunButton); 
        }


        private void InitializePlanets()
        {
            int[] planetSizes = { 20, 25, 30, 28, 50, 45, 40, 35 };
            Color[] planetColors = { Color.Brown, Color.Orange, Color.Aqua, Color.Red, Color.Beige, Color.BurlyWood, Color.LightBlue, Color.Blue };

            for (int i = 0; i < numberOfPlanets; i++)
            {
                planets[i] = new RoundButton();
                planets[i].Size = new Size(planetSizes[i], planetSizes[i]); 
                planets[i].BackColor = planetColors[i]; 
                
                planets[i].Location = new Point(200 + i * 50, 200);
                this.Controls.Add(planets[i]);
            }
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 50; //updating interval
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdatePlanetPositions();
        }

        private void UpdatePlanetPositions()
        {
            double time = DateTime.Now.TimeOfDay.TotalSeconds;

            
            for (int i = 0; i < numberOfPlanets; i++)
            {
                int radius = 80 + i * 30; // orbits
                double angularVelocity = 2 * Math.PI / (10 * (i + 1)); // speed of rotation

                double angle = angularVelocity * time;

                int x = 750 + (int)(radius * Math.Cos(angle)); 
                int y = 400 + (int)(radius * Math.Sin(angle)); 

                planets[i].Location = new Point(x - planets[i].Width / 2, y - planets[i].Height / 2);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Gray); // Choose the color for orbits
            pen.DashStyle = DashStyle.Dot; // Set the style for orbits

            // Draw orbits for each planet
            for (int i = 0; i < numberOfPlanets; i++)
            {
                int radius = 80 + i * 30;
                int x = 750 - radius;
                int y = 400 - radius;
                int diameter = 2 * radius;

                g.DrawEllipse(pen, x, y, diameter, diameter);
            }
        }


    }

}
