namespace CoronGame.Models.Interfaces
{
    public interface IJump
    {
        public int JumpHeight { get; }

        public bool IsJumped { get; }
    }
}