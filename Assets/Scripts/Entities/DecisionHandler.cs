using Assets.Scripts.Behaviour.Fuzzy_Logic;
using Assets.Scripts.Entities.Player;
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