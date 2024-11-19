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
            // Відкриває нове вікно
            SearchPage searchPage = new SearchPage();
            searchPage.Owner = this; // Установлює батьківське вікно (необов'язково)
            searchPage.Show();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            SearchPage searchPage = new SearchPage();
            searchPage.Show();
        }
        private void OpenRecommendations_Click(object sender, RoutedEventArgs e)
        {
            RecommendationsPage recommendationsPage = new RecommendationsPage();
            recommendationsPage.Show();
        }

    }
}
