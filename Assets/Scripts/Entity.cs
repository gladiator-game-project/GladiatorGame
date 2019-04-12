using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
	[SerializeField] public int maxHealth;
	[SerializeField] public int maxStamina;

	private int health;
	private int stamina;

	private int normalize(int value, int max) {
		if (value >= max) return max;
		else if (value <= 0) return 0;
		else return value;
	}

	public int Health { 
		get { return health; }
		set { health = normalize(value, maxHealth); }
	}

	public int Stamina { 
		get { return stamina; }
		set { stamina = normalize(value, maxStamina); }
	}

    public bool LoseStamina(int staminaLoss) // this method can be called on when the player does an action and needs to lose stamina
    {
        if ((stamina + staminaLoss) >= 0) // if new stamina would be bigger than 0, then its okay.
        {
            stamina = stamina - staminaLoss;
            return true;
        }
        else // if new stamina would be lower than 0 not allowed
        {
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
        Stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
