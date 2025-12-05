using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input; // Для обработки событий клавиатуры

namespace UnitConverter
{
    public partial class MainWindow : Window
    {
        private List<Unit> currentUnits;

        public MainWindow()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            var categories = ConversionService.GetCategories();
            CategoryComboBox.ItemsSource = categories;
            if (categories.Count > 0)
                CategoryComboBox.SelectedIndex = 0;
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryComboBox.SelectedItem == null) return;

            string category = CategoryComboBox.SelectedItem.ToString();
            currentUnits = ConversionService.GetUnitsByCategory(category);

            FromUnitComboBox.ItemsSource = currentUnits;
            ToUnitComboBox.ItemsSource = currentUnits;

            if (currentUnits.Count > 0)
            {
                FromUnitComboBox.SelectedIndex = 0;
                ToUnitComboBox.SelectedIndex = Math.Min(1, currentUnits.Count - 1);
            }

            ResultsTextBox.Text = "";
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(InputValueTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
            {
                CustomMessageBox.Show("Пожалуйста, введите корректное числовое значение.", "Ошибка ввода", 
                                     MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (FromUnitComboBox.SelectedItem == null || ToUnitComboBox.SelectedItem == null)
            {
                CustomMessageBox.Show("Выберите единицы измерения.", "Ошибка", 
                                     MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var fromUnit = (Unit)FromUnitComboBox.SelectedItem;
            var toUnit = (Unit)ToUnitComboBox.SelectedItem;

            try
            {
                double result = ConversionService.Convert(fromUnit, toUnit, value);
                ResultsTextBox.Text = $"{value} {fromUnit.Name} = {result:F6} {toUnit.Name}";
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Ошибка конвертации: {ex.Message}", "Ошибка", 
                                     MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ConvertAllButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(InputValueTextBox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
            {
                CustomMessageBox.Show("Пожалуйста, введите корректное числовое значение.", "Ошибка ввода", 
                                     MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (FromUnitComboBox.SelectedItem == null)
            {
                CustomMessageBox.Show("Выберите исходную единицу измерения.", "Ошибка", 
                                     MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var fromUnit = (Unit)FromUnitComboBox.SelectedItem;
            var allResults = ConversionService.ConvertAll(fromUnit, value);

            ResultsTextBox.Text = "";
            foreach (var kvp in allResults)
            {
                ResultsTextBox.AppendText($"{value} {fromUnit.Name} = {kvp.Value} {kvp.Key}\r\n");
            }
        }

        private void InputValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ResultsTextBox.Text = "";
        }

        private void InputValueTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Выполняем конвертацию
                ConvertButton_Click(sender, e);
                e.Handled = true; // Предотвращаем стандартное поведение
            }
        }
    }
}