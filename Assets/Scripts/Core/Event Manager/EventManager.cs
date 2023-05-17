using System.Collections.Generic;

namespace Core.Event_Manager
{
    public class EventManager
    {
        private static EventManager _instance;
        public delegate void EventCallback(params object[] args);
        private readonly Dictionary<string, EventCallback> _events = new ();

        public static EventManager Instance => _instance ??= new EventManager();

        private EventManager(){}

        public void Subscribe(string eventName, EventCallback callback)
        {
            if (_events.ContainsKey(eventName))
            {
                _events[eventName] += callback;
                return;
            }

            _events.Add(eventName, callback);
        }

        public void Unsubscribe(string eventName, EventCallback callback)
        {
            if (_events.ContainsKey(eventName))
            {
                _events[eventName] -= callback;
            }
        }

        public void Trigger(string eventName, params object[] args)
        {
            if (_events.TryGetValue(eventName, out var @event))
            {
                @event.Invoke(args);
            }
        }
    }
}