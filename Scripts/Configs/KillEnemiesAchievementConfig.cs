using Scripts.Core;
using Scripts.Strategies;
using UnityEngine;

namespace Scripts.Configs
{
    [CreateAssetMenu(fileName = "KillEnemiesAchievement", menuName = "Achievements/Kill Enemies")]
    public class KillEnemiesAchievementConfig : AchievementConfig
    {
        public override IAchievementStrategy CreateStrategy()
        {
            return new KillEnemiesStrategy();
        }
    }
}
