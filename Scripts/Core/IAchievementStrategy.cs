namespace Scripts.Core
{
    public interface IAchievementStrategy
    {
        bool CheckProgress(int currentValue, int targetValue);
        string GetEventKey();
    }
}