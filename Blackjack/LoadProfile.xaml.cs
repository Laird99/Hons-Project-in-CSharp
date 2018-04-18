using System;
using System.IO;
using System.Windows;

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for LoadProfile.xaml
    /// </summary>
    public partial class LoadProfile : Window
    {
        public LoadProfile()
        {
            this.WindowState = WindowState.Maximized;
            InitializeComponent();
            RefreshList();
        }
        string[] names;

        private void RefreshList() {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "Players.txt";
            // https://msdn.microsoft.com/en-us/library/system.appdomain.basedirectory(v=vs.110).aspx

            try
            {
                names = File.ReadAllLines(filepath);
                for (int l = 0; l < names.Length; l++)
                {
                    if (l % 2 == 0)
                        lstPlayers.Items.Add(names[l]);
                    else { }
                }
            }
            catch (Exception ex)
            {
                string error = "Could not read the file";
                lstPlayers.Items.Add(error);
            }
        }

        private void btnstart_Click(object sender, RoutedEventArgs e)
        {
            string p1;
            try {p1 = lstPlayers.SelectedValue.ToString(); }
            catch (NullReferenceException ex) {
                p1 = null;
            }    

            if (p1 == null) {
                lblerror.Visibility = Visibility.Visible;
            }
            else
            {
                string NumAsStr;
                int chipLoc, chipcount = 0;
                for (int x = 0; x < names.Length; x++)
                {
                    if (names[x].Equals(p1))
                    {
                        chipLoc = x + 1;
                        NumAsStr = names[chipLoc];
                        chipcount = int.Parse(NumAsStr);
                    }
                    else { }
                }

                var maingame = new MainWindow(p1, chipcount);
                this.Close();
                maingame.ShowDialog();
            }
        }

        private void btnmenu_Click(object sender, RoutedEventArgs e)
        {
            var menu = new Menu();
            this.Close();
            menu.ShowDialog();
        }
    }
}
