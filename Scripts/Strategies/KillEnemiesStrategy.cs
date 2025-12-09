using Scripts.Core;

namespace Scripts.Strategies
{
    public class KillEnemiesStrategy : IAchievementStrategy
    {
        public bool CheckProgress(int current, int target) => current >= target;
        public string GetEventKey() => GameEventsConstants.EnemyKilled;
    }
}