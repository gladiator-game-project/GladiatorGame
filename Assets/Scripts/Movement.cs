using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 TowardsPosition;
    public GameObject Target;

    private float _speed = 5.5f;
    private int _rotationSpeed = 35;
    private Animator _animator;

    public void Start()
    {
        _animator = GetComponent<Animator>();
        TowardsPosition = transform.position;
    }

    float angle;

    public void Update()
    {
        angle = Vector3.Angle(TowardsPosition - transform.position, transform.forward);

        MoveModel();
        RotateModel();
        AnimateModel();
    }

    private void MoveModel()
    {
        if (angle > 5 && angle < 65)
            return;

        var newPos = Vector3.MoveTowards(transform.position, TowardsPosition, Time.deltaTime * _speed);
        transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
    }

    private void AnimateModel()
    {
        int x = angle > 65 && angle < 105 ? 1 : 0;
        int y = angle < 10 ? 1 : angle > 175 ? -1 : 0;

        x *= TowardsPosition.x < transform.position.x ? -1 : 1;

       // if (Vector3.Distance(transform.position, TowardsPosition) < 3)
      //  {
       //     x = 0;
       //     y = 0;
      //  }

        _animator.SetInteger("inputx", x);
        _animator.SetInteger("inputy", y);
    }

    private void RotateModel()
    {
        var newDir = Vector3.RotateTowards(transform.forward, Target.transform.position - transform.position, Time.deltaTime * _rotationSpeed * Mathf.Deg2Rad, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
