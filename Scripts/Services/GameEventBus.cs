using System;
using System.Collections.Generic;
using UniRx;

namespace Scripts.Services
{
    public static class GameEventBus
    {
        private static readonly Dictionary<string, Subject<object>> subjects = new Dictionary<string, Subject<object>>();

        public static void Publish(string eventKey, object data = null)
        {
            if (!subjects.ContainsKey(eventKey))
            {
                subjects[eventKey] = new Subject<object>();
            }

            subjects[eventKey].OnNext(data);
        }

        public static IDisposable Subscribe(string eventKey, Action<object> action)
        {
            if (!subjects.ContainsKey(eventKey))
            {
                subjects[eventKey] = new Subject<object>();
            }

            var subscription = subjects[eventKey].Subscribe(action);

            return subscription;
        }
    }
}