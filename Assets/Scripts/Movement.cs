using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 TowardsPosition;
    private float _speed = 2.5f;

    void Start()
    {
        TowardsPosition = transform.position;
    }

    void Update()
    {
        var newPos = Vector3.MoveTowards(transform.position, TowardsPosition, Time.deltaTime * _speed);
        transform.position = new Vector3(newPos.x, 1, newPos.z);
    }
}
