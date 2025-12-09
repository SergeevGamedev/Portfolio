using Cysharp.Threading.Tasks;
using Scripts.Core;

namespace Scripts.Interfaces
{
    public interface INotificationService
    {
        UniTask ShowNotification(Achievement achievement);
    }
}