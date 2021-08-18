using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BullsAndCowsWindowsFormsApp
{
    public partial class MainForm : Form
    {
        string puzzledWord = "";
        const int wordLength = 4;
        int stepCount = 0;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            List<int> digits = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Random random = new Random();

            for (int i = 0; i < wordLength; i++)
            {
                int digitIndex = random.Next(0, digits.Count);
                puzzledWord += digits[digitIndex].ToString();
                digits.RemoveAt(digitIndex);
            }
            //puzzledWordLabel.Text = puzzledWord;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userWord = userWordTextBox.Text;

            if (!IsValid(userWord))
            {
                return;
            }

            stepCount++;

            int bullsCount = CalculateBullsCount(userWord);
            int cowsCount = CalculateCowsCount(userWord);

            bullsCountLabel.Text = "Bulls = " + bullsCount;
            cowsCountLabel.Text = "Cows = " + cowsCount;

            historyDataGridView.Rows.Add(stepCount, userWord, bullsCount, cowsCount);

            if (bullsCount == wordLength)
            {
                if (stepCount <= 20)
                {
                    MessageBox.Show("Good job! You won in " + stepCount + " steps.");                    
                    button1.Enabled = false;
                    userWordTextBox.Enabled = false;
                    puzzledWordLabel.Text = puzzledWord;
                }
                else
                {
                    MessageBox.Show("Thank you for my win! You guessed my number in " + stepCount + " steps.");                    
                    button1.Enabled = false;
                    userWordTextBox.Enabled = false;
                    puzzledWordLabel.Text = puzzledWord;
                }
            }
        }

        private bool IsValid(string userWord)
        {
            if (userWord.Length != wordLength)
            {
                MessageBox.Show("Number contains 4 digits!");
                return false;
            }

            for (int i = 0; i < userWord.Length; i++)
            {
                if (!char.IsDigit(userWord[i]))
                {
                    MessageBox.Show("Please use digits only!");
                    return false;
                }
            }

            for (int i = 0; i < userWord.Length; i++)
            {
                for (int j = i + 1; j < userWord.Length; j++)
                {
                    if (userWord[i] == userWord[j])
                    {
                        MessageBox.Show("Digits are not repeated!");
                        return false;
                    }
                }
            }
            return true;
        }

        private int CalculateBullsCount(string userWord)
        {
            int bullsCount = 0;

            for (int i = 0; i < wordLength; i++)
            {
                if (puzzledWord[i] == userWord[i])
                {
                    bullsCount++;
                }
            }
            return bullsCount;
        }
        private int CalculateCowsCount(string userWord)
        {
            int cowsCount = 0;

            for (int i = 0; i < wordLength; i++)
            {
                for (int j = 0; j < wordLength; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    if (puzzledWord[i] == userWord[j])
                    {
                        cowsCount++;
                    }
                }
            }
            return cowsCount;
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Your aim is to guess the number (4 digits) as quickly as possible. \n " +
                "Your hints: \n" +
                "Bull - right digit and it is in its place. \n" +
                "Cow - right digit, but not in this place. \n" +
                "Zero could be the first.\n" +
                "Have a fun!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void puzzledWordLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
