using System.IO;
using NAudio.Wave;

namespace CoronGame.SoundProvider.SoundPlayers
{
    public abstract class SoundPlayer
    {
        private readonly string startupPath = Directory.GetParent(@"../../").FullName;
        private LoopStream stream;

        protected SoundPlayer(string filePath, bool enableLooping)
        {
            var reader = new WaveFileReader(startupPath + filePath);
            stream = new LoopStream(reader, enableLooping);
            Init();
        }

        protected SoundPlayer(string filePath, bool enableLooping, float volume)
           : this(filePath, enableLooping) { }

        private IWavePlayer WavePlayer { get; set; }

        public void Play()
        {
            if (WavePlayer.PlaybackState == PlaybackState.Playing) return;
            Init();
            WavePlayer.Play();
        }

        public void Pause()
        {
            WavePlayer.Pause();
        }

        private void Init()
        {
            WavePlayer = new WaveOut();
            stream.Position = 0;
            WavePlayer.Init(stream);
        }
    }
}