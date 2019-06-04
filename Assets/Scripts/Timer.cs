using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Timer
    {
        public Dictionary<string, float> timers;

        public Timer()
        {
            timers = new Dictionary<string, float>();
        }

        public void Update()
        {
            var newDict = new Dictionary<string, float>();
            foreach (var timer in timers)
                newDict.Add(timer.Key, timer.Value - Time.deltaTime);
            timers = newDict;
        }

        public void AddTimer(string name, float delay)
        {
            if (timers.ContainsKey(name))
            {
                timers[name] = delay;
                return;
            }

            timers.Add(name, delay);
        }

        public bool CheckTimer(string name) =>
            timers.ContainsKey(name) == false || timers[name] <= 0;
    }
}
