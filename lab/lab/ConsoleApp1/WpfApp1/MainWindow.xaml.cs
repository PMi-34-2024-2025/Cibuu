using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainPage());
            
            TopBar.CIBUUClicked += TopBar_CIBUUClicked;
        }

        private void TopBar_CIBUUClicked(object sender, EventArgs e)
        {
            MainFrame.Navigate(new MainPage());
        }


        public void RemoveText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "search")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        public void AddText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "search";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void OpenSearchPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SearchPage());
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RegistrationPage());
        }

        private void OpenRecommendations_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RecommendationsPage());
        }
    }
}
