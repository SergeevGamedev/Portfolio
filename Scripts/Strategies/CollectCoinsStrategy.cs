using Scripts.Core;

namespace Scripts.Strategies
{
    public class CollectCoinsStrategy : IAchievementStrategy
    {
        public bool CheckProgress(int current, int target) => current >= target;
        public string GetEventKey() => GameEventsConstants.CoinCollected;
    }
}