namespace Scripts.Interfaces
{
    public interface IAchievementProgressUpdater
    {
        void UpdateProgress(string achievementId, int increment);
    }
}