using Assets.Scripts.Behaviour.Fuzzy_Logic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class DecisionHandler : MonoBehaviour
    {
        public string CurrentMovementAction;
        public bool UseInternalValues;

        public Fuzzy_Sets Sets { get; private set; }

        [Range(0, 100)] public float Health;
        [Range(0, 100)] public float Courage;
        [Range(0, 180)] public float Angle;

        private Entity _entity;
        private Fuzzy_Rules _rules;


        public void Start()
        {
            _entity = GetComponent<Entity>();

            Sets = new Fuzzy_Sets();
            _rules = new Fuzzy_Rules();

            GetDecision();
        }

        public void Update()
        {
            CurrentMovementAction = GetDecision();
        }

        public string GetDecision()
        {
            float[] values = UseInternalValues ?
                            new float[] { Health, Courage, Angle } : new float[] { _entity.Health, _entity.Courage, 0 }; //TODO use angle

            //1 calculate from the Sets            
            var setValues = GetSetValues(values);

            //2 Throw into the Rules and get a decision
            return _rules.GetPreferredAction(setValues);
        }

        public Dictionary<string, float> GetSetValues(float[] values) =>
            Sets.CalculateValues(values);
    }

    public enum MovementBehaviour
    {
        IDLE,
        CIRLCE_AROUND,
        WALK_BACKWARDS,
        FLEE
    }
}