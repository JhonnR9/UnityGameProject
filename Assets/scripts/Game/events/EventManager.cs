using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Events
{
    public class EventManager 
    {
        private Dictionary<Type, EventHandle> eventDictionary;

        public T GetEventHandle<T>() where T : EventHandle
        {
            Type eventType = typeof(T);
            if (eventDictionary.TryGetValue(eventType, out EventHandle handle))
            {
                return (T)handle;
            }

            Debug.LogError($"Event of type {eventType} not found.");
            return null;
        }

        public void AddEventHandle<T>() where T : EventHandle, new()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<Type, EventHandle>();
            }

            if (!eventDictionary.ContainsKey(typeof(T)))
            {
                eventDictionary.Add(typeof(T), new T());
            }
      
        }

    }
}


