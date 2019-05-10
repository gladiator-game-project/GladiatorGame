using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    [SerializeField] public float maxStamina;

    [SerializeField] public BaseWeapon weapon;
    [SerializeField] private GameObject Hand;
    private float health;
    private float stamina;
    private float courage;
    public bool Alive = true;

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

    public float Courage
    {
        get => courage + health - 100;
    }


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

    public void Attack(PlayerMovement.Direction direction)
    {
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
