using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snake_Game
{
    public partial class Form1 : Form
    {
        Random randFood = new Random();
        Graphics paper;
        Snake snake = new Snake();
        Food food;

        bool left = false;
        bool right = false;
        bool down = false;
        bool up = false;

        int score = 0;


        public Form1()
        {
            InitializeComponent();
            food = new Food(randFood);
            timer1.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            paper = e.Graphics;
            if (timer1.Enabled == true)
            {
                food.drawFood(paper);
                snake.drawSnake(paper);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space && timer1.Enabled == false)
            {
                timer1.Enabled = true;
                titleBarLabel.Text = "";
                spaceBarLabel.Text = "Created by Poseroa";
                up = false;
                right = false;
                left = false;
                down = true;
            }

            if (e.KeyData == Keys.Down && up == false)
            {
                down = true;
                up = false;
                right = false;
                left = false;
            }
            if (e.KeyData == Keys.Up && down == false)
            {
                down = false;
                up = true;
                right = false;
                left = false;
            }
            if (e.KeyData == Keys.Right && left == false)
            {
                down = false;
                up = false;
                right = true;
                left = false;
            }
            if (e.KeyData == Keys.Left && right == false)
            {
                down = false;
                up = false;
                right = false;
                left = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            snakeScoreLabel.Text = Convert.ToString(score);

            if (down) { snake.moveDown(); }
            if (up) { snake.moveUp(); }
            if (right) { snake.moveRight(); }
            if (left) { snake.moveLeft(); }

            for (int i = 0; i < snake.SnakeRec.Length; i++)
            {
                if (snake.SnakeRec[i].IntersectsWith(food.foodRec))
                {
                    score += 10;
                    snake.growSnake();
                    food.foodLocation(randFood);
                }
            }

            collision();

            this.Invalidate();
        }

        public void collision()
        {
            for (int i = 1; i < snake.SnakeRec.Length; i++)
            {
                if (snake.SnakeRec[0].IntersectsWith(snake.SnakeRec[i]))
                {
                    restart();
                }
            }

            if (snake.SnakeRec[0].X < 0 || snake.SnakeRec[0].X > 290)
            {
                restart();
            }

            if (snake.SnakeRec[0].Y < 0 || snake.SnakeRec[0].Y > 290)
            {
                restart();
            }
        }

        public void restart()
        {
            timer1.Enabled = false;
            spaceBarLabel.Text = "Your Snake is Dead";
            MessageBox.Show("Your snake is dead. Your score: "+score);
            snakeScoreLabel.Text = "0";
            score = 0;
            titleBarLabel.Text = "SNAKE GAME";
            spaceBarLabel.Text = "Press Space to Begin";
            snake = new Snake();
        }

        private void titleBarLabel_Click(object sender, EventArgs e)
        {

        }
    }
}