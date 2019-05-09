using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITowardsPoint : MonoBehaviour
{
    public Movement NPCMovement;
    void Start()
    {
    }

    void Update()
    {
        transform.position = NPCMovement.TowardsPosition;
    }
}
