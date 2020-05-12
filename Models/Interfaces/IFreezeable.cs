namespace CoronGame.Models.Interfaces
{
    public interface IFreezable
    {
        int FreezeTime { get; set; }
        int Time { get; set; }
        bool IsFreeze { get; set; }
    }
}