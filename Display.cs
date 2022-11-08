using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 期末專案
{
    public partial class Display : Form
    {
        public Display()
        {
            InitializeComponent();
        }
        int level = 1;

        
        int buy_in;
        int number_of_people;
        int left_number_of_people;
        int origin_bet;
        int first_price, second_price, third_price, forth_price, fifth_price;

        int break_time;
        int blind_raise_time;
        int blind_raise_count_down;
        int break_time_count_down;
        int next_break_timeLeft;

        public Display(string race_text, string buy_in_text, string number_of_people_text, string left_number_of_people_text, string origin_bet_text,
            string break_time_text, string blind_raise_time_text)
        {
            InitializeComponent();
            raceLabel.Text = race_text;
            buy_in = int.Parse(buy_in_text);
            number_of_people = int.Parse(number_of_people_text);
            left_number_of_people = int.Parse(left_number_of_people_text);
            origin_bet = int.Parse(origin_bet_text);
            break_time = int.Parse(break_time_text)*60;
            blind_raise_time = int.Parse(blind_raise_time_text)*60;

            blind_raise_count_down = blind_raise_time;
            break_time_count_down = break_time;
            next_break_timeLeft = blind_raise_time * 5;

            change();
        }

        

        public void update(string race_text, string buy_in_text, string number_of_people_text, string left_number_of_people_text, string origin_bet_text
            , string first_price_text, string second_price_text, string third_price_text, string forth_price_text, string fifth_price_text)
        {
            raceLabel.Text = race_text;

            buy_in = int.Parse(buy_in_text);
            number_of_people = int.Parse(number_of_people_text);
            left_number_of_people = int.Parse(left_number_of_people_text);
            origin_bet = int.Parse(origin_bet_text);


            if (first_price_text == "")
                first_price = 0;
            else
                first_price = int.Parse(first_price_text);

            if (second_price_text == "")
                second_price = 0;
            else
                second_price = int.Parse(second_price_text);

            if (third_price_text == "")
                third_price = 0;
            else
                third_price = int.Parse(third_price_text);

            if (forth_price_text == "")
                forth_price = 0;
            else
                forth_price = int.Parse(forth_price_text);

            if (fifth_price_text == "")
                fifth_price = 0;
            else
                fifth_price = int.Parse(fifth_price_text);

            
            
            
            change();
        }

        

        public void change()
        {
            averageLabel.Text = (origin_bet * number_of_people / left_number_of_people).ToString();

            playersLabel.Text = left_number_of_people.ToString() + "/" + number_of_people.ToString();

            total_priceLabel.Text = "$" + (buy_in * number_of_people * 0.8).ToString();

            if (first_price == 0 && second_price == 0 && third_price == 0 && forth_price == 0 && fifth_price ==0)
            {
                
            }
            else if (forth_price != 0 && fifth_price != 0)
            {
                price_listLabel.Text = "1st\t$" + first_price + "\n\n" +
                                                        "2nd\t$" + second_price + "\n\n" +
                                                        "3rd\t$" + third_price + "\n\n" +
                                                        "4th\t$" + forth_price + "\n\n" +
                                                        "5th\t$" + fifth_price;
            }
            else
            {
                price_listLabel.Text = "1st\t$" + first_price + "\n\n" +
                                                        "2nd\t$" + second_price + "\n\n" +
                                                        "3rd\t$" + third_price;
            }



        }

        

        private void Display_Load(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.ToString("D");
            timeLabel.Text = DateTime.Now.ToString("tt HH:mm:ss");

            break_time_countdown_timer.Stop();

            player.Ctlcontrols.play();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.ToString("D");
            timeLabel.Text = DateTime.Now.ToString("tt HH:mm:ss");

            if (blind_raise_count_down > 0)
            {
                // Display the new time left
                // by updating the Time Left label.
                blind_raise_count_down = blind_raise_count_down - 1;

                string a, b;

                if (blind_raise_count_down / 60 < 10)
                    a = "0" + blind_raise_count_down / 60;
                else
                    a = "" + blind_raise_count_down / 60;

                if (blind_raise_count_down % 60 < 10)
                    b = "0" + blind_raise_count_down % 60;
                else
                    b = "" + blind_raise_count_down % 60;


                left_timeLabel.Text = a + ":" + b;


                
            }
            else
            {
                level += 1;

                levelLabel.Text = "LEVEL " + level;
                blindsLabel.Text = level * 100 + "/" + level * 200;
                next_levelLabel.Text = level * 100 + 100 + "/" + (level * 200 + 200);

                blind_raise_count_down = blind_raise_time;

                if (level == 6 || level == 11 || level == 16 || level == 21)
                {
                    break_timelLabel.Visible = true;
                    break_panel.Visible = true;
                    player.Ctlcontrols.play();

                    blind_raise_countdown_timer.Stop();
                    break_time_countdown_timer.Start();;
                }


            }
        }


        private void timer3_Tick(object sender, EventArgs e)
        {
            if (break_time_count_down > 0)
            {
                break_time_count_down -= 1;

                string a, b;

                if (break_time_count_down / 60 < 10)
                    a = "0" + break_time_count_down / 60;
                else
                    a = "" + break_time_count_down / 60;

                if (break_time_count_down % 60 < 10)
                    b = "0" + break_time_count_down % 60;
                else
                    b = "" + break_time_count_down % 60;


                left_timeLabel.Text = a + ":" + b;
            }
            else
            {
                break_time_count_down = break_time;

                break_timelLabel.Visible = false;
                break_panel.Visible = false;
                player.Ctlcontrols.play();

                break_time_countdown_timer.Stop();
                blind_raise_countdown_timer.Start();
            }
        }



        private void timer2_Tick(object sender, EventArgs e)
        {
            if (next_break_timeLeft > 0)
            {
                
                // Display the new time left
                // by updating the Time Left label.
                next_break_timeLeft = next_break_timeLeft - 1;

                string a, b, c;

                if (next_break_timeLeft /3600 < 0)
                    a = "00";
                else
                    a = "0" + next_break_timeLeft / 3600;

                if ((next_break_timeLeft % 3600) / 60 < 10)
                    b = "0" + ((next_break_timeLeft % 3600) / 60).ToString();
                else
                    b = "" + ((next_break_timeLeft % 3600) / 60).ToString();

                if (next_break_timeLeft % 60 < 10)
                    c = "0" + next_break_timeLeft % 60;
                else
                    c = "" + next_break_timeLeft % 60;


                next_break_timeLabel.Text = a + ":" + b + ":" + c;
            }
            else
            {
                next_break_timeLeft = break_time + blind_raise_time * 5;
            }
        }

        
    }
}