using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Events
{
    public class EventManager : MonoBehaviour
    {
        private static EventManager instance;
        private Dictionary<Type, EventHandle> eventDictionary;
        public static EventManager Instance
        {
            get
            {
                if (instance == null)
                {
                    // Tenta encontrar uma instância existente na cena
                    instance = FindObjectOfType<EventManager>();

                    // Caso não haja, cria um novo GameObject com o EventManager
                    if (instance == null)
                    {
                        GameObject singletonObject = new GameObject("EventManager");
                        instance = singletonObject.AddComponent<EventManager>();
                    }
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }


        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            
        }

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
            T handle = new T();
            if (!eventDictionary.ContainsKey(typeof(T)))
            {
                eventDictionary.Add(typeof(T), handle);
            }
      
        }

    }
}


