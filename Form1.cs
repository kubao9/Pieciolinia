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
            Play(@"NotesAudio/c1.wav");
        }

        private void cis_Click(object sender, EventArgs e)
        {
            Play(@"NotesAudio/csharp1.wav");
        }

        private void d_Click(object sender, EventArgs e)
        {
            Play(@"NotesAudio/d1.wav");
        }

        private void dis_Click(object sender, EventArgs e)
        {
            Play(@"NotesAudio/dsharp1.wav");
        }

        private void e_Click(object sender, EventArgs e)
        {
            Play(@"NotesAudio/e1.wav");
        }

        private void f_Click(object sender, EventArgs e)
        {
            Play(@"NotesAudio/f1.wav");
        }

        private void fis_Click(object sender, EventArgs e)
        {
            Play(@"NotesAudio/fsharp1.wav");
        }

        private void g_Click(object sender, EventArgs e)
        {
            Play(@"NotesAudio/g1.wav");
        }

        private void gis_Click(object sender, EventArgs e)
        {
            Play(@"NotesAudio/gsharp1.wav");
        }

        private void a_Click(object sender, EventArgs e)
        {
            Play(@"NotesAudio/a1.wav");
        }

        private void b_Click(object sender, EventArgs e)
        {
            Play(@"NotesAudio/asharp1.wav");
        }

        private void h_Click(object sender, EventArgs e)
        {
            Play(@"NotesAudio/b1.wav");
        }


    }
}