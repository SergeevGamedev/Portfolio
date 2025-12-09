using Scripts.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
{
    public class AchievementRepository : IAchievementRepository
    {
        private const string SaveKey = "Achievements";

        public void Save(List<Achievement> achievements)
        {
            var saveData = new List<AchievementSaveData>();

            foreach (var achievement in achievements)
            {
                saveData.Add(new AchievementSaveData
                {
                    Id = achievement.Id,
                    CurrentValue = achievement.CurrentValue,
                    IsCompleted = achievement.IsCompleted,
                    CompletedDate = achievement.CompletedDate?.ToString("o")
                });
            }

            string json = JsonUtility.ToJson(new AchievementListWrapper { achievements = saveData });
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }

        public List<AchievementSaveData> Load()
        {
            string json = PlayerPrefs.GetString(SaveKey, "");

            if (string.IsNullOrEmpty(json))
                return new List<AchievementSaveData>();

            var wrapper = JsonUtility.FromJson<AchievementListWrapper>(json);
            return wrapper.achievements;
        }

        [Serializable]
        private class AchievementListWrapper
        {
            public List<AchievementSaveData> achievements;
        }
    }
}