using Achievements.Models;
using Scripts.Configs;
using Scripts.Core;
using Scripts.Data;
using Scripts.Interfaces;
using Scripts.Services;
using Scripts.Views;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Controllers
{
    public class AchievementSystemInitializer : MonoBehaviour
    {
        [SerializeField] private List<AchievementConfig> _achievementConfigs;
        [SerializeField] private AchievementView _achievementView;

        private IAchievementModel _model;
        private AchievementController _controller;

        private void Start()
        {
            InitializeSystem();
        }

        private void InitializeSystem()
        {
            if (!Validate()) return;

            IAchievementRepository repository = new AchievementRepository();
            IEventBus eventBus = new GameEventBusAdapter();

            _model = new AchievementModel(_achievementConfigs, repository);
            _controller = new AchievementController(_model, eventBus);

            _achievementView.Initialize(_model);
        }

        private bool Validate()
        {
            if (_achievementConfigs == null || _achievementConfigs.Count == 0)
            {
                Debug.LogError("No configs assigned!");
                return false;
            }
            if (_achievementView == null)
            {
                Debug.LogError("View not assigned!");
                return false;
            }
            return true;
        }

        private void OnDestroy()
        {
            _controller?.Dispose();
        }
    }
}