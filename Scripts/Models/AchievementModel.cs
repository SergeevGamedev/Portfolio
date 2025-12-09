using Scripts.Configs;
using Scripts.Core;
using Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Achievements.Models
{
    public class AchievementModel : IAchievementModel
    {
        private readonly List<Achievement> _achievements;
        private readonly IAchievementRepository _repository;

        public event Action<Achievement> OnAchievementUnlocked;
        public event Action<Achievement> OnProgressChanged;

        public AchievementModel(IEnumerable<AchievementConfig> configs, IAchievementRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _achievements = InitializeAchievements(configs);
            LoadProgress();
        }

        private List<Achievement> InitializeAchievements(IEnumerable<AchievementConfig> configs)
        {
            return configs.Where(c => c != null).Select(c => new Achievement(c)).ToList();
        }

        public IReadOnlyList<Achievement> GetAchievements() => _achievements;

        public Achievement GetAchievementById(string id)
        {
            return _achievements.FirstOrDefault(a => a.Id == id);
        }

        public void UpdateProgress(string achievementId, int increment)
        {
            var achievement = GetAchievementById(achievementId);
            if (achievement == null || achievement.IsCompleted)
                return;

            achievement.CurrentValue = Math.Min(
                achievement.CurrentValue + increment,
                achievement.TargetValue);

            OnProgressChanged?.Invoke(achievement);

            if (ShouldComplete(achievement))
            {
                CompleteAchievement(achievement);
            }

            SaveProgress();
        }

        private bool ShouldComplete(Achievement achievement)
        {
            return achievement.Strategy?.CheckProgress(
                achievement.CurrentValue,
                achievement.TargetValue) ?? false;
        }

        private void CompleteAchievement(Achievement achievement)
        {
            achievement.IsCompleted = true;
            achievement.CompletedDate = DateTime.Now;
            achievement.CurrentValue = achievement.TargetValue;

            OnAchievementUnlocked?.Invoke(achievement);
            SaveProgress();
        }

        private void SaveProgress()
        {
            try
            {
                _repository.Save(_achievements);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[AchievementModel] Save failed: {ex.Message}");
            }
        }

        private void LoadProgress()
        {
            try
            {
                var savedData = _repository.Load();
                foreach (var data in savedData)
                {
                    var achievement = GetAchievementById(data.Id);
                    if (achievement != null)
                    {
                        achievement.CurrentValue = data.CurrentValue;
                        achievement.IsCompleted = data.IsCompleted;
                        if (DateTime.TryParse(data.CompletedDate, out var date))
                            achievement.CompletedDate = date;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[AchievementModel] Load failed: {ex.Message}");
            }
        }
    }
}