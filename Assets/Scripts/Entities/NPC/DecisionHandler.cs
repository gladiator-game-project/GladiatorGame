using Assets.Scripts.Behaviour.Fuzzy_Logic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class DecisionHandler : MonoBehaviour
    {
        public string CurrentMovementAction;
        public string CurrentInteractionAction;
        public bool UseInternalValues;

        public Fuzzy_Sets Sets { get; private set; }

        [Range(0, 100)] public float Health;
        [Range(0, 100)] public float Courage;
        [Range(0, 180)] public float Angle;

        private Movement _movement;
        private Entity _entity;
        private AnimationHandler _animHandler;

        private Fuzzy_Rules _rules;
        private Timer _timer;

        private int _circlingCounter;

        public void Start()
        {
            _movement = GetComponent<Movement>();
            _entity = GetComponent<Entity>();
            _animHandler = GetComponent<AnimationHandler>();

            Sets = new Fuzzy_Sets();
            _rules = new Fuzzy_Rules();

            _timer = new Timer();

            GetDecision();
        }

        public void Update()
        {
            if (_timer.CheckTimer("updater"))
            {
                CurrentMovementAction = GetDecision();
                CurrentInteractionAction = GetInteractionDecision();
                _timer.AddTimer("updater", 0.1f);
            }

            _timer.Update();
        }

        public string GetInteractionDecision()
        {
            //Am I attacked? Defend
            if (_timer.CheckTimer("Defend"))
            {
                if (_movement.Target.GetComponent<AnimationHandler>().IsAnimationRunning("attack"))
                {
                    _timer.AddTimer("Defend", 0.3f);
                    return "DEFEND";
                }
            }

            //Do I have enough stamina to attack
            if (_timer.CheckTimer("Attack"))
            {
                _timer.AddTimer("Attack", 1f);
                return "ATTACK";
            }

            return "IDLE";
        }

        /// <summary>
        /// Gets a movement action based on Health, Courage and Angle
        /// </summary>
        /// <returns>Movement Action string</returns>
        public string GetDecision()
        {
            //Get the angle between the Target and the NPC
            float angle = Vector3.Angle(transform.position - _movement.Target.transform.position, _movement.Target.transform.forward);

            float[] values = UseInternalValues ?
                            new float[] { Health, Courage, Angle } : new float[] { _entity.Health, _entity.Courage, angle };

            //1 calculate from the Sets            
            var setValues = GetSetValues(values);

            //2 Throw into the Rules and get a decision
            var preferredAction = _rules.GetPreferredAction(setValues);

            //Make sure the AI does not circle constantly
            if (preferredAction == "CIRCLE_AROUND")
            {
                _circlingCounter++;

                if (_circlingCounter == Random.Range(18, 25))
                    _timer.AddTimer("Circling", 1.5f);

                if (_timer.CheckTimer("Circling") == false)
                    preferredAction = "IDLE";
            }
            else
                _circlingCounter = 0;
                       
            return preferredAction;
        }

        public Dictionary<string, float> GetSetValues(float[] values) =>
            Sets.CalculateValues(values);
    }
}