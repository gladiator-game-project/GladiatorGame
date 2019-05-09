using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    [SerializeField] public float maxStamina;

    [SerializeField] public BaseWeapon weapon;
    private Animator _animator;
    private float health;
    private float stamina;
    public bool _hasShield;
    public bool Alive;

    public void Attack(PlayerMovement.Direction direction)
    {
        switch (direction)
        {
            case PlayerMovement.Direction.Left:
                weapon.AttackLeft();
                break;
            case PlayerMovement.Direction.Right:
                weapon.AttackRight();
                break;
            default:
                weapon.AttackDefault();
                break;
        }
    }

    public void LowerDefense()
    {
        if (_hasShield)
        {
            _animator.SetBool("HoldShield", false);
        }
        else
        {
            _animator.SetBool("HoldSword", false);
        }
    }

    public void RaiseDefense()
    {
        if (_hasShield)
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

    public bool LoseStamina(float stamina)
    {
        if (Stamina - stamina > 0)
        {
            Stamina -= stamina;
            return true;
        } else
        {
            return false;
        }
    }

    /// <summary>
    /// Keeps the value between the max and above 0.
    /// </summary>
    /// <param name="value">Value to normalize.</param>
    /// <param name="max">Stop increasing at the max.</param>
    /// <returns></returns>
    private float Normalize(float value, float max)
    {
        if (value >= max) return max;
        else if (value <= 0) return 0;
        else return value;
    }

    public float Health
    {
        get { return health; }
        set { health = Normalize(value, maxHealth); }
    }

    public float Stamina
    {
        get { return stamina; }
        set { stamina = Normalize(value, maxStamina); }
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
        Stamina = maxStamina;
        Alive = true;
        InvokeRepeating("RegainStamina", 2.0f, 2.0f); // repeat function
        _animator = GetComponent<Animator>();
    }


    private void RegainStamina()
    {
        Stamina += 20;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDeath();
    }

    private void UpdateDeath()
    {
        if (health <= 0)
        {
            Alive = false;
        }
    }

}
