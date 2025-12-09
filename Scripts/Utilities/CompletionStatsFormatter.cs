using Scripts.Core;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Utilities
{
    public class CompletionStatsFormatter
    {
        public string Format(IEnumerable<Achievement> achievements)
        {
            int completed = achievements.Count(a => a.IsCompleted);
            int total = achievements.Count();
            return $"Completed {completed:000}/{total:000}";
        }
    }
}