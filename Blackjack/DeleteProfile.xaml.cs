using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for DeleteProfile.xaml
    /// </summary>
    public partial class DeleteProfile : Window
    {
        List<Player> Players = new List<Player>();

        public DeleteProfile()
        {
            this.WindowState = WindowState.Maximized;
            InitializeComponent();
            GetList();
        }

        string[] names;

        public void Delete(string name) {
            int line1 =0, line2 =0;
            var content = "";

            for (int x = 0; x < names.Length; x++)
            {
                if (names[x].Equals(name))
                {
                    line1 = x;
                    line2 = x + 1;
                }
                else { }
            }

            StreamWriter writer = new StreamWriter("Players.txt");
            writer.Write(content);
            for (int x = 0; x < names.Length; x++)
            {
                if (x == line1 || x == line2){
                    // left blank to skip writing the profile wanted to be deleted
                }
                else {
                    writer.WriteLine(names[x]);
                }
            }
            writer.Close();

            var menu = new Menu();
            this.Close();
            menu.ShowDialog();
        }

        private void btnremove_Click(object sender, RoutedEventArgs e)
        {
            string p1;
            try { p1 = lstplayers.SelectedValue.ToString(); }
            catch (NullReferenceException ex)
            {
                p1 = null;
            }

            if (p1 == null)
            {
                lblwarning.Visibility = Visibility.Visible;
            }
            else
            {
                Delete(p1);
                lblwarning.Visibility = Visibility.Hidden;
            }

        }

        public void GetList() {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "Players.txt";
            // https://msdn.microsoft.com/en-us/library/system.appdomain.basedirectory(v=vs.110).aspx

            try
            {

                names = File.ReadAllLines(filepath);
                for (int l = 0; l < names.Length; l++)
                {
                    if (l % 2 == 0)
                        lstplayers.Items.Add(names[l]);

                    else { }
                }
            }
            catch (Exception)
            {
                string error = "Could not read the file";
                lstplayers.Items.Add(error);
            }

        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            var menu = new Menu();
            this.Close();
            menu.ShowDialog();
        }
    }
}
