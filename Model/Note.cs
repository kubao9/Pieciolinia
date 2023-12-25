namespace Pieciolinia.Model
{
    public class Note
    {
        public string Pitch { get; set; } // np. "C", "D#", "E"
        public int Duration { get; set; } // np. 1 dla całej nuty, 2 dla półnuty itd.
        public bool IsSharp { get; set; } // true jeśli nuta jest krzyżykiem
        public string NoteIcon {get; set;}

        //nowe
        public int Octave { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }

        public Note(string pitch, int duration, int octave, bool isSharp = false, string noteIcon = "♪")
        {
            Pitch = pitch;
            Duration = duration;
            Octave = octave;
            IsSharp = isSharp;
            NoteIcon = noteIcon;
        }
    }

}
