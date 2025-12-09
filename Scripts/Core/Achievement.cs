using Scripts.Configs;
using System;
using UnityEngine;

namespace Scripts.Core
{
    [Serializable]
    public class Achievement
    {
        public string Id;
        public string Title;
        public string Description;
        public Sprite Icon;
        public int CurrentValue;
        public int TargetValue;
        public bool IsCompleted;
        public DateTime? CompletedDate;
        public int RewardCoins;

        public IAchievementStrategy Strategy;

        public float Progress => Mathf.Clamp01((float)CurrentValue / TargetValue);

        public Achievement(AchievementConfig config)
        {
            Id = config.AchievementId;
            Title = config.Title;
            Description = config.Description;
            Icon = config.Icon;
            TargetValue = config.TargetValue;
            RewardCoins = config.RewardCoins;
            CurrentValue = 0;
            IsCompleted = false;
            Strategy = config.CreateStrategy();
        }
    }
}