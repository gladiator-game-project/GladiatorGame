using Assets.Scripts.Entities.Player;
using Assets.Scripts.Weapon;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class AnimationHandler : MonoBehaviour
    {
        public bool HasShield;

        private Animator _animator;

        public void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Update()
        {
        }

        public void SetIdle(BaseWeapon.AttackType type)
        {
            switch (type)
            {
                case BaseWeapon.AttackType.PUNCH:
                    _animator.SetFloat("IdleStance", 0);
                    break;
                case BaseWeapon.AttackType.STAB:
                    _animator.SetFloat("IdleStance", 1);
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
        /// Set sprinting
        /// </summary>
        /// <param name="movementAmp"></param>
        public void SetSprinting(float movementAmp) =>
            _animator.SetFloat("MovementAmp", movementAmp);


        /// <summary>
        /// Set the correct animation based on the direction
        /// </summary>
        /// <param name="attackType"></param>
        /// <param name="direction"></param>
        public void Attack(BaseWeapon.AttackType attackType, PlayerMovement.Direction direction)
        {
            if (attackType == BaseWeapon.AttackType.SLASH)
                _animator.SetTrigger("Slash" + direction);
            else
                _animator.SetTrigger(attackType.ToString());
        }


        /// <summary>
        /// Raise the Defence
        /// </summary>
        public void RaiseDefense()
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
        /// Lower the Defence
        /// </summary>
        public void LowerDefense() =>
            _animator.SetBool(HasShield ? "HoldShield" : "HoldSword", false);

        public void SetEnabled(bool b) =>
            _animator.enabled = b;
    }
}