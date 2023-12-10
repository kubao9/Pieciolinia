namespace Pieciolinia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Play(string notePath)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(notePath);
            player.Play();
        }

        private void c_Click(object sender, EventArgs e)
        {
            notes += (recordState) ? "c\n" : String.Empty;
            Play(@"NotesAudio/c1.wav");
        }

        private void cis_Click(object sender, EventArgs e)
        {
            notes += (recordState) ? "cis\n" : String.Empty;
            Play(@"NotesAudio/csharp1.wav");
        }

        private void d_Click(object sender, EventArgs e)
        {
            notes += (recordState) ? "d\n" : String.Empty;
            Play(@"NotesAudio/d1.wav");
        }

        private void dis_Click(object sender, EventArgs e)
        {
            notes += (recordState) ? "dis\n" : String.Empty;
            Play(@"NotesAudio/dsharp1.wav");
        }

        private void e_Click(object sender, EventArgs e)
        {
            notes += (recordState) ? "e\n" : String.Empty;
            Play(@"NotesAudio/e1.wav");
        }

        private void f_Click(object sender, EventArgs e)
        {
            notes += (recordState) ? "f\n" : String.Empty;
            Play(@"NotesAudio/f1.wav");
        }

        private void fis_Click(object sender, EventArgs e)
        {
            notes += (recordState) ? "fis\n" : String.Empty;
            Play(@"NotesAudio/fsharp1.wav");
        }

        private void g_Click(object sender, EventArgs e)
        {
            notes += (recordState) ? "g\n" : String.Empty;
            Play(@"NotesAudio/g1.wav");
        }

        private void gis_Click(object sender, EventArgs e)
        {
            notes += (recordState) ? "gis\n" : String.Empty;
            Play(@"NotesAudio/gsharp1.wav");
        }

        private void a_Click(object sender, EventArgs e)
        {
            notes += (recordState) ? "a\n" : String.Empty;
            Play(@"NotesAudio/a1.wav");
        }

        private void b_Click(object sender, EventArgs e)
        {
            notes += (recordState) ? "b\n" : String.Empty;
            Play(@"NotesAudio/asharp1.wav");
        }

        private void h_Click(object sender, EventArgs e)
        {
            notes += (recordState) ? "h\n" : String.Empty;
            Play(@"NotesAudio/b1.wav");
        }

        private bool recordState = false;
        private string notes = String.Empty;

        private void recordButton_Click(object sender, EventArgs e)
        {
            if (recordState)
            {
                var loc = "temp.txt";

                File.WriteAllText(loc, notes);

                //The text input is done
                MessageBox.Show("Zarejestrowano melodie.");
                notes = String.Empty;
            }

            recordState = !recordState;
        }
    }
}