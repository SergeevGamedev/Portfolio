using Scripts.Core;
using System.Collections.Generic;

namespace Scripts.Interfaces
{
    public interface IAchievementDataWriter
    {
        void Save(List<Achievement> achievements);
    }
}