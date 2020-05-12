namespace CoronGame.Models.Interfaces
{
    public interface IKillable
    {
        int Damage { get; }
        
        int Life { get; }
        
        bool IsAlive { get; set; }

        bool CanKill { get; set; }
    }
}