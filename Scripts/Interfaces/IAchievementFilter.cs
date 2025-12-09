using Scripts.Core;

namespace Scripts.Interfaces
{
    public interface IAchievementFilter
    {
        string Name { get; }
        bool IsMatch(Achievement achievement);
    }
}