using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pieciolinia.Model
{
    public class Note
    {
        public string Pitch { get; set; } // np. "C", "D#", "E"
        public int Duration { get; set; } // np. 1 dla całej nuty, 2 dla półnuty itd.
        public bool IsSharp { get; set; } // true jeśli nuta jest krzyżykiem

        //nowe
        public int Octave { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }

        public Note(string pitch, int duration, int octave, bool isSharp = false)
        {
            Pitch = pitch;
            Duration = duration;
            Octave = octave;
            IsSharp = isSharp;
        }
    }

}
