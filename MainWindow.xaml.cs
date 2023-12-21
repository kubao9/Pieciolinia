using Microsoft.Win32;
using Pieciolinia.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Pieciolinia
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            // Pobieranie danych z interfejsu użytkownika
            string pitch = PitchComboBox.SelectedItem.ToString();
            //int duration = int.Parse(DurationTextBox.Text);
            ComboBoxItem selectedDurationItem = (ComboBoxItem)DurationComboBox.SelectedItem;
            int duration = int.Parse(selectedDurationItem.Tag.ToString());

            //int octave = int.Parse(OctaveTextBox.Text);
            ComboBoxItem selectedOctaveItem = (ComboBoxItem)OctaveComboBox.SelectedItem;
            int octave = int.Parse(selectedOctaveItem.Tag.ToString());
            bool isSharp = IsSharpCheckBox.IsChecked ?? false;

            // Dostęp do MainViewModel i wywołanie funkcji AddNote
            var mainViewModel = DataContext as MainViewModel;

            // Dodajemy notę przy użyciu MainViewModel
            mainViewModel?.AddNote(pitch, duration, octave, isSharp);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            // Dostęp do MainViewModel 
            var mainViewModel = DataContext as MainViewModel;

            mainViewModel?.PlayMusic();
        }

        private void Open1_Click(object sender, RoutedEventArgs e)
        {
            // Okno dialogowe do wyboru pliku
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                (DataContext as MainViewModel)?.LoadNotesFromFile(openFileDialog.FileName);
            }
        }

        private void Save1_Click(object sender, RoutedEventArgs e)
        {
            // Tutaj możesz dodać okno dialogowe do wyboru ścieżki pliku
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                (DataContext as MainViewModel)?.SaveNotesToFile(saveFileDialog.FileName);
            }
        }
    }
}
