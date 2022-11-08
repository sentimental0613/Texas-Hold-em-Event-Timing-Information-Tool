using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 期末專案
{
    public partial class Backstage : Form
    {
        public Backstage()
        {
            InitializeComponent();
            isLoaded = false;
        }

        private float X;//當前窗體的寬度
        private float Y;//當前窗體的高度
        bool isLoaded;  // 是否已設定各控制的尺寸資料到Tag屬性
        bool isGenerate = false;

        Display f;

        private void SetTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    SetTag(con);
            }
        }

        private void SetControls(float newx, float newy, Control cons)
        {
            if (isLoaded)
            {
                //遍歷窗體中的控制項，重新設置控制項的值
                foreach (Control con in cons.Controls)
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ':' });//獲取控制項的Tag屬性值，並分割後存儲字元串數組
                    float a = System.Convert.ToSingle(mytag[0]) * newx;//根據窗體縮放比例確定控制項的值，寬度
                    con.Width = (int)a;//寬度
                    a = System.Convert.ToSingle(mytag[1]) * newy;//高度
                    con.Height = (int)(a);
                    a = System.Convert.ToSingle(mytag[2]) * newx;//左邊距離
                    con.Left = (int)(a);
                    a = System.Convert.ToSingle(mytag[3]) * newy;//上邊緣距離
                    con.Top = (int)(a);
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字體大小
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        SetControls(newx, newy, con);
                    }
                }
            }
        }

        private void Backstage_Resize(object sender, EventArgs e)
        {
            //SetControls(X, Y, this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.ToString("D");
            timeLabel.Text = DateTime.Now.ToString("tt HH:mm:ss");

            X = this.Width;//獲取窗體的寬度
            Y = this.Height;//獲取窗體的高度
            isLoaded = true;// 已設定各控制項的尺寸到Tag屬性中
            SetTag(this);//調用方法
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            //Display form = new Display();//產生Display的物件，才可以使用它所提供的Method

            //form.Visible = true;

            if (raceTextBox.Text == "" || break_timeTextBox.Text == "" || next_blind_timeTextBox.Text == "" || number_of_peopleTextBox.Text == "" || left_number_of_peopleTextBox.Text == "" || origin_betTextBox.Text == "")
            {
                MessageBox.Show("there's blank in textbox!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                f = new Display(raceTextBox.Text, buy_InTextBox.Text, number_of_peopleTextBox.Text, left_number_of_peopleTextBox.Text, origin_betTextBox.Text,
                    break_timeTextBox.Text, blind_raise_timeTextBox.Text);//產生Display的物件，才可以使用它所提供的Method

                isGenerate = true;
                f.Show();
            }

            if (isGenerate)
            {
                generateButton.Enabled = false;
                updateButton.Enabled = true;
            }
            else
            {
                updateButton.Enabled = false;
            }
        }


       











        private void plusButton_Click(object sender, EventArgs e)
        {
            if (number_of_peopleTextBox.Text == "")
            {
                MessageBox.Show("不可為空白!");
            }
            else
            {
                int a = int.Parse(number_of_peopleTextBox.Text);
                number_of_peopleTextBox.Text = (a + 1).ToString();
            }

        }

        private void subtractButton_Click(object sender, EventArgs e)
        {
            if (left_number_of_peopleTextBox.Text == "")
            {
                MessageBox.Show("不可為空白!");
            }
            else
            {
                int a = int.Parse(left_number_of_peopleTextBox.Text);
                left_number_of_peopleTextBox.Text = (a - 1).ToString();
            }

        }

        private void total_priceLabel_change(object sender, EventArgs e)
        {

            if (buy_InTextBox.Text == "" || number_of_peopleTextBox.Text == "")
            {
                total_priceLabel.Text = "$" + "0";
            }
            else
            {
                int unit_price = int.Parse(buy_InTextBox.Text);
                int amount = int.Parse(number_of_peopleTextBox.Text);
                double total_price = unit_price * amount * 0.8;

                total_priceLabel.Text = "$" + (total_price).ToString();

                first_price_cal3_label.Text = "1st   : $" + (total_price * 0.6).ToString();
                second_price_cal3_label.Text = "2nd   : $" + (total_price * 0.3).ToString();
                third_price_cal3_label.Text = "3rd   : $" + (total_price * 0.1).ToString();


                first_price_cal5_label.Text = "1st   : $" + (total_price * 0.5).ToString();
                second_price_cal5_label.Text = "2nd   : $" + (total_price * 0.3).ToString();
                third_price_cal5_label.Text = "3rd   : $" + (total_price * 0.1).ToString();
                forth_price_cal5_label.Text = "4th   : $" + (total_price * 0.05).ToString();
                fifth_price_cal5_label.Text = "5th   : $" + (total_price * 0.05).ToString();
            }





        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            f.update(raceTextBox.Text, buy_InTextBox.Text, number_of_peopleTextBox.Text, left_number_of_peopleTextBox.Text, 
                origin_betTextBox.Text, first_priceTextBox.Text, second_priceTextBox.Text, third_priceTextBox.Text, forth_priceTextBox.Text, fifth_priceTextBox.Text);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int unit_price = int.Parse(buy_InTextBox.Text);
            int amount = int.Parse(number_of_peopleTextBox.Text);
            double total_price = unit_price * amount * 0.8;

            first_priceTextBox.Text = (total_price * 0.6).ToString();
            second_priceTextBox.Text = (total_price * 0.3).ToString();
            third_priceTextBox.Text = (total_price * 0.1).ToString();
            forth_priceTextBox.Text = "";
            fifth_priceTextBox.Text = "";
        }

        private void applyButton2_Click(object sender, EventArgs e)
        {
            int unit_price = int.Parse(buy_InTextBox.Text);
            int amount = int.Parse(number_of_peopleTextBox.Text);
            double total_price = unit_price * amount * 0.8;

            first_priceTextBox.Text = (total_price * 0.5).ToString();
            second_priceTextBox.Text = (total_price * 0.3).ToString();
            third_priceTextBox.Text = (total_price * 0.1).ToString();
            forth_priceTextBox.Text = (total_price * 0.05).ToString();
            fifth_priceTextBox.Text = (total_price * 0.05).ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = DateTime.Now.ToString("tt HH:mm:ss");
            dateLabel.Text = DateTime.Now.ToString("D");
        }
    }
}
