using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goat.EventEngine
{
    internal class SubscribersStorage
    {
        private readonly Dictionary<Type, List<object>> _map;

        public SubscribersStorage()
        {
            _map = new Dictionary<Type, List<object>>();
        }

        public void Add<TEvent>(ISubscriber<TEvent> subscriber)
        {
            if (!_map.TryGetValue(typeof(TEvent), out var subscribers))
            {
                subscribers = new List<object>();
                _map.Add(typeof(TEvent), subscribers);
            }

            subscribers.Add(subscriber);
        }

        public void Remove<TEvent>(ISubscriber<TEvent> subscriber)
        {
            if (!_map.TryGetValue(typeof(TEvent), out var subscribers))
                throw new InvalidOperationException($"No one subscribed to the event {typeof(TEvent).FullName}");

            subscribers.Remove(subscriber);
        }

        public IEnumerable<ISubscriber<TEvent>> Get<TEvent>()
        {
            if (!_map.TryGetValue(typeof(TEvent), out var subscribers))
                return new List<ISubscriber<TEvent>>();

            return subscribers.Cast<ISubscriber<TEvent>>();
        }
    }
}
