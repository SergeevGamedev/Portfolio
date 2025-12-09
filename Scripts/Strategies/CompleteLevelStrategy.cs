using Scripts.Core;

namespace Scripts.Strategies
{
    public class CompleteLevelStrategy : IAchievementStrategy
    {
        public bool CheckProgress(int current, int target) => current >= target;
        public string GetEventKey() => GameEventsConstants.LevelCompleted;
    }
}