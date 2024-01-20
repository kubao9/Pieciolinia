namespace Pieciolinia.Tests
{
    public class NoteTests
    {

        [Test]
        public void InitializingNoteWithoutArgsSetsProprerPitch()
        {
            Note note = new Note();
            Assert.That(Equals(note.Pitch, "C"));
        }

        [Test]
        public void InitializingNoteWithoutArgsSetsProprerDuration()
        {
            Note note = new Note();
            Assert.That(Equals(note.Duration, 1));
        }

        [Test]
        public void InitializingNoteWithoutArgsSetsProprerOctave()
        {
            Note note = new Note();
            Assert.That(Equals(note.Octave, 1));
        }

        [Test]
        public void InitializingNoteWithoutArgsSetsProprerIsSharp()
        {
            Note note = new Note();
            Assert.That(Equals(note.IsSharp, false));
        }

        [Test]
        public void InitializingNoteWithoutArgsSetsProprerNoteIcon()
        {
            Note note = new Note();
            Assert.That(Equals(note.NoteIcon, "♪"));
        }

        [Test]
        public void GetPitchReturnsProperValueWithPitchEqualsC()
        {
            Note note = new Note("C");
            int result = note.GetPitch();
            Assert.True(Equals(0, result));
        }

        [Test]
        public void GetPitchReturnsProperValueWithPitchEqualsD()
        {
            Note note = new Note("D");
            int result = note.GetPitch();
            Assert.True(Equals(1, result));
        }

        [Test]
        public void GetPitchReturnsProperValueWithPitchEqualsE()
        {
            Note note = new Note("E");
            int result = note.GetPitch();
            Assert.True(Equals(2, result));
        }

        [Test]
        public void GetPitchReturnsProperValueWithPitchEqualsF()
        {
            Note note = new Note("F");
            int result = note.GetPitch();
            Assert.True(Equals(3, result));
        }

        [Test]
        public void GetPitchReturnsProperValueWithPitchEqualsG()
        {
            Note note = new Note("G");
            int result = note.GetPitch();
            Assert.True(Equals(4, result));
        }

        [Test]
        public void GetPitchReturnsProperValueWithPitchEqualsA()
        {
            Note note = new Note("A");
            int result = note.GetPitch();
            Assert.True(Equals(5, result));
        }

        [Test]
        public void GetPitchReturnsProperValueWithPitchEqualsB()
        {
            Note note = new Note("B");
            int result = note.GetPitch();
            Assert.True(Equals(6, result));
        }

        [Test]
        public void GetPitchReturnsDefaultWithoutCaseArgument()
        {
            Note note = new Note("");
            int result = note.GetPitch();
            Assert.True(Equals(0, result));
        }

        [Test]
        public void GetDurationIndexReturnsProperValueWitchDurationEquals1()
        {
            Note note = new Note();
            note.Duration = 1;
            int result = note.GetDurationsIndex();
            Assert.True(Equals(0, result));
        }

        [Test]
        public void GetDurationIndexReturnsProperValueWitchDurationEquals2()
        {
            Note note = new Note();
            note.Duration = 2;
            int result = note.GetDurationsIndex();
            Assert.True(Equals(1, result));
        }

        [Test]
        public void GetDurationIndexReturnsProperValueWitchDurationEquals4()
        {
            Note note = new Note();
            note.Duration = 4;
            int result = note.GetDurationsIndex();
            Assert.True(Equals(2, result));
        }

        [Test]
        public void GetDurationIndexReturnsProperValueWitchDurationEquals8()
        {
            Note note = new Note();
            note.Duration = 8;
            int result = note.GetDurationsIndex();
            Assert.True(Equals(3, result));
        }

        [Test]
        public void GetDurationIndexReturnsProperValueWitchDurationEquals16()
        {
            Note note = new Note();
            note.Duration = 16;
            int result = note.GetDurationsIndex();
            Assert.True(Equals(4, result));
        }

        [Test]
        public void GetDurationIndexReturnsDefaultWithoutCaseArgument()
        {
            Note note = new Note();
            note.Duration = 100;
            int result = note.GetDurationsIndex();
            Assert.True(Equals(0, result));
        }
    }
}