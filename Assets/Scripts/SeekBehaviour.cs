using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehaviour : MonoBehaviour
{
    public Vector3 Target;
    public GameObject TargetObject; // u can either set an object to follow, or a position
    public float Mass = 15; // mass of the entity, we could/should set this in the entity class later
    public float MaxVelocity = 3; 
    public float MaxForce = 15;

    private Vector3 Velocity; // current velocity

    // Start is called before the first frame update
    void Start()
    {
        Velocity = Vector3.zero; // velocity starts zero
        Target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetObject != null)
            Target = TargetObject.transform.position; // if there is no object specified to go to, set a position to go to, which is standard itself

        Vector3 DesiredVelocity = Target - transform.position; // calculate desired velocity
        DesiredVelocity = DesiredVelocity.normalized * MaxVelocity; // normalize it
        Vector3 Steering = DesiredVelocity - Velocity; // calculate steering
        Steering = Vector3.ClampMagnitude(Steering, MaxForce); // truncate
        Steering = Steering / Mass; // divide by mass
        Velocity = Vector3.ClampMagnitude(Velocity + Steering, MaxVelocity); // truncate velocity
        transform.position += Velocity * Time.deltaTime; // set new position
    }
}
