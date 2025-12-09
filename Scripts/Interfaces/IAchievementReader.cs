using Scripts.Core;
using System.Collections.Generic;

namespace Scripts.Interfaces
{
    public interface IAchievementReader
    {
        IReadOnlyList<Achievement> GetAchievements();
        Achievement GetAchievementById(string id);
    }
}