using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
	[SerializeField] private int maxHealth;
	[SerializeField] private int maxStamina;

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

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
        Stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Make stamina regain over time.
    }
}
