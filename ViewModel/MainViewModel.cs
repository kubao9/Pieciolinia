using Pieciolinia.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System;
using System.Media;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Windows;
using NAudio.Wave;

namespace Pieciolinia.ViewModel
{
    public class AudioRecorder
    {
        private WasapiLoopbackCapture capture;
        private WaveFileWriter writer;

        public void StartRecording(string outputPath)
        {
            capture = new WasapiLoopbackCapture();
            writer = new WaveFileWriter(outputPath, capture.WaveFormat);

            capture.DataAvailable += (s, a) =>
            {
                writer.Write(a.Buffer, 0, a.BytesRecorded);
            };

            capture.RecordingStopped += (s, a) =>
            {
                writer.Dispose();
                writer = null;
                capture.Dispose();
            };

            capture.StartRecording();
        }

        public void StopRecording()
        {
            capture.StopRecording();
        }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        public bool isPlaying { get; set; } = false;
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Note> _notes;

        //Recording music 
        private AudioRecorder recorder;
        private string recordFilePath;
        public bool isRecording { get; set; } = false;
        public ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    OnPropertyChanged(nameof(Notes));
                }
            }
        }
        public List<string> Pitches { get; private set; }
        public int SelectedNote { get; set; }
        private int selectedPitch;
        public int SelectedPitch
        {
            get { return selectedPitch; }
            set
            {
                if (selectedPitch != value)
                {
                    selectedPitch = value;
                    OnPropertyChanged(nameof(SelectedPitch));
                }
            }
        }
        private int selectedDuration;
        public int SelectedDuration
        {
            get { return selectedDuration; }
            set
            {
                if (selectedDuration != value)
                {
                    selectedDuration = value;
                    OnPropertyChanged(nameof(SelectedDuration));
                }
            }
        }
        private int selectedOctave;
        public int SelectedOctave
        {
            get { return selectedOctave; }
            set
            {
                if (selectedOctave != value)
                {
                    selectedOctave = value;
                    OnPropertyChanged(nameof(SelectedOctave));
                }
            }
        }
        private bool selectedIsSharp;
        public bool SelectedIsSharp
        {
            get { return selectedIsSharp; }
            set
            {
                if (selectedIsSharp != value)
                {
                    selectedIsSharp = value;
                    OnPropertyChanged(nameof(SelectedIsSharp));
                }
            }
        }
        //public ICommand PlayMusicCommand { get; }

        public MainViewModel()
        {
            SelectedNote = 0;
            VisEdit = Visibility.Collapsed;
            VisMain = Visibility.Visible;
            Notes = new ObservableCollection<Note>();
            Pitches = new List<string> { "C", "D", "E", "F", "G", "A", "B" };

            //PlayMusicCommand = new RelayCommand(PlayMusic);
            recorder = new AudioRecorder();
        }
        private Visibility visMain;
        public Visibility VisMain
        {
            get { return visMain; }
            private set
            {
                visMain = value;
                OnPropertyChanged(nameof(VisMain));
            }
        }
        private Visibility visEdit;
        public Visibility VisEdit
        {
            get { return visEdit; }
            private set
            {
                visEdit = value;
                OnPropertyChanged(nameof(VisEdit));
            }
        }
        public void AddNote(string pitch, int duration, int octave, bool isSharp, string noteIcon)
        {
            var note = new Note(pitch, duration, octave, isSharp, noteIcon)
            {
                XPosition = CalculateXPosition(),
                YPosition = CalculateYPosition(pitch, octave)
            };

            noteIcon = GetNoteIconFromDuration(duration);

            if (Notes.Count() != 30)
            {
                Notes.Add(note);
            }
            else
            {
                MessageBox.Show("Przekroczono ilość nut (max 30 nut)");
            }

            /*Debug.WriteLine($"Dodano nutę: {pitch}, liczba nut w kolekcji: {Notes.Count}");
            Debug.WriteLine($"{note.XPosition} : {note.YPosition}");*/
            foreach (var _note in Notes)
            {
                Debug.WriteLine($"Pitch: {_note.Pitch}, Duration: {_note.Duration}, Octave: {_note.Octave}, IsSharp: {_note.IsSharp}, X: {_note.XPosition}, Y: {_note.YPosition}, NoteIcon: {noteIcon}");
            }

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

        private int CalculateXPosition()
        {
            int baseXPosition = 90; // Początkowa pozycja X dla pierwszej nuty
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
            positionY -= 190;
            return positionY;
        }
        private int CalculateEditedXPosition()
        {
            int baseXPosition = 90; // Początkowa pozycja X dla pierwszej nuty /10
            int noteSpacing = 30; // Odległość między nutami

            // rozmieszczenie nut 
            int positionX = baseXPosition + SelectedNote * noteSpacing;
            return positionX;
        }

        //Mechanizm odgrywania muzyki
        public async void PlayMusic()
        {
            if (isPlaying)
            {
                return;
            }

            //var recorder = new AudioRecorder();
            if (isRecording)
            {
                recorder.StartRecording(recordFilePath);
            }
            foreach (var note in Notes)
            {
                isPlaying = true;
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
            if (isRecording)
            {
                recorder.StopRecording();
                isRecording = false;
            }

            isPlaying = false;
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

                var noteIcon = GetNoteIconFromDuration(int.Parse(parts[1]));

                var note = new Note(parts[0], int.Parse(parts[1]), int.Parse(parts[2]), bool.Parse(parts[3]), noteIcon)
                {
                    XPosition = CalculateXPosition(),
                    YPosition = CalculateYPosition(parts[0], int.Parse(parts[2]))
                };

                Notes.Add(note);
            }
        }
        public void SaveEditedNote(string pitch, int duration, int octave, bool isSharp, string noteIcon)
        {
            var note = new Note(pitch, duration, octave, isSharp, noteIcon)
            {
                XPosition = CalculateEditedXPosition(),
                YPosition = CalculateYPosition(pitch, octave)
            };
            noteIcon = GetNoteIconFromDuration(duration);
            var TempNotes = Notes.ToList();
            TempNotes[SelectedNote] = note;
            Notes = new ObservableCollection<Note>(TempNotes);
        }
        public void NotesComboBoxEdit_SelectionChanged()
        {
            if (SelectedNote >= 0 && SelectedNote < Notes.Count)
            {
                SelectedPitch = Notes.ElementAt(SelectedNote).GetPitch();
                SelectedDuration = Notes.ElementAt(SelectedNote).GetDurationsIndex();
                SelectedIsSharp = Notes.ElementAt(SelectedNote).IsSharp;
                SelectedOctave = Notes.ElementAt(SelectedNote).Octave - 1;
            }
        }
        public void EditButton_Click()
        {
            VisMain = Visibility.Collapsed;
            VisEdit = Visibility.Visible;

        }
        public void SaveButton_Click()
        {
            VisEdit = Visibility.Collapsed;
            VisMain = Visibility.Visible;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void PrepareRecording(string filePath, string format)
        {
            recordFilePath = filePath;
            isRecording = true;
        }

    }
}
