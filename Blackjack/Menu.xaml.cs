using System.Windows;

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            this.WindowState = WindowState.Maximized;
            InitializeComponent();
        }

        private void btncreate_Click(object sender, RoutedEventArgs e)
        {
            var creation = new PlayerCreation();
            this.Close();
            creation.ShowDialog();
        }

        private void btnload_Click(object sender, RoutedEventArgs e)
        {
            var choice = new LoadProfile();
            this.Close();
            choice.ShowDialog();
        }

        private void btnexit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            var deletion = new DeleteProfile();
            this.Close();
            deletion.ShowDialog();
        }

        private void btninfo_Click(object sender, RoutedEventArgs e)
        {
            var howto = new Rules();
            this.Close();
            howto.ShowDialog();
        }
    }
}
