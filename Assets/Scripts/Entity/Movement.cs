using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 TowardsPosition;
    public GameObject Target;

    public float Speed = 5.5f;
    private int _rotationSpeed = 35;
    private Animator _animator;

    private int xAngleCutOff = 15;

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
        if (angle > xAngleCutOff && angle < 65)
            return;

        var newPos = Vector3.MoveTowards(transform.position, TowardsPosition, Time.deltaTime * Speed);
        transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
    }

    private void AnimateModel()
    {
        int x = angle > 65 && angle < 105 ? 1 : 0;
        int y = angle < xAngleCutOff ? 1 : angle > 175 ? -1 : 0;

        //Check whether the x position of TowardsPosition is left or right of the AI
        var i = transform.InverseTransformPoint(TowardsPosition);
        x *= i.x < 0 ? -1 : 1;
                
        if (Vector3.Distance(transform.position, TowardsPosition) < 5.2f)
            y = 0;

        _animator.SetInteger("inputx", x);
        _animator.SetInteger("inputy", y);
    }

    private void RotateModel()
    {
        var newDir = Vector3.RotateTowards(transform.forward, Target.transform.position - transform.position, Time.deltaTime * _rotationSpeed * Mathf.Deg2Rad, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private Vector3 GetAnglePoint(int degrees)
    {
        var offsetDegrees = (transform.rotation.y + degrees) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(offsetDegrees), 0, Mathf.Cos(offsetDegrees)) * 3;
    }
}
