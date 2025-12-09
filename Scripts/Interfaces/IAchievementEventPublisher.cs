using Scripts.Core;
using System;

namespace Scripts.Interfaces
{
    public interface IAchievementEventPublisher
    {
        event Action<Achievement> OnAchievementUnlocked;
        event Action<Achievement> OnProgressChanged;
    }
}