using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainForm
{
    public partial class Form1 : Form
    {
        private static string GeneratePassword(int length,bool value)
        {
            string pass = "";
            bool symbolStatus = value;
            string[] allList = { "0","1", "2", "3", "4", "5", "6", "7", "8", "9", "A","B", "C", "D","E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Z", "b", "c", "d", "f", "g", "h", "j", "k", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z", "U", "Y", "a", "e", "i", "o", "u", "y" , "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=", ";", ":", ",", ".", "/", "?","|", "`", "~", "[", "]", "{", "}"};
            string[] charNumList = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Z", "b", "c", "d", "f", "g", "h", "j", "k", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z", "U", "Y", "a", "e", "i", "o", "u", "y" };
            Random symbol = new Random();
            if (symbolStatus)
            {
                for (int count = 0; count < length; count++)
                {
                    pass = pass + allList[symbol.Next(allList.Length)];
                }
            }
            else
                for (int count = 0; count < length; count++)
                {
                    pass = pass + charNumList[symbol.Next(charNumList.Length)];
                }
            return pass;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void pwdTxtBox_TextChanged(object sender, EventArgs e)
        {
            pwdMeter.SetPassword(pwdTxtBox.Password);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = pwdTxtBox.Password;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void pwdMeter_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pwdMeter.SetPassword("");
            //pwdTxtBox.Password.Remove(Convert.ToInt32(comboBox1.Text));
            textBox1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            bool statusSymbol;
            int length = Convert.ToInt32(comboBox1.Text);
            
            if (comboBox3.Text == "Yes")
                statusSymbol = true;
            else
                statusSymbol = false;
            string pass = GeneratePassword(length,statusSymbol);
            int score = PasswordUtils.ComputePasswordScore(pass);
            if (comboBox2.Text == "Strong")
                while (score < 59 || score > 79)
                {
                    pass = GeneratePassword(length, statusSymbol);
                    score = PasswordUtils.ComputePasswordScore(pass);
                }
            else
                while (score < 80)
                {
                    pass = GeneratePassword(length, statusSymbol);
                    score = PasswordUtils.ComputePasswordScore(pass);
                }        
            pwdMeter.SetPassword(pass);
            textBox2.Text = pass;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pwdMeter.SetPassword("");
            textBox2.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.SelectionStart = 0;
            textBox2.SelectionLength = textBox2.Text.Length;
            textBox2.Focus();
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
