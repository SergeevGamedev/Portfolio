using Assets.Scripts.Core;
using Scripts.Core;
using Scripts.Interfaces;
using UnityEngine.UIElements;

namespace Scripts.Views
{
    public class AchievementItemRenderer : IAchievementItemRenderer
    {
        public VisualElement CreateAchievementItem(Achievement achievement)
        {
            var container = new VisualElement();
            container.AddToClassList(UIConstants.Achievement.Item);
            container.name = $"achievement-{achievement.Id}";

            if (achievement.IsCompleted)
                container.AddToClassList(UIConstants.Achievement.ItemCompleted);

            AddIcon(container, achievement);
            AddInfo(container, achievement);

            return container;
        }

        private void AddIcon(VisualElement container, Achievement a)
        {
            var icon = new VisualElement();
            icon.AddToClassList(UIConstants.Achievement.Icon);
            if (a.Icon != null)
                icon.style.backgroundImage = new StyleBackground(a.Icon);
            container.Add(icon);
        }

        private void AddInfo(VisualElement container, Achievement a)
        {
            var info = new VisualElement();
            info.AddToClassList(UIConstants.Achievement.Info);

            info.Add(CreateLabel(a.Title, UIConstants.Achievement.Title));
            info.Add(CreateLabel(a.Description, UIConstants.Achievement.Description));
            info.Add(CreateProgressBar(a));

            container.Add(info);
        }

        private Label CreateLabel(string text, string className)
        {
            var label = new Label(text);
            label.AddToClassList(className);
            return label;
        }

        private ProgressBar CreateProgressBar(Achievement a)
        {
            var bar = new ProgressBar();
            bar.AddToClassList(UIConstants.Achievement.Progress);
            bar.lowValue = 0;
            bar.highValue = a.TargetValue;
            bar.value = a.CurrentValue;
            bar.title = $"{a.CurrentValue}/{a.TargetValue}";
            return bar;
        }
    }
}