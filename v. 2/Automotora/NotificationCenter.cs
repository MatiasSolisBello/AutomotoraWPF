using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automotora
{
    // -----------------------------------
    // Se debe agregar referencia a System.ServiceModel
    // -----------------------------------
    public static class NotificationCenter
    {
        private static SynchronizedCollection<Tuple<string, Action>> _subscribers { get; set; }
        private static SynchronizedCollection<Tuple<string, Action<object>>> _subscribersWithData { get; set; }
        static NotificationCenter()
        {
            _subscribers = new SynchronizedCollection<Tuple<string, Action>>();
            _subscribersWithData = new SynchronizedCollection<Tuple<string, Action<object>>>();
        }
        public static void Subscribe(string subscriptionKey, Action eventToCall)
        {
            _subscribers.Add(new Tuple<string, Action>(subscriptionKey, eventToCall));
        }
        public static void Subscribe(string subscriptionKey, Action<object> eventToCall)
        {
            _subscribersWithData.Add(new Tuple<string, Action<object>>(subscriptionKey, eventToCall));
        }
        public static void Notify(string subscriptionKey)
        {
            foreach (var subscription in _subscribers.Where(s => s.Item1 == subscriptionKey))
            {
                subscription.Item2.BeginInvoke(cb =>
                {
                    if (cb.IsCompleted && cb.AsyncWaitHandle != null)
                    {
                        cb.AsyncWaitHandle.Close();
                    }
                }, null);
            }
        }
        public static void Notify(string subscriptionKey, object data)
        {
            foreach (var subscription in _subscribersWithData.Where(s => s.Item1 == subscriptionKey))
            {
                subscription.Item2.BeginInvoke(data, cb =>
                {
                    if (cb.IsCompleted && cb.AsyncWaitHandle != null)
                    {
                        cb.AsyncWaitHandle.Close();
                    }
                }, null);
            }
        }

    }
}
