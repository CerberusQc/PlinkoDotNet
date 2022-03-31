using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlinkoDotNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void append_Text(String text)
        {
            MessageBox.Text += text;
            MessageBox.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String text = InputBet.Text;
            String user = inputUser.Text;

            if (double.TryParse(text, out double value))
            {
                append_Text(String.Format("Starting a Plinko game with {0}$\n", value));
                Plinko game = new Plinko(GameZone, user, value);
                double result = game.Play();
            }
            else
            {
                append_Text(String.Format("The Input is not a number {0}\n", text));
            }
        }
    }
}
