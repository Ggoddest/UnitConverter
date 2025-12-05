using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UnitConverter
{
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox(string message, string title = "Сообщение", 
                               MessageBoxButton button = MessageBoxButton.OK, 
                               MessageBoxImage icon = MessageBoxImage.None)
        {
            InitializeComponent();
            this.Title = title;
            SetupContent(message, icon);
            SetupWindowIcon(icon);
            
            // Настройка кнопок
            if (button == MessageBoxButton.YesNo)
            {
                OkButton.Content = "Да";
            }
            else
            {
                OkButton.Content = "OK";
            }
        }

        private void SetupContent(string message, MessageBoxImage icon)
        {
            var brushConverter = new BrushConverter();
            
            switch (icon)
            {
                case MessageBoxImage.Error:
                    IconTextBlock.Text = "❌";
                    IconTextBlock.Foreground = (Brush)brushConverter.ConvertFrom("#E53E3E");
                    break;
                case MessageBoxImage.Warning:
                    IconTextBlock.Text = "⚠";
                    IconTextBlock.Foreground = (Brush)brushConverter.ConvertFrom("#DD6B20");
                    break;
                case MessageBoxImage.Information:
                    IconTextBlock.Text = "ℹ";
                    IconTextBlock.Foreground = (Brush)brushConverter.ConvertFrom("#3182CE");
                    break;
                case MessageBoxImage.Question:
                    IconTextBlock.Text = "?";
                    IconTextBlock.Foreground = (Brush)brushConverter.ConvertFrom("#3182CE");
                    break;
                default:
                    IconTextBlock.Text = "ℹ";
                    IconTextBlock.Foreground = (Brush)brushConverter.ConvertFrom("#3182CE");
                    break;
            }
            MessageTextBlock.Text = message;
        }

        private void SetupWindowIcon(MessageBoxImage icon)
        {
            // Используем стандартные иконки Windows через URI
            string iconUri = icon switch
            {
                MessageBoxImage.Error => "pack://application:,,,/PresentationFramework;component/Resources/Error.ico",
                MessageBoxImage.Warning => "pack://application:,,,/PresentationFramework;component/Resources/Warning.ico",
                MessageBoxImage.Question => "pack://application:,,,/PresentationFramework;component/Resources/Question.ico",
                _ => "pack://application:,,,/PresentationFramework;component/Resources/Information.ico"
            };

            try
            {
                this.Icon = new BitmapImage(new Uri(iconUri));
            }
            catch
            {
                // Если системные иконки недоступны, используем нашу иконку приложения
                this.Icon = new BitmapImage(new Uri("pack://application:,,,/app_icon2.ico"));
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        public static MessageBoxResult Show(string message, string title = "Сообщение", 
                                            MessageBoxButton button = MessageBoxButton.OK, 
                                            MessageBoxImage icon = MessageBoxImage.None)
        {
            var window = new CustomMessageBox(message, title, button, icon);
            window.ShowDialog();
            return MessageBoxResult.OK;
        }
    }
}