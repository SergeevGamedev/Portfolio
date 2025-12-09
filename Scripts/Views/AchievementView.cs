using Assets.Scripts.Core;
using DG.Tweening;
using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Services;
using Scripts.Utilities;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Scripts.Views
{
    public class AchievementView : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;

        private VisualElement _root;
        private VisualElement _achievementList;
        private Label _completionStats;

        private IAchievementModel _model;
        private AchievementFilterManager _filterManager;
        private IAchievementItemRenderer _renderer;
        private INotificationService _notificationService;
        private AchievementItemUpdater _itemUpdater;
        private CompletionStatsFormatter _statsFormatter;

        public void Initialize(IAchievementModel model)
        {
            _model = model;

            SetupUI();
            SetupDependencies();
            SetupFilters();
            SubscribeToEvents();

            Render();
        }

        private void SetupUI()
        {
            _root = _uiDocument.rootVisualElement;
            _achievementList = _root.Q<VisualElement>(UIConstants.Layout.AchievementList);
            _completionStats = _root.Q<Label>(UIConstants.Layout.CompletionStats);
        }

        private void SetupDependencies()
        {
            var filters = new List<IAchievementFilter>
            {
                new AllAchievementsFilter(),
                new CompletedFilter(),
                new InProgressFilter()
            };

            _filterManager = new AchievementFilterManager(filters);
            _renderer = new AchievementItemRenderer();
            _notificationService = new NotificationService(_root);
            _itemUpdater = new AchievementItemUpdater(_achievementList);
            _statsFormatter = new CompletionStatsFormatter();
        }

        private void SetupFilters()
        {
            foreach (var filter in _filterManager.GetFilters())
            {
                var button = _root.Q<Button>($"{filter.Name}Button");
                if (button != null)
                    button.clicked += () => ApplyFilter(filter);
            }
        }

        private void SubscribeToEvents()
        {
            _model.OnProgressChanged += OnProgressChanged;
            _model.OnAchievementUnlocked += OnAchievementUnlocked;
        }

        private void ApplyFilter(IAchievementFilter filter)
        {
            _filterManager.SetFilter(filter);
            Render();
        }

        private void Render()
        {
            _achievementList.Clear();
            UpdateStats();

            var filtered = _filterManager.Filter(_model.GetAchievements());

            foreach (var achievement in filtered)
            {
                var item = _renderer.CreateAchievementItem(achievement);
                _achievementList.Add(item);
            }
        }

        private void UpdateStats()
        {
            if (_completionStats != null)
                _completionStats.text = _statsFormatter.Format(_model.GetAchievements());
        }

        private void OnProgressChanged(Achievement achievement)
        {
            UpdateStats();
            _itemUpdater.UpdateProgress(achievement);
        }

        private async void OnAchievementUnlocked(Achievement achievement)
        {
            UpdateStats();
            _itemUpdater.MarkCompleted(achievement);
            await _notificationService.ShowNotification(achievement);
        }

        private void OnDestroy()
        {
            if (_model != null)
            {
                _model.OnProgressChanged -= OnProgressChanged;
                _model.OnAchievementUnlocked -= OnAchievementUnlocked;
            }
            DOTween.Kill(this);
        }

        [Title("Testing")]
        [Button("Kill Enemy", ButtonSizes.Large)]
        [GUIColor(1f, 0.7f, 0.7f)]
        private void TestKillEnemy() =>
            GameEventBus.Publish(GameEventsConstants.EnemyKilled);

        [Button("Collect Coin", ButtonSizes.Large)]
        [GUIColor(1f, 0.9f, 0.5f)]
        private void TestCollectCoin() =>
            GameEventBus.Publish(GameEventsConstants.CoinCollected);

        [Button("Complete Level", ButtonSizes.Large)]
        [GUIColor(0.7f, 1f, 0.7f)]
        private void TestCompleteLevel() =>
            GameEventBus.Publish(GameEventsConstants.LevelCompleted);

        [Button("Reset Progress (Only Edit Mode)", ButtonSizes.Large)]
        [GUIColor(1f, 1f, 1f)]
        private void ResetProgress()
        {
            PlayerPrefs.DeleteKey("Achievements");
            PlayerPrefs.Save();
        }
    }
}