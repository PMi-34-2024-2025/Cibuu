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
    
    // ����� RemoveText ��� �������� ������, ���� ���� ������ �����
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
            // ������� ���� �� ����
            this.Close();
        }


        // ����� AddText ��� ���������� ������-�����������, ���� ���� ������ �����
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
