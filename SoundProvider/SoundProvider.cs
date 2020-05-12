using CoronGame.SoundProvider.SoundPlayers;

namespace CoronGame.SoundProvider
{
    public class SoundProvider
    {
        private readonly SoundPlayer beginningPlayer;

        public SoundProvider()
        {
            beginningPlayer = new BeginningPlayer();
        }

        public void BeginningPlay()
        {
            beginningPlayer.Play();
        }
    }
}