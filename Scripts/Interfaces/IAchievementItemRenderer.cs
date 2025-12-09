using Scripts.Core;
using UnityEngine.UIElements;

namespace Scripts.Interfaces
{
    public interface IAchievementItemRenderer
    {
        VisualElement CreateAchievementItem(Achievement achievement);
    }
}