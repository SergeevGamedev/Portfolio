using System;

namespace Scripts.Interfaces
{
    public interface IEventBus
    {
        void Publish(string eventKey, object data = null);
        IDisposable Subscribe(string eventKey, Action<object> action);
    }
}