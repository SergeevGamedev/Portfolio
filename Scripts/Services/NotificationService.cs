using Assets.Scripts.Core;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Scripts.Core;
using Scripts.Interfaces;
using System;
using UnityEngine.UIElements;

namespace Scripts.Services
{
    public class NotificationService : INotificationService
    {
        private readonly VisualElement _container;
        private readonly float _duration;

        public NotificationService(VisualElement root, float duration = 3f)
        {
            _duration = duration;
            _container = CreateContainer(root);
        }

        private VisualElement CreateContainer(VisualElement root)
        {
            var container = root.Q<VisualElement>(UIConstants.Layout.NotificationContainer);
            if (container == null)
            {
                container = new VisualElement { name = UIConstants.Layout.NotificationContainer };
                container.AddToClassList("notification");
                root.Add(container);
            }
            return container;
        }

        public async UniTask ShowNotification(Achievement achievement)
        {
            var notification = CreateNotification(achievement);
            _container.Add(notification);

            await AnimateIn(notification);
            await UniTask.Delay(TimeSpan.FromSeconds(_duration));
            await AnimateOut(notification);

            _container.Remove(notification);
        }

        private VisualElement CreateNotification(Achievement a)
        {
            var notif = new VisualElement();
            notif.AddToClassList(UIConstants.Notification.Item);
            notif.Add(CreateLabel("Achievement Unlocked!", UIConstants.Notification.Title));
            notif.Add(CreateLabel(a.Title, UIConstants.Notification.AchievementTitle));
            notif.Add(CreateLabel($"+{a.RewardCoins} coins", UIConstants.Notification.Reward));

            notif.style.opacity = 0;
            notif.style.translate = new Translate(0, new Length(-50, LengthUnit.Pixel));

            return notif;
        }

        private Label CreateLabel(string text, string className)
        {
            var label = new Label(text);
            label.AddToClassList(className);
            return label;
        }

        private async UniTask AnimateIn(VisualElement elem)
        {
            var seq = DOTween.Sequence();
            seq.Append(DOTween.To(
                () => 0f, 
                x => elem.style.opacity = x, 1f, 
                0.3f));
            seq.Join(DOTween.To(
                () => -50f,
                x => elem.style.translate = new Translate(0, new Length(x, LengthUnit.Pixel)),
                0, 0.3f)
                .SetEase(Ease.OutBack));
            
            await seq.AsyncWaitForCompletion();
        }

        private async UniTask AnimateOut(VisualElement elem)
        {
            var seq = DOTween.Sequence();
            seq.Append(DOTween.To(
                () => 1f,
                x => elem.style.opacity = x, 
                0f, 
                0.3f));
            seq.Join(DOTween.To(
                () => 0f,
                x => elem.style.translate = new Translate(0, new Length(x, LengthUnit.Pixel)),
                50f, 0.3f)
                .SetEase(Ease.InBack));
            
            await seq.AsyncWaitForCompletion();
        }
    }
}