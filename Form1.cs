using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Racer
{
    public partial class Form1 : Form
    {
        int roadSpeed, trafficSpeed;
        int playerSpeed = 12;
        int score, carImage;

        Random rand = new Random();
        Random carPosition = new Random();

        bool moveLeft, moveRight;

        public Form1()
        {
            InitializeComponent();
            resetGame();
        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                moveLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                moveRight = true;
            }
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                moveLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                moveRight = false;
            }
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            score++;

            if (moveLeft && player.Left > 10)
            {
                player.Left -= playerSpeed;
            }
            if (moveRight && player.Left < 750)
            {
                player.Left += playerSpeed;
            }

            roadTrack1.Top += roadSpeed;
            roadTrack2.Top += roadSpeed;

            if (roadTrack1.Top > 604)
            {
                roadTrack1.Top = -604;
            }
            if (roadTrack2.Top > 604)
            {
                roadTrack2.Top = -604;
            }

            AI1.Top += trafficSpeed;
            AI2.Top += trafficSpeed;

            if (AI1.Top > 605)
            {
                changeAIcars(AI1);
            }
            if (AI2.Top > 605)
            {
                changeAIcars(AI2);
            }

            if (player.Bounds.IntersectsWith(AI1.Bounds) || player.Bounds.IntersectsWith(AI2.Bounds))
            {
                gameOver();
            }

            if (score > 40 && score < 500)
            {
                prize.Image = Properties.Resources.bronze;
            }
            if (score >= 500 && score < 2000)
            {
                prize.Image = Properties.Resources.silver;
                roadSpeed = 20;
                trafficSpeed = 22;
            }
            if (score >= 2000)
            {
                prize.Image = Properties.Resources.gold;
                roadSpeed = 25;
                trafficSpeed = 27;
            }
        }

        private void changeAIcars(PictureBox tempCar)
        {
            carImage = rand.Next(1, 9);

            switch (carImage)
            {
                case 1:
                    tempCar.Image = Properties.Resources.carPink;
                    break;
                case 2:
                    tempCar.Image = Properties.Resources.ambulance;
                    break;
                case 3:
                    tempCar.Image = Properties.Resources.carGrey;
                    break;
                case 4:
                    tempCar.Image = Properties.Resources.carGreen;
                    break;
                case 5:
                    tempCar.Image = Properties.Resources.carYellow;
                    break;
                case 6:
                    tempCar.Image = Properties.Resources.TruckWhite;
                    break;
                case 7:
                    tempCar.Image = Properties.Resources.TruckBlue;
                    break;
                case 8:
                    tempCar.Image = Properties.Resources.carOrange;
                    break;
                case 9:
                    tempCar.Image = Properties.Resources.CarRed;
                    break;
            }

            tempCar.Top = carPosition.Next(100, 400) * -1;

            if ((string)tempCar.Tag == "carLeft")
            {
                tempCar.Left = carPosition.Next(8, 367);
            }
            if ((string)tempCar.Tag == "carRight")
            {
                tempCar.Left = carPosition.Next(424, 765);
            }
        }

        private void restartGame(object sender, EventArgs e)
        {
            resetGame();
        }

        private void gameOver()
        {
            playSound();
            gameTimer.Stop();
            explosion.Visible = true;
            player.Controls.Add(explosion);
            explosion.Location = new Point(-8, 5);
            explosion.BackColor = Color.Transparent;

            prize.Visible = true;
            prize.BringToFront();

            startButton.Enabled = true;
        }

        private void resetGame()
        {
            startButton.Enabled = false;
            explosion.Visible = false;
            prize.Visible = false;
            moveLeft = false;
            moveRight = false;
            score = 0;
            prize.Image = Properties.Resources.bronze;

            roadSpeed = 12;
            trafficSpeed = 15;

            AI1.Top = carPosition.Next(200, 500) * -1;
            AI1.Left = carPosition.Next(8, 367);

            AI2.Top = carPosition.Next(200, 500) * -1;
            AI2.Left = carPosition.Next(424, 765);

            gameTimer.Start();
        }

        private void playSound()
        {
            System.Media.SoundPlayer crashSound = new System.Media.SoundPlayer(Properties.Resources.hit);
            crashSound.Play();
        }
    }
}
