using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventHandler { }

public static class EventManager
{
    private static List<IEventHandler> eventHandlers = new List<IEventHandler>();

    public static void AddHandler(IEventHandler eventHandler)
    {
        if (eventHandler == null)
            return;

        eventHandlers.Add(eventHandler);
    }

    public static void RemoveHandler(IEventHandler eventHandler)
    {
        eventHandlers.Remove(eventHandler);
    }

    public static void Call<T>(Action<T> action) where T : IEventHandler
    {
        if (action == null)
            return;

        for(int i = 0; i < eventHandlers.Count; i++)
        {
            var eventHandler = eventHandlers[i];
            if(eventHandler is T)
            {
                action?.Invoke((T)eventHandler);
            }
        }
    }
}
