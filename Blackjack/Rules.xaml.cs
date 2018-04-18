using System.Windows;
using System.Windows.Controls;

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for Rules.xaml
    /// </summary>
    public partial class Rules : Window
    {
        public Rules()
        {
            this.WindowState = WindowState.Maximized;
            InitializeComponent();
        }

        private void txtrules_TextChanged(object sender, TextChangedEventArgs e)
        {
            // another accidental click
        }

        private void txtsources_TextChanged(object sender, TextChangedEventArgs e)
        {
            // another accidental click
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            var menu = new Menu();
            this.Close();
            menu.ShowDialog();
        }
    }
}
