﻿using Assets.Scripts.Entities.Player;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class AnimationHandler : MonoBehaviour
    {
        public bool HasShield;

        private Animator _animator;
        private ArmorHandler _armorHandler;

        public void Start()
        {
            _animator = GetComponent<Animator>();
            _armorHandler = GetComponent<ArmorHandler>();
        }

        public void Update()
        {
            HasShield = _armorHandler.HasShield;
        }

        public void SetIdle(BaseWeapon.AttackType type)
        {
            switch (type)
            {
                case BaseWeapon.AttackType.PUNCH:
                    if(HasShield)
                        _animator.SetFloat("IdleStance", 3);
                    else
                        _animator.SetFloat("IdleStance", 0);
                    break;
                case BaseWeapon.AttackType.STAB:
                    _animator.SetFloat("IdleStance", 1);
                    break;
                case BaseWeapon.AttackType.SLASH:
                    _animator.SetFloat("IdleStance", 2);
                    break;
                default:
                    _animator.SetFloat("IdleStance", 0);
                    break;
            }
        }

        public bool IsAnimationRunning(string animTag) =>
            _animator.GetCurrentAnimatorStateInfo(0).IsTag(animTag);

        /// <summary>
        /// Set the movement animation
        /// </summary>
        /// <param name="horizontal"></param>
        /// <param name="vertical"></param>
        public void SetMovement(float horizontal, float vertical)
        {
            _animator.SetInteger("inputx", (int)horizontal);
            _animator.SetInteger("inputy", (int)vertical);
        }

        /// <summary>
        /// Set animation speed
        /// </summary>
        /// <param name="movementAmp"></param>
        public void SetAnimationSpeed(float speed) =>
            _animator.SetFloat("MovementAmp", speed);


        /// <summary>
        /// Set the correct animation based on the direction
        /// </summary>
        /// <param name="attackType"></param>
        /// <param name="direction"></param>
        public void Attack(BaseWeapon.AttackType attackType, Direction direction)
        {
            if (attackType == BaseWeapon.AttackType.SLASH && (direction == Direction.LEFT || direction == Direction.RIGHT))
                _animator.SetTrigger("SLASH" + direction);
            else
                _animator.SetTrigger(attackType.ToString());
        }


        /// <summary>
        /// Raise the Defence
        /// </summary>
        public void RaiseDefence()
        {
            if (HasShield)
            {
                _animator.SetTrigger("RaiseShield");
                _animator.SetBool("HoldShield", true);
            }
            else
            {
                _animator.SetTrigger("RaiseSword");
                _animator.SetBool("HoldSword", true);
            }
        }

        /// <summary>
        /// Raises defence and lowers it after the given timeLength
        /// </summary>
        /// <param name="timeLength"></param>
        public void RaiseDefence(float timeLength)
        {
            RaiseDefence();
            StartCoroutine("LowerDefence", timeLength);
        }

        /// <summary>
        /// Lower the Defence
        /// </summary>
        public void LowerDefence() =>
            _animator.SetBool(HasShield ? "HoldShield" : "HoldSword", false);

        public void SetEnabled(bool b) =>
            _animator.enabled = b;

        public void StartVictory() =>
            _animator.SetTrigger("victory");
    }
}