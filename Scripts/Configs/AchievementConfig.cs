using Scripts.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.Configs
{
    public abstract class AchievementConfig : ScriptableObject
    {
        [PreviewField(80, ObjectFieldAlignment.Left)]
        public Sprite Icon;

        [Required]
        public string AchievementId;

        [Required]
        public string Title;

        [TextArea(3, 5)]
        public string Description;

        public int TargetValue = 100;

        public int RewardCoins = 100;

        public abstract IAchievementStrategy CreateStrategy();
    }
}