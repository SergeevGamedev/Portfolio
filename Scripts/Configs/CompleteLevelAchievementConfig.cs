using Scripts.Core;
using Scripts.Strategies;
using UnityEngine;

namespace Scripts.Configs
{
    [CreateAssetMenu(fileName = "CompleteLevelAchievement", menuName = "Achievements/Complete Level")]
    public class CompleteLevelAchievementConfig : AchievementConfig
    {
        public override IAchievementStrategy CreateStrategy()
        {
            return new CompleteLevelStrategy();
        }
    }
}