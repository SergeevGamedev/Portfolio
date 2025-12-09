using Assets.Scripts.Core;
using DG.Tweening;
using Scripts.Core;
using UnityEngine.UIElements;

namespace Scripts.Utilities
{
    public class AchievementItemUpdater
    {
        private readonly VisualElement _listContainer;

        public AchievementItemUpdater(VisualElement container)
        {
            _listContainer = container;
        }

        public void UpdateProgress(Achievement achievement)
        {
            var item = FindItem(achievement);
            if (item == null) return;

            var bar = item.Q<ProgressBar>();
            if (bar != null)
            {
                AnimateProgressBar(bar, achievement);
            }
        }

        public void MarkCompleted(Achievement achievement)
        {
            var item = FindItem(achievement);
            if (item == null) return;

            item.AddToClassList(UIConstants.Achievement.ItemCompleted);

            var bar = item.Q<ProgressBar>();
            if (bar != null)
            {
                bar.value = achievement.TargetValue;
                bar.title = $"{achievement.TargetValue}/{achievement.TargetValue}";
            }
        }

        private VisualElement FindItem(Achievement a)
        {
            return _listContainer?.Q<VisualElement>($"achievement-{a.Id}");
        }

        private void AnimateProgressBar(ProgressBar bar, Achievement a)
        {
            DOTween.To(
                () => bar.value, 
                x => {bar.value = x; bar.title = $"{(int)x}/{a.TargetValue}";}, 
                a.CurrentValue, 0.5f)
                .SetEase(Ease.OutQuad);
        }
    }
}