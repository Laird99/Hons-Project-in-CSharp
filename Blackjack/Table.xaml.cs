using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string player, int chips)
        {
            this.WindowState = WindowState.Maximized;
            InitializeComponent();
            AssignProfile(player, chips);
            PlaceBet();
        }
        // global varables required due to design
        int cardsRem = 52, pScore = 0, dScore = 0, chipcount, bet;
        Card[] DeckOrder = new Card[52];
        string p1;

        // Load Game ---------------------------------------------------------------------------
        public void AssignProfile(string person, int amount) {

            // http://www.dreamincode.net/forums/topic/70162-search-for-a-string-in-a-text-file/

            p1 = person; // This made to allow other methods to access player name, not most efficient method
            chipcount = amount; // same with player chips

            StreamReader reader = new StreamReader("Players.txt");
            string text = reader.ReadToEnd();
            if (System.Text.RegularExpressions.Regex.IsMatch(text, person)) {

                lblcount.Content = "Chip Count: " + chipcount;
            }
            else {
                MessageBox.Show("Sorry, but " + person + " could not be found");
            }
   
        }

        // Pre-Deal ---------------------------------------------------------------------------
        public void PlaceBet() {

            if (chipcount <= 0)
            {
                lblWarning.Visibility = Visibility.Visible;
            }
            else
            {
                // Large If statement to customise slider values for each bet depending on chip count before hand
                sliderbet.Maximum = chipcount;
                if (chipcount > 2000 && chipcount < 5000)
                {
                    sliderbet.TickFrequency = 150;
                }
                else if (chipcount > 4999 && chipcount < 10000)
                {
                    sliderbet.TickFrequency = 250;
                }
                else if (chipcount > 9999 && chipcount < 25000)
                {
                    sliderbet.TickFrequency = 500;
                }
                else if (chipcount > 24999 && chipcount < 50000)
                {
                    sliderbet.TickFrequency = 1000;
                }
                else if (chipcount > 49999)
                {
                    sliderbet.TickFrequency = 2000;
                }
                else {
                    // keeps it default
                }
                sliderbet.Visibility = Visibility.Visible;
                btnbet.Visibility = Visibility.Visible;
            }

            
        }

        private void SaveChipCount(string oldcount, string newcount)
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "Players.txt";
            string names = File.ReadAllText(filepath);

            names = names.Replace(oldcount, newcount);

            File.WriteAllText(filepath, names);
        }

        private void btnbet_Click(object sender, RoutedEventArgs e)
        {
            string oldchip, newchip;

            bet = (int)sliderbet.Value;

            oldchip = chipcount + "";
            chipcount = chipcount - bet;
            newchip = chipcount + "";

            lblbet.Content = "Player Bet: " + bet;
            lblcount.Content = "Chip Count: " + chipcount;
            sliderbet.Visibility = Visibility.Hidden;
            btnbet.Visibility = Visibility.Hidden;
            btnreplay.Visibility = Visibility.Visible;
            Btn_hit.Visibility = Visibility.Visible;
            Btn_stick.Visibility = Visibility.Visible;
            pcard1.Visibility = Visibility.Visible;
            pcard2.Visibility = Visibility.Visible;
            dcard1.Visibility = Visibility.Visible;
            dcard2.Visibility = Visibility.Visible;
            lbldscore.Visibility = Visibility.Visible;
            lblpscore.Visibility = Visibility.Visible;

            SaveChipCount(oldchip, newchip);
            PlayStartGame();
        }

        // Main Game --------------------------------------------------------------------------
        public void PlayStartGame()
        {
            int value = 0;
            int count = 0, i = 0;
            string face = "", design = "";

            //Initialising the deck and filling the array with objects
            for (int d = 0; d < DeckOrder.Length; d++)
            {
                switch (d) {
                    //----------Aces------------------------------------
                    case 0: { face = "AC"; value = 11; break; }
                    case 1: { face = "AS"; value = 11; break; }
                    case 2: { face = "AH"; value = 11; break; }
                    case 3: { face = "AD"; value = 11; break; }
                    //----------Twos------------------------------------
                    case 4: { face = "2C"; value = 2; break; }
                    case 5: { face = "2S"; value = 2; break; }
                    case 6: { face = "2H"; value = 2; break; }
                    case 7: { face = "2D"; value = 2; break; }
                    //----------Threes------------------------------------
                    case 8: { face = "3C"; value = 3; break; }
                    case 9: { face = "3S"; value = 3; break; }
                    case 10: { face = "3H"; value = 3; break; }
                    case 11: { face = "3D"; value = 3; break; }
                    //----------Fours------------------------------------
                    case 12: { face = "4C"; value = 4; break; }
                    case 13: { face = "4S"; value = 4; break; }
                    case 14: { face = "4H"; value = 4; break; }
                    case 15: { face = "4D"; value = 4; break; }
                    //----------Fives------------------------------------
                    case 16: { face = "5C"; value = 5; break; }
                    case 17: { face = "5S"; value = 5; break; }
                    case 18: { face = "5H"; value = 5; break; }
                    case 19: { face = "5D"; value = 5; break; }
                    //----------Sixes------------------------------------
                    case 20: { face = "6C"; value = 6; break; }
                    case 21: { face = "6S"; value = 6; break; }
                    case 22: { face = "6H"; value = 6; break; }
                    case 23: { face = "6D"; value = 6; break; }
                    //----------Sevens------------------------------------
                    case 24: { face = "7C"; value = 7; break; }
                    case 25: { face = "7S"; value = 7; break; }
                    case 26: { face = "7H"; value = 7; break; }
                    case 27: { face = "7D"; value = 7; break; }
                    //----------Eights------------------------------------
                    case 28: { face = "8C"; value = 8; break; }
                    case 29: { face = "8S"; value = 8; break; }
                    case 30: { face = "8H"; value = 8; break; }
                    case 31: { face = "8D"; value = 8; break; }
                    //----------Nines------------------------------------
                    case 32: { face = "9C"; value = 9; break; }
                    case 33: { face = "9S"; value = 9; break; }
                    case 34: { face = "9H"; value = 9; break; }
                    case 35: { face = "9D"; value = 9; break; }
                    //----------Tens------------------------------------
                    case 36: { face = "10C"; value = 10; break; }
                    case 37: { face = "10S"; value = 10; break; }
                    case 38: { face = "10H"; value = 10; break; }
                    case 39: { face = "10D"; value = 10; break; }
                    //----------Jacks------------------------------------
                    case 40: { face = "JC"; value = 10; break; }
                    case 41: { face = "JS"; value = 10; break; }
                    case 42: { face = "JH"; value = 10; break; }
                    case 43: { face = "JD"; value = 10; break; }
                    //----------Queens------------------------------------
                    case 44: { face = "QC"; value = 10; break; }
                    case 45: { face = "QS"; value = 10; break; }
                    case 46: { face = "QH"; value = 10; break; }
                    case 47: { face = "QD"; value = 10; break; }
                    //----------Kings------------------------------------
                    case 48: { face = "KC"; value = 10; break; }
                    case 49: { face = "KS"; value = 10; break; }
                    case 50: { face = "KH"; value = 10; break; }
                    case 51: { face = "KD"; value = 10; break; }

                }

                DeckOrder[d] = new Card
                {
                    Face = face,
                    Value = value
                };
            }
            //------ End of for loop for initiating deck

            Random random = new Random();
            do
            {
                count = (int)(random.Next(0, cardsRem)); // issues random number for card selection
                design = DeckOrder[count].Face;
                // issues each selection to player
                switch (i)
                {
                    // Updates the card shown to each position on the screen and adds the value to the respective scores
                    case 0: { UpdateImage(pcard1, design); pScore = pScore + DeckOrder[count].Value; break; }
                    case 1: { UpdateImage(dcard1, design); dScore = dScore + DeckOrder[count].Value; break; }
                    case 2: { UpdateImage(pcard2, design); pScore = pScore + DeckOrder[count].Value; break; }
                    case 3: { UpdateImage(dcard2, design); dScore = dScore + DeckOrder[count].Value; break; }
                    default: { break; }

                }
                
                DeckOrder = RemoveCard(count, DeckOrder, cardsRem); // removes card for deck so no duplicates

                cardsRem--; //decreases variable to represent cards left in deck
                i++;
            } while (i < 4);

            //Setting score to 11+1 if two Aces are drawn, removes instant loss bug
            if (pScore == 22)
                pScore = 12;

            if (dScore == 22)
                dScore = 12;

            lblpscore.Content = "Your Score: " + pScore;
            lbldscore.Content = "Dealer Score: " + dScore;
        }

        private Card[] RemoveCard(int x, Card[] deck, int rem)
        {

            for (int cardpos = x; cardpos < rem; cardpos++)
            {
                // removes last card to compensate for array moving up one
                if (cardpos < (rem - 1))
                {
                    int j = cardpos + 1;
                    string y = deck[j].Face;
                    int z = deck[j].Value;

                    deck[cardpos].Face = y;
                    deck[cardpos].Value = z;

                }
                // overwrites array from position card taken from
                else
                {
                    deck[cardpos].Face = null;
                    deck[cardpos].Value = 0;
                }

            }

            return deck;
        }

        private string GetImage(string x) {

            string card = "";
            switch (x)
            {
                // getting file from path
                // https://stackoverflow.com/questions/43780994/wpf-accessing-a-resource-from-anywhere-in-the-assembly-using-relative-path

                //----------------------Aces-------------------------------------
                case "AC": { card = "pack://application:,,,/Resources/AC.png"; break; }
                case "AS": { card = "pack://application:,,,/Resources/AS.png"; break; }
                case "AH": { card = "pack://application:,,,/Resources/AH.png"; break; }
                case "AD": { card = "pack://application:,,,/Resources/AD.png"; break; }
                //----------------------Twos-------------------------------------
                case "2C": { card = "pack://application:,,,/Resources/_2C.png"; break; }
                case "2S": { card = "pack://application:,,,/Resources/_2S.png"; break; }
                case "2H": { card = "pack://application:,,,/Resources/_2H.png"; break; }
                case "2D": { card = "pack://application:,,,/Resources/_2D.png"; break; }
                //----------------------Threes-------------------------------------
                case "3C": { card = "pack://application:,,,/Resources/_3C.png"; break; }
                case "3S": { card = "pack://application:,,,/Resources/_3S.png"; break; }
                case "3H": { card = "pack://application:,,,/Resources/_3H.png"; break; }
                case "3D": { card = "pack://application:,,,/Resources/_3D.png"; break; }
                //----------------------Fours-------------------------------------
                case "4C": { card = "pack://application:,,,/Resources/_4C.png"; break; }
                case "4S": { card = "pack://application:,,,/Resources/_4S.png"; break; }
                case "4H": { card = "pack://application:,,,/Resources/_4H.png"; break; }
                case "4D": { card = "pack://application:,,,/Resources/_4D.png"; break; }
                //----------------------Fives-------------------------------------
                case "5C": { card = "pack://application:,,,/Resources/_5C.png"; break; }
                case "5S": { card = "pack://application:,,,/Resources/_5S.png"; break; }
                case "5H": { card = "pack://application:,,,/Resources/_5H.png"; break; }
                case "5D": { card = "pack://application:,,,/Resources/_5D.png"; break; }
                //----------------------Sixes-------------------------------------
                case "6C": { card = "pack://application:,,,/Resources/_6C.png"; break; }
                case "6S": { card = "pack://application:,,,/Resources/_6S.png"; break; }
                case "6H": { card = "pack://application:,,,/Resources/_6H.png"; break; }
                case "6D": { card = "pack://application:,,,/Resources/_6D.png"; break; }
                //----------------------Sevens-------------------------------------
                case "7C": { card = "pack://application:,,,/Resources/_7C.png"; break; }
                case "7S": { card = "pack://application:,,,/Resources/_7S.png"; break; }
                case "7H": { card = "pack://application:,,,/Resources/_7H.png"; break; }
                case "7D": { card = "pack://application:,,,/Resources/_7D.png"; break; }
                //----------------------Eights-------------------------------------
                case "8C": { card = "pack://application:,,,/Resources/_8C.png"; break; }
                case "8S": { card = "pack://application:,,,/Resources/_8S.png"; break; }
                case "8H": { card = "pack://application:,,,/Resources/_8H.png"; break; }
                case "8D": { card = "pack://application:,,,/Resources/_8D.png"; break; }
                //----------------------Nines-------------------------------------
                case "9C": { card = "pack://application:,,,/Resources/_9C.png"; break; }
                case "9S": { card = "pack://application:,,,/Resources/_9S.png"; break; }
                case "9H": { card = "pack://application:,,,/Resources/_9H.png"; break; }
                case "9D": { card = "pack://application:,,,/Resources/_9D.png"; break; }
                //----------------------Tens-------------------------------------
                case "10C": { card = "pack://application:,,,/Resources/_10C.png"; break; }
                case "10S": { card = "pack://application:,,,/Resources/_10S.png"; break; }
                case "10H": { card = "pack://application:,,,/Resources/_10H.png"; break; }
                case "10D": { card = "pack://application:,,,/Resources/_10D.png"; break; }
                //----------------------Jacks-------------------------------------
                case "JC": { card = "pack://application:,,,/Resources/JC.png"; break; }
                case "JS": { card = "pack://application:,,,/Resources/JS.png"; break; }
                case "JH": { card = "pack://application:,,,/Resources/JH.png"; break; }
                case "JD": { card = "pack://application:,,,/Resources/JD.png"; break; }
                //----------------------Queens-------------------------------------
                case "QC": { card = "pack://application:,,,/Resources/QC.png"; break; }
                case "QS": { card = "pack://application:,,,/Resources/QS.png"; break; }
                case "QH": { card = "pack://application:,,,/Resources/QH.png"; break; }
                case "QD": { card = "pack://application:,,,/Resources/QD.png"; break; }
                //----------------------Kings-------------------------------------
                case "KC": { card = "pack://application:,,,/Resources/KC.png"; break; }
                case "KS": { card = "pack://application:,,,/Resources/KS.png"; break; }
                case "KH": { card = "pack://application:,,,/Resources/KH.png"; break; }
                case "KD": { card = "pack://application:,,,/Resources/KD.png"; break; }

                default: { break; }

            }

            return card;

        }

        private void UpdateImage(Image card, string pos) {
            // link for refreshing image during runtime https://stackoverflow.com/questions/350027/setting-wpf-image-source-in-code
            BitmapImage deal = new BitmapImage();
            string filepath = GetImage(pos);
            deal.BeginInit();
            deal.UriSource = new Uri(filepath);
            deal.EndInit();
            card.Source = deal;

        }

        private void btnquit_Click(object sender, RoutedEventArgs e)
        {
            var menu = new Menu();
            this.Close();
            menu.ShowDialog();
        }

        private void btnreplay_Click(object sender, RoutedEventArgs e)
        {
            var maingame = new MainWindow(p1, chipcount);
            this.Close();
            maingame.ShowDialog();
        }

        private async void Btn_hit_Click(object sender, RoutedEventArgs e)
        {
            if (pScore <= 21)
            {
                Random num = new Random();
                int count = (int)(num.Next(0, cardsRem));
                string design = DeckOrder[count].Face;
                switch (cardsRem)
                {

                    case 48: { UpdateImage(pcard3, design); pScore = pScore + DeckOrder[count].Value; RemoveCard(count, DeckOrder, cardsRem); break; }
                    case 47: { UpdateImage(pcard4, design); pScore = pScore + DeckOrder[count].Value; RemoveCard(count, DeckOrder, cardsRem); break; }
                    case 46: { UpdateImage(pcard5, design); pScore = pScore + DeckOrder[count].Value; RemoveCard(count, DeckOrder, cardsRem); Btn_stick_Click(sender, e); break; }
                    default: { break; }
                }
                lblpscore.Content = "Your Score: " + pScore;
                cardsRem--;
            }

            if (pScore > 21)
            {
                Btn_hit.Visibility = Visibility.Hidden;
                Btn_stick.Visibility = Visibility.Hidden;
                var endgame = new Results();
                endgame.lbloutput.Content = "PLAYER BUST with " + pScore + "! HOUSE WINS!";
                await Task.Delay(1500);
                endgame.ShowDialog();
            }


        }

        private async void Btn_stick_Click(object sender, RoutedEventArgs e)
        {
            string oldchip, newchip;
            // Await task is used to simulate CPU thinking and to give player time to view cards before opening the results window!
            // https://stackoverflow.com/questions/15599884/how-to-put-delay-before-doing-an-operation-in-wpf
            int dealt = 0;

            Btn_hit.Visibility = Visibility.Hidden;
            Btn_stick.Visibility = Visibility.Hidden;

            if (pScore > 21)
            {
                var endgame = new Results();
                endgame.lbloutput.Content = "PLAYER BUST with " + pScore + "! HOUSE WINS!";
                await Task.Delay(1500);
                endgame.ShowDialog();
                
            }
            else {

                while (dScore < 17)
                {
                    Random num = new Random();
                    int count = (int)(num.Next(0, cardsRem));
                    string design = DeckOrder[count].Face;
                    switch (dealt)
                    {

                        case 0: { UpdateImage(dcard3, design); dScore = dScore + DeckOrder[count].Value; RemoveCard(count, DeckOrder, cardsRem); break; }
                        case 1: { UpdateImage(dcard4, design); dScore = dScore + DeckOrder[count].Value; RemoveCard(count, DeckOrder, cardsRem); break; }
                        case 2: { UpdateImage(dcard5, design); dScore = dScore + DeckOrder[count].Value; RemoveCard(count, DeckOrder, cardsRem); break; }
                        default: { break; }
                    }
                    lbldscore.Content = "Dealer Score: " + dScore;
                    cardsRem--;
                    dealt++;
                    await Task.Delay(500);
                }

            }

            //Moves onto calculating result of game when dealer score => 17
            if (pScore == 21 & dScore != 21)
            {
                // navigating to results window
                // https://stackoverflow.com/questions/24285132/navigating-from-one-xaml-to-another-in-wpf
                var endgame = new Results();
                endgame.lbloutput.Content = "PLAYER WINS WITH BLACKJACK!";
                oldchip = chipcount + "";
                chipcount = chipcount + (int)(bet + (bet * 1.5));
                newchip = chipcount + "";
                SaveChipCount(oldchip, newchip);
                await Task.Delay(1500);
                endgame.ShowDialog();
                

            }          
            else if (dScore > 21)
            {
                var endgame = new Results();
                endgame.lbloutput.Content = "HOUSE BUST with " + dScore + "! PLAYER WINS!";
                oldchip = chipcount + "";
                chipcount = chipcount + (bet * 2);
                newchip = chipcount + "";
                SaveChipCount(oldchip, newchip);
                await Task.Delay(1500);
                endgame.ShowDialog();
                

            }
            else if (pScore > dScore)
            {
                var endgame = new Results();
                endgame.lbloutput.Content = "PLAYER WINS with " + pScore + "!";
                oldchip = chipcount + "";
                chipcount = chipcount + (bet * 2);
                newchip = chipcount + "";
                SaveChipCount(oldchip, newchip);
                await Task.Delay(1500);
                endgame.ShowDialog();
                
            }
            else
            {
                var endgame = new Results();
                await Task.Delay(1500);
                endgame.lbloutput.Content = "HOUSE WINS with " + dScore + "!";
                endgame.ShowDialog();

            }
        }


    }
}
