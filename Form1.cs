namespace Pieciolinia
{
    public partial class Form1 : Form
    {
        private DateTime lastNoteStartTime;
        private bool recordState = false;
        private string notes = String.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        public void Play(string notePath)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(notePath);
            player.Play();
        }

        private void RecordNote(string note)
        {
            if (recordState)
            {
                var duration = DateTime.Now - lastNoteStartTime;
                //notes += $"{note} {duration.TotalMilliseconds}ms\n";
                notes += $"{note} {duration.TotalMilliseconds:F0}ms\n";
            }

            lastNoteStartTime = DateTime.Now;
        }

        private void c_MouseDown(object sender, EventArgs e)
        {
            RecordNote("c");
            Play(@"NotesAudio/c1.wav");
        }

        private void cis_MouseDown(object sender, EventArgs e)
        {
            RecordNote("cis");
            Play(@"NotesAudio/csharp1.wav");
        }

        private void d_MouseDown(object sender, EventArgs e)
        {
            RecordNote("d");
            Play(@"NotesAudio/d1.wav");
        }

        private void dis_MouseDown(object sender, EventArgs e)
        {
            RecordNote("dis");
            Play(@"NotesAudio/dsharp1.wav");
        }

        private void e_MouseDown(object sender, EventArgs e)
        {
            RecordNote("e");
            Play(@"NotesAudio/e1.wav");
        }

        private void f_MouseDown(object sender, EventArgs e)
        {
            RecordNote("f");
            Play(@"NotesAudio/f1.wav");
        }

        private void fis_MouseDown(object sender, EventArgs e)
        {
            RecordNote("fis");
            Play(@"NotesAudio/fsharp1.wav");
        }

        private void g_MouseDown(object sender, EventArgs e)
        {
            RecordNote("g");
            Play(@"NotesAudio/g1.wav");
        }

        private void gis_MouseDown(object sender, EventArgs e)
        {
            RecordNote("gis");
            Play(@"NotesAudio/gsharp1.wav");
        }

        private void a_MouseDown(object sender, EventArgs e)
        {
            RecordNote("a");
            Play(@"NotesAudio/a1.wav");
        }

        private void b_MouseDown(object sender, EventArgs e)
        {
            RecordNote("b");
            Play(@"NotesAudio/asharp1.wav");
        }

        private void h_MouseDown(object sender, EventArgs e)
        {
            RecordNote("h");
            Play(@"NotesAudio/b1.wav");
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            if (recordState)
            {
                var loc = "temp.txt";
                File.WriteAllText(loc, notes);
                MessageBox.Show("Zarejestrowano melodiê.");
                notes = String.Empty;
            }
            else
            {
                lastNoteStartTime = DateTime.Now; // Rozpoczêcie œledzenia czasu na pocz¹tku nagrywania
            }

            recordState = !recordState;
        }
    }
}