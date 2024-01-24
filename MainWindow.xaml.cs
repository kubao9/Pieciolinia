﻿using Microsoft.Win32;
using Pieciolinia.ViewModel;
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

        public bool ReturnsTrue()
        {
            return true;
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

            string noteIcon = GetNoteIconFromDuration(duration);

            // Dostęp do MainViewModel i wywołanie funkcji AddNote
            var mainViewModel = DataContext as MainViewModel;

            // Dodajemy notę przy użyciu MainViewModel
            mainViewModel?.AddNote(pitch, duration, octave, isSharp, noteIcon);
        }

        private string GetNoteIconFromDuration(int duration)
        {
            int wholeNoteDurationMs = 2000;

            switch (wholeNoteDurationMs / duration)
            {
                case 2000:
                    return "𝅝";
                case 1000:
                    return "𝅗𝅥";
                case 500:
                    return "𝅘𝅥";
                case 250:
                    return "𝅘𝅥𝅮";
                case 125:
                    return "𝅘𝅥𝅯";
                default:
                    return "𝅘𝅥𝅮";
            }
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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var mainViewModel = DataContext as MainViewModel;
            mainViewModel?.EditButton_Click();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainViewModel = DataContext as MainViewModel;
            mainViewModel?.SaveButton_Click();
        }
        private void NotesComboBoxEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mainViewModel = DataContext as MainViewModel;
            mainViewModel?.NotesComboBoxEdit_SelectionChanged();

        }

        private void ButtonSaveEdited_Click(object sender, RoutedEventArgs e)
        {
            // Pobieranie danych z interfejsu użytkownika
            string pitch = PitchComboBoxEdit.SelectedItem.ToString();
            //int duration = int.Parse(DurationTextBox.Text);
            ComboBoxItem selectedDurationItem = (ComboBoxItem)DurationComboBoxEdit.SelectedItem;
            int duration = int.Parse(selectedDurationItem.Tag.ToString());
            //int octave = int.Parse(OctaveTextBox.Text);
            ComboBoxItem selectedOctaveItem = (ComboBoxItem)OctaveComboBoxEdit.SelectedItem;
            int octave = int.Parse(selectedOctaveItem.Tag.ToString());
            bool isSharp = IsSharpCheckBoxEdit.IsChecked ?? false;
            string noteIcon = GetNoteIconFromDuration(duration);
            // Dostęp do MainViewModel i wywołanie funkcji AddNote
            var mainViewModel = DataContext as MainViewModel;
            // Dodajemy notę przy użyciu MainViewModel
            mainViewModel?.SaveEditedNote(pitch, duration, octave, isSharp, noteIcon);
        }

        private void RecordButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedFormat = (FileFormatComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string filter;
            switch (selectedFormat)
            {
                case "MP3":
                    filter = "MP3 files (*.mp3)|*.mp3";
                    break;
                case "MIDI":
                    filter = "MIDI files (*.midi)|*.midi";
                    break;
                default:
                    filter = "WAV files (*.wav)|*.wav";
                    break;
            }

            var saveFileDialog = new SaveFileDialog
            {
                Filter = filter,
                Title = "Save record file"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                var mainViewModel = DataContext as MainViewModel;
                mainViewModel?.PrepareRecording(saveFileDialog.FileName, selectedFormat);
            }
        }

        private void ClearNotesButton_Click(object sender, RoutedEventArgs e)
        {
            var mainViewModel = DataContext as MainViewModel;
            mainViewModel?.ClearNotes();
        }

    }
}
