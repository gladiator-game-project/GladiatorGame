
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Behaviour.Fuzzy_Logic
{
    public class Fuzzy_Rules
    {
        private RulesWalking _walkingRules;

        public Fuzzy_Rules()
        {
            _walkingRules = JsonHandler<RulesWalking>.GetFromJson("RulesWalking.json");
        }

        public string GetPreferredAction(Dictionary<string, float> d)
        {
            float highestScore = -1;
            string bestAction = "default";

            foreach (var r in _walkingRules.WalkingRules)
            {
                d.TryGetValue("Health_" + r.Health, out float one);
                d.TryGetValue("Courage_" + r.Courage, out float two);
                d.TryGetValue("Angle_" + r.Angle, out float three);

                var score = Mathf.Min(one, two, three);

                if (score > highestScore)
                {
                    highestScore = score;
                    bestAction = r.Action;
                }
            }
            return bestAction;
        }
    }

    [Serializable]
    public partial class RulesWalking
    {
        public List<WalkingRule> WalkingRules;
    }

    [Serializable]
    public partial class WalkingRule
    {
        public string Rule;
        public string Health;
        public string Courage;
        public string Angle;
        public string Action;
    }
}
