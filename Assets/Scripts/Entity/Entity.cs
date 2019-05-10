using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    [SerializeField] public float maxStamina;

    [SerializeField] public BaseWeapon weapon;
    private Animator _animator;

    [SerializeField] private GameObject Hand;
    private float health;
    private float stamina;
    public bool _hasShield;
    private float courage;
    public bool Alive = true;

    public void Start()
    {
        Health = maxHealth;
        Stamina = maxStamina;
        courage = 100;
        InvokeRepeating("RegainStamina", 2.0f, 2.0f); // repeat function
    }

    public void Update()
    {
        UpdateDeath();
        UpdateWeapon();
    }

    public float Health
    {
        get { return health; }
        set { health = Mathf.Clamp(value, 0, maxHealth); }
    }

    public float Stamina
    {
        get { return stamina; }
        set { stamina = Mathf.Clamp(value, 0, maxStamina); }
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

    public float Courage
    {
        get => courage + health - 100;
    }

    public void Attack(PlayerMovement.Direction direction)
    {
        Health = maxHealth;
        Stamina = maxStamina;
        Alive = true;
        InvokeRepeating("RegainStamina", 2.0f, 2.0f); // repeat function
        _animator = GetComponent<Animator>();
        weapon.Attack(direction);
    }

    public bool LoseStamina(float stamina)
    {
        Stamina -= Stamina - stamina > 0 ? stamina : 0;
        return Stamina - stamina > 0;
    }

    private void RegainStamina()
    {
        Stamina += 20;
    }

    private void UpdateDeath()
    {
        if (health <= 0)
            Alive = false;
    }

    private void UpdateWeapon()
    {
        if (weapon == null)
        {
            GameObject knucklesPrefab = (GameObject)Instantiate(Resources.Load("Prefabs/Weapons/knuckles"));
            knucklesPrefab.transform.parent = Hand.transform;
            weapon = knucklesPrefab.GetComponent<BaseWeapon>();
            weapon.currentType = BaseWeapon.AttackType.Punch;
        }
    }

}
