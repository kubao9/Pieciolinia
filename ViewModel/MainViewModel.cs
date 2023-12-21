using Pieciolinia.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;
using NAudio.Wave;
using NAudio.Midi;
using NAudio.Wave.SampleProviders;
using System;
using System.Media;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Windows;

namespace Pieciolinia.ViewModel
{

    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Note> Notes { get; private set; }
        public List<string> Pitches { get; private set; }

        //public ICommand PlayMusicCommand { get; }

        public MainViewModel()
        {
            Notes = new ObservableCollection<Note>();
            Pitches = new List<string> { "C", "D", "E", "F", "G", "A", "B" };

            //PlayMusicCommand = new RelayCommand(PlayMusic);
        }

        public void AddNote(string pitch, int duration, int octave, bool isSharp)
        {
            var note = new Note(pitch, duration, octave, isSharp)
            {
                XPosition = CalculateXPosition(),
                YPosition = CalculateYPosition(pitch, octave)
            };

            Notes.Add(note);

            /*Debug.WriteLine($"Dodano nutę: {pitch}, liczba nut w kolekcji: {Notes.Count}");
            Debug.WriteLine($"{note.XPosition} : {note.YPosition}");*/
            foreach (var _note in Notes)
            {
                Debug.WriteLine($"Pitch: {_note.Pitch}, Duration: {_note.Duration}, Octave: {_note.Octave}, IsSharp: {_note.IsSharp}, X: {_note.XPosition}, Y: {_note.YPosition}");
            }

        }

        private int CalculateXPosition()
        {
            int baseXPosition = 10; // Początkowa pozycja X dla pierwszej nuty
            int noteSpacing = 30; // Odległość między nutami

            // rozmieszczenie nut 
            int positionX = baseXPosition + Notes.Count * noteSpacing;
            return positionX;
        }

        private int CalculateYPosition(string pitch, int octave)
        {
            int baseLine = 50; // Bazowa pozycja Y dla linii C środkowej oktawy (C4)
            int lineSpacing = 10; // Odległość między liniami pięciolinii

            // Mapowanie wysokości dźwięku na pozycję Y 
            int pitchPosition;
            switch (pitch)
            {
                case "C":
                    pitchPosition = 0;
                    break;
                case "D":
                    pitchPosition = 1;
                    break;
                case "E":
                    pitchPosition = 2;
                    break;
                case "F":
                    pitchPosition = 3;
                    break;
                case "G":
                    pitchPosition = 4;
                    break;
                case "A":
                    pitchPosition = 5;
                    break;
                case "B":
                    pitchPosition = 6;
                    break;
                default:
                    pitchPosition = 0;
                    break;
            }


            // Obliczanie pozycji Y w oparciu o wysokość dźwięku i oktawe
            int positionY = baseLine - (pitchPosition + (octave - 4) * 7) * lineSpacing;
            positionY -= 150;
            return positionY;
        }

        //Mechanizm odgrywania muzyki
        public async void PlayMusic()
        {
            foreach (var note in Notes)
            {
                string fileName = GetFileNameForNote(note);
                string filePath = Path.Combine("NotesAudio", fileName);

                if (File.Exists(filePath))
                {
                    using (var player = new SoundPlayer(filePath))
                    {
                        /*//player.PlaySync(); // Odtwarza dźwięk synchronicznie
                        player.Play(); // Odtwarzanie dźwięku asynchronicznie

                        // Odczekaj 1 sekundę (1000 milisekund)
                        await Task.Delay(1000);

                        player.Stop();*/

                        player.Play();

                        int playDuration = ConvertDurationToMilliseconds(note.Duration);
                        await Task.Delay(playDuration);
                        player.Stop();
                    }

                    //Duration jest w milisekundach
                    //await Task.Delay(note.Duration);
                    //await Task.Delay(200);
                }
                else
                {
                    // Obsługa błędu, plik nie istnieje
                    Console.WriteLine($"File not found: {filePath}");
                }
            }
        }

        private int ConvertDurationToMilliseconds(int duration)
        {
            // Załóżmy, że cała nuta to 2000 ms
            int wholeNoteDurationMs = 2000;
            return wholeNoteDurationMs / duration;
        }

        private string GetFileNameForNote(Note note)
        {
            // Przykład nazwy pliku: "a1.wav"
            string noteName = note.Pitch.ToLower(); // "a"
            if (note.IsSharp)
            {
                noteName += "sharp"; // "a#"
            }
            string fileName = $"{noteName}{note.Octave}.wav"; // "a14.wav"
            return fileName;
        }

        public void SaveNotesToFile(string filePath)
        {
            var lines = Notes.Select(n => $"{n.Pitch},{n.Duration},{n.Octave},{n.IsSharp}");
            File.WriteAllLines(filePath, lines);
        }
        public void LoadNotesFromFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            Notes.Clear();
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                /*var note = new Note
                {
                    Pitch = parts[0],
                    Duration = int.Parse(parts[1]),
                    Octave = int.Parse(parts[2]),
                    IsSharp = bool.Parse(parts[3]),
                    XPosition = int.Parse(parts[4]),
                    YPosition = int.Parse(parts[5]),
                };*/

                var note = new Note(parts[0], int.Parse(parts[1]), int.Parse(parts[2]), bool.Parse(parts[3]))
                {
                    XPosition = CalculateXPosition(),
                    YPosition = CalculateYPosition(parts[0], int.Parse(parts[2]))
                };

                Notes.Add(note);
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
