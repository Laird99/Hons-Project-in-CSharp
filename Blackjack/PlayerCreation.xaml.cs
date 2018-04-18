using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for PlayerCreation.xaml
    /// </summary>
    public partial class PlayerCreation : Window
    {
        List<Player> PlayerNames = new List<Player>();

        public PlayerCreation()
        {
            this.WindowState = WindowState.Maximized;
            InitializeComponent();
        }

        private void SaveProfiles() {
            // http://www.wpfsharp.com/2011/01/04/c-mono-reading-and-writing-to-a-text-file/
            string FileName = "Players.txt";

            
            StreamReader file = File.OpenText(FileName);
            string content = file.ReadToEnd();
            
            file.Close();
            StreamWriter writer = new StreamWriter(FileName);

            for (int y = 0; y < PlayerNames.Count; y++)
            {
                writer.Write(content);
                writer.WriteLine(PlayerNames[y].Name);
                writer.WriteLine(PlayerNames[y].Chips);             
            }
            writer.Close();
        }    

        public async void btnplayersubmit_Click(object sender, RoutedEventArgs e)
        {
            string name = txtname.Text;
            double chips = sliderchip.Value;

            PlayerNames.Add(new Player { Name = name, Chips = chips});
            SaveProfiles();
            lblinfo.Visibility = Visibility.Visible;
            await Task.Delay(1000);
            lblinfo.Visibility = Visibility.Hidden;
            var menu = new Menu();
            this.Close();
            menu.ShowDialog();
        }

        private void btnplayercancel_Click(object sender, RoutedEventArgs e)
        {
            var menu = new Menu();
            this.Close();
            menu.ShowDialog();
        }

        private void frameinfo_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            // accidentally clicked on this, this needs to be here
        }
    }
}
