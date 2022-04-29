using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goat.EventEngine
{
    public class EventBus
    {
        private readonly SubscribersStorage _subscribers;
        private static EventBus? _instance;

        public static EventBus Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EventBus();

                return _instance;
            }
        }

        private EventBus()
        {
            _subscribers = new SubscribersStorage();
        }

        public void Subscribe<TEvent>(ISubscriber<TEvent> subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            _subscribers.Add(subscriber);
        }

        public void Unsubscribe<TEvent>(ISubscriber<TEvent> subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            _subscribers.Remove(subscriber);
        }

        public void Publish<TEvent>(TEvent ev)
        {
            var subscribers = _subscribers.Get<TEvent>();
            foreach (var subscriber in subscribers)
            {
                subscriber.HandleEvent(ev);
            }
        }
    }
}
