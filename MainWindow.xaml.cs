using System.Collections.ObjectModel;
using System.IO;  // Inkludera System.IO för filhantering
using System.Windows;
using System.Windows.Controls;

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> Names { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Names = new ObservableCollection<string>();
            DataContext = this;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string nameToAdd = txtName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(nameToAdd) && !Names.Contains(nameToAdd))
            {
                Names.Add(nameToAdd);
                txtName.Clear();
            }
            else
            {
                MessageBox.Show("Name cannot be empty or already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lstNames.SelectedItem != null)
            {
                string? nameToRemove = lstNames.SelectedItem as string; // Säker nullkontroll
                if (nameToRemove != null)
                {
                    Names.Remove(nameToRemove);
                }
                else
                {
                    MessageBox.Show("Select a name to remove", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Select a name to remove", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                File.WriteAllLines("names.txt", Names);
                MessageBox.Show("Names saved successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save names. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists("names.txt"))
                {
                    var namesFromFile = File.ReadAllLines("names.txt");
                    Names.Clear();
                    foreach (var name in namesFromFile)
                    {
                        Names.Add(name);
                    }
                    MessageBox.Show("Names loaded successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No saved names found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load names. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
