using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class RecommendationsPage : Window
    {
        public RecommendationsPage()
        {
            InitializeComponent();
        }

        // Метод для очищення тексту в полі TextBox при фокусі
        private void RemoveText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "search")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        // Метод для відновлення тексту в полі TextBox при втраті фокусу
        private void AddText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "search";
                textBox.Foreground = Brushes.Gray;
            }
        }
    }
}
