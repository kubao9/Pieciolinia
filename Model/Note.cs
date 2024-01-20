using System.Runtime.CompilerServices;

namespace Pieciolinia.Model
{
    public class Note
    {
        public string Pitch { get; set; } // np. "C", "D#", "E"
        public int Duration { get; set; } // np. 1 dla całej nuty, 2 dla półnuty itd.
        public bool IsSharp { get; set; } // true jeśli nuta jest krzyżykiem
        public string NoteIcon { get; set; }

        //nowe
        public int Octave { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }

        public Note(string pitch = "C", int duration = 1, int octave = 1, bool isSharp = false, string noteIcon = "♪")
        {
            Pitch = pitch;
            Duration = duration;
            Octave = octave;
            IsSharp = isSharp;
            NoteIcon = noteIcon;
        }
        public int GetPitch()
        {
            switch (Pitch)
            {
                case "C":
                    return 0;
                case "D":
                    return 1;
                case "E":
                    return 2;
                case "F":
                    return 3;
                case "G":
                    return 4;
                case "A":
                    return 5;
                case "B":
                    return 6;
                default:
                    return 0;
            }
        }
        public int GetDurationsIndex() 
        {
            switch (Duration)
            { 
            case 1 :
                    return 0;
            case 2 :
                    return 1;
            case 4 :
                    return 2;
            case 8 : 
                    return 3;
            case 16 : 
                    return 4;
            default:
                    return 0;
            }
        }
    }
}
