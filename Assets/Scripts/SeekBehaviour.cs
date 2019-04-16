using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehaviour : MonoBehaviour
{
    public Vector3 Target;
    public GameObject TargetObject; // You can either set an object to follow, or a position to follow
    public float _mass = 15; // mass of the entity, we could/should set this in the entity class later
    public float _maxVelocity = 3;  // just like this one
    public float _maxForce = 15; // just like this one

    private Vector3 _velocity; // current velocity

    // Start is called before the first frame update
    void Start()
    {
        _velocity = Vector3.zero; // velocity starts zero
        Target = transform.position; // set current position
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetObject != null)
            Target = TargetObject.transform.position; // if there is no object specified to go towards, set a position to go to, which is standard itself if not specified

        Vector3 DesiredVelocity = Target - transform.position; // calculate desired velocity
        DesiredVelocity = DesiredVelocity.normalized * _maxVelocity; // normalize it

        Vector3 Steering = DesiredVelocity - _velocity; // calculate steering
        Steering = Vector3.ClampMagnitude(Steering, _maxForce); // truncate
        Steering = Steering / _mass; // divide by mass

        _velocity = Vector3.ClampMagnitude(_velocity + Steering, _maxVelocity); // truncate velocity

        transform.position += _velocity * Time.deltaTime; // set new position
    }
}
