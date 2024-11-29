using System;
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

            if (MainFrame != null)
            {
                MainFrame.Navigate(new MainPage());
            }
            else
            {
                MessageBox.Show("MainFrame is not initialized.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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

        // Відображення зеленої кнопки після успішної аутентифікації
        public void OnUserAuthenticated()
        {
            TopBar.ShowUserPageButton();
        }

        // Виправлення: доступ до TopBar без рекурсії
        public TopBar TopBarControl
        {
            get { return TopBar; }
        }

        public void ShowUserPageButton()
        {
            // Переконуємося, що кнопка на панелі TopBar стає видимою
            TopBar.UserPageButton.Visibility = Visibility.Visible;
        }


    }
}
