using Scripts.Core;
using Scripts.Strategies;
using UnityEngine;

namespace Scripts.Configs
{
    [CreateAssetMenu(fileName = "CollectCoinsAchievement", menuName = "Achievements/Collect Coins")]
    public class CollectCoinsAchievementConfig : AchievementConfig
    {
        public override IAchievementStrategy CreateStrategy()
        {
            return new CollectCoinsStrategy();
        }
    }
}

