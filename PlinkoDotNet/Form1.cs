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
        // Generic Folder Pathing to avoid Reusing to much code
        const string ASSETS_FOLDER = @"C:\cerberus\Assets\cards";
        const string PROJECT_FOLDER = @"C:\cerberus\PlinkoDotNet";
        private static string CARD_BACK = String.Format("{0}\\{1}", ASSETS_FOLDER, "Back.png");
        private static string CARDS_LIST = String.Format("{0}\\{1}", PROJECT_FOLDER, "Cartes.txt");

        // We Load all the Cards of the file
        // I saved already put the name of the files of my assets folder instead to save time
        private readonly String[] cards = File.ReadAllLines(CARDS_LIST);


        private PictureBox[] cards_images = null;

        private List<String> ShuffleCards(String[] input)
        {
            Random r = new Random();
            // Random Order by using empty Next, return 0 or 1 so the OrderBy will by completely Random everytime
            return input.OrderBy(x => r.Next()).ToList();
        }


        private void RefreshCard(PictureBox card, String file, Boolean visible = true)
        {
            card.Visible = visible;
            card.Image = Image.FromFile(file);
            card.Refresh();
        }

        private void RefreshAllCard(Boolean visible=true)
        {
            foreach (var card in cards_images)
            {
                // Force the Back of the Card to be visible for all Cards
                RefreshCard(card, CARD_BACK, visible);
            }
        }

        public void Poker(string username1, int betvalue)
        {
            // Shuffle the Cards
            List<String> shuffled_cards = ShuffleCards(cards);
            Random random = new Random();

            RefreshAllCard();
            Thread.Sleep(1000);

            // Pick one by one
            foreach (var card in cards_images)
            {
                // let Random pick a number between 0 and our Count of shuffled Cards
                int pick_index = random.Next(shuffled_cards.Count());
                String picked_card = shuffled_cards[pick_index];

                // Remove the picked Card to avoid Duplicate Pick
                shuffled_cards.RemoveAt(pick_index); 
                String file = String.Format("{0}\\{1}.png", ASSETS_FOLDER, picked_card);
                RefreshCard(card, file, true);
                Thread.Sleep(1000);
            }

            Thread.Sleep(2000);
            RefreshAllCard(false);
        }

        public Form1()
        {
            InitializeComponent();
            cards_images = new PictureBox[] { poker1, poker2, poker3, poker4, poker5 };
        }

        private void append_Text(String text)
        {
            MessageBox.Text += text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String text = InputForm.Text;

            if (int.TryParse(text, out int value))
            {
                append_Text(String.Format("Starting a Plinko game with {0}$\n", value));
            }
            else
            {
                append_Text(String.Format("The Input is not a number {0}\n", text));
            }
        }

        private void btnPoker_Click(object sender, EventArgs e)
        {
            Poker("Test", 10);
        }
    }
}
