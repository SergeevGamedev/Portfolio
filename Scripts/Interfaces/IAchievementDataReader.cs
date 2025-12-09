using Scripts.Data;
using System.Collections.Generic;

namespace Scripts.Interfaces
{
    public interface IAchievementDataReader
    {
        List<AchievementSaveData> Load();
    }
}