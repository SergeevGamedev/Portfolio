using Scripts.Interfaces;
using Scripts.Services;
using System;
using System.Collections.Generic;

namespace Scripts.Controllers
{
    public class AchievementController : IDisposable
    {
        private readonly IAchievementModel _model;
        private readonly IEventBus _eventBus;
        private readonly Dictionary<string, IDisposable> _subscriptions;

        public AchievementController(IAchievementModel model, IEventBus eventBus)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _subscriptions = new Dictionary<string, IDisposable>();

            SubscribeToGameEvents();
        }

        private void SubscribeToGameEvents()
        {
            var achievements = _model.GetAchievements();

            foreach (var achievement in achievements)
            {
                if (achievement.IsCompleted || achievement.Strategy == null)
                    continue;

                string eventKey = achievement.Strategy.GetEventKey();
                if (!_subscriptions.ContainsKey(eventKey))
                {
                    _subscriptions[eventKey] = _eventBus.Subscribe(
                        eventKey,
                        _ => HandleEvent(eventKey));
                }
            }
        }

        private void HandleEvent(string eventKey)
        {
            var achievements = _model.GetAchievements();

            foreach (var achievement in achievements)
            {
                if (!achievement.IsCompleted &&
                    achievement.Strategy?.GetEventKey() == eventKey)
                {
                    _model.UpdateProgress(achievement.Id, 1);
                }
            }
        }

        public void Dispose()
        {
            foreach (var subscription in _subscriptions.Values)
                subscription?.Dispose();
            _subscriptions.Clear();
        }
    }

    public class GameEventBusAdapter : IEventBus
    {
        public void Publish(string eventKey, object data = null)
        {
            GameEventBus.Publish(eventKey, data);
        }

        public IDisposable Subscribe(string eventKey, Action<object> action)
        {
            return GameEventBus.Subscribe(eventKey, action);
        }
    }
}