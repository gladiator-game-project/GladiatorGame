using Assets.Scripts.Behaviour.Fuzzy_Logic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class DecisionHandler : MonoBehaviour
    {
        //TODO private Entity _entity;

        private Fuzzy_Sets _sets;
        private Fuzzy_Rules _rules;


        [Range(0, 100)] public float Health;
        [Range(0, 100)] public float Courage;
        [Range(0, 180)] public float Angle;


        public void Start()
        {
            //TODO _entity = GetComponent<Entity>();

            _sets = new Fuzzy_Sets();
            _rules = new Fuzzy_Rules();

            Decide();
        }

        public void Decide()
        {
            //1 calculate from the Sets
            var setValues = _sets.CalculateValues(new float[] { Health, Courage, Angle });

            //2 Throw into the Rules
           Debug.Log(_rules.GetPreferredAction(setValues));
        }
    }

    public enum MovementBehaviour
    {
        IDLE,
        CIRLCE_AROUND,
        WALK_BACKWARDS,
        FLEE
    }
}