using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    [SerializeField] public int maxStamina;

    [SerializeField] private BaseWeapon weapon;
    public GameObject Hand;
    private int health;
    private int stamina;
    public bool Alive;

    public void Attack(PlayerMovement.Direction direction)
    {
        weapon.Attack(direction);
    }

    private int normalize(int value, int max)
    {
        if (value >= max) return max;
        else if (value <= 0) return 0;
        else return value;
    }

    public int Health
    {
        get { return health; }
        set { health = normalize(value, maxHealth); }
    }

    public int Stamina
    {
        get { return stamina; }
        set { stamina = normalize(value, maxStamina); }
    }


    public bool LoseStamina(int staminaLoss) // this method can be called on when the player does an action and needs to lose stamina
    {
        if ((stamina - staminaLoss) >= 0) // if new stamina would be bigger than 0, then its okay.
        {
            stamina = stamina - staminaLoss; // then change the stamina
            return true;
        }
        else // if new stamina would be lower than 0 not allowed
        {
            return false;
        }
    }

    public void LoseHealth(int healthLoss)
    {
        health = health - healthLoss; // Changes the current health
        Debug.Log(gameObject.name + "now has " + health);
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
        Stamina = maxStamina;
        Alive = true;
        InvokeRepeating("RegainStamina", 2.0f, 2.0f); // repeat function
    }


    private void RegainStamina()
    {
        if ((stamina + 10) > maxStamina) // make sure it doesnt exceed maximum
        {
            stamina = maxStamina; // set stamina
        }
        else
        {
            stamina = stamina + 20; // set new stamina
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWeapon();
        UpdateDeath();
    }

    private void UpdateDeath()
    {
        if (health <= 0)
        {
            Alive = false;
        }
    }

    private void UpdateWeapon()
    {
        if (weapon == null)
        {
            GameObject knucklesPrefab = (GameObject) Instantiate(Resources.Load("Prefabs/Weapons/knuckles"));
            knucklesPrefab.transform.parent = Hand.transform;
            weapon = knucklesPrefab.GetComponent<BaseWeapon>();
        }
    }

}
