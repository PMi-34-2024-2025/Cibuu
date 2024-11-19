using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class SearchPage : Window
    {
        public SearchPage()
        {
            InitializeComponent();
        }
    
    // Метод RemoveText для очищення тексту, коли поле отримує фокус
    private void RemoveText(object sender, RoutedEventArgs e)
    {
        TextBox textBox = sender as TextBox;
        if (textBox != null && textBox.Text == "search")
        {
            textBox.Text = "";
            textBox.Foreground = Brushes.Black;
        }
    }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            // Закрити лише це вікно
            this.Close();
        }


        // Метод AddText для відновлення тексту-заповнювача, коли поле втрачає фокус
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
