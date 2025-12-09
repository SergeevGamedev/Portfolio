using System;

namespace Scripts.Data
{
    [Serializable]
    public class AchievementSaveData
    {
        public string Id;
        public int CurrentValue;
        public bool IsCompleted;
        public string CompletedDate;
    }
}