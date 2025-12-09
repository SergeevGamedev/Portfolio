using Scripts.Core;
using Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Utilities
{
    public class AllAchievementsFilter : IAchievementFilter
    {
        public string Name => "All";
        public bool IsMatch(Achievement achievement) => true;
    }

    public class CompletedFilter : IAchievementFilter
    {
        public string Name => "Completed";
        public bool IsMatch(Achievement achievement) => achievement.IsCompleted;
    }

    public class InProgressFilter : IAchievementFilter
    {
        public string Name => "InProgress";
        public bool IsMatch(Achievement achievement) => !achievement.IsCompleted;
    }

    public class AchievementFilterManager
    {
        private readonly List<IAchievementFilter> _filters;
        private IAchievementFilter _currentFilter;

        public AchievementFilterManager(List<IAchievementFilter> filters)
        {
            _filters = filters;
            _currentFilter = _filters.FirstOrDefault() ?? new AllAchievementsFilter();
        }

        public IEnumerable<IAchievementFilter> GetFilters() => _filters;
        public IAchievementFilter GetCurrent() => _currentFilter;
        public void SetFilter(IAchievementFilter filter) => _currentFilter = filter;

        public IEnumerable<Achievement> Filter(IEnumerable<Achievement> achievements)
        {
            return achievements.Where(a => _currentFilter.IsMatch(a));
        }
    }
}