namespace Server.Items
{
    public interface ICursedItem
    {
        bool IsCursed { get; }
        void StartCurseTimer();
        void EndCurse();
    }
}