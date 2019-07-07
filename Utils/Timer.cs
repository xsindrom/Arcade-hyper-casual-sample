using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TimerData
{
    public string id;
    public float waitTime;
    public float currentTime;
    public float fullTime;
    public Action action;

    public void Invoke()
    {
        action?.Invoke();
    }
}

public class Timer : MonoSingleton<Timer>
{
    [SerializeField]
    private List<TimerData> timers = new List<TimerData>();

    public void AddTimer(string id, float waitTime, Action action)
    {
        var timer = new TimerData()
        {
            id = id,
            waitTime = waitTime,
            currentTime = 0,
            fullTime = 0,
            action = action
        };
        timers.Add(timer);
    }

    public void AddTimer(TimerData timer)
    {
        timers.Add(timer);
    }

    public void RemoveTimer(Predicate<TimerData> predicate)
    {
        if (predicate == null)
            return;

        timers.RemoveAll(predicate);
    }

    private void Update()
    {
        for(int i = 0; i < timers.Count; i++)
        {
            var timer = timers[i];
            timer.currentTime += Time.deltaTime;
            timer.fullTime += Time.deltaTime;
            if(timer.currentTime >= timer.waitTime)
            {
                timer.currentTime = 0;
                timer.Invoke();
            }
            timers[i] = timer;
        }
    }

}
