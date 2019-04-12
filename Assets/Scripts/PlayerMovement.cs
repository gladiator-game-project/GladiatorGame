using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int MovementSpeed = 6;
    public int CameraSpeed = 10;
    public int MaxCameraPitch = 90;

    public GameObject Camera; // TODO Change to Head object
    private float _pitch;

    private Rigidbody _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        UpdateKeyMovement();
        UpdateCameraMovement();
    }

    private void UpdateKeyMovement()
    {
        var xInput = transform.right * Input.GetAxis("Horizontal");
        var zInput = transform.forward * Input.GetAxis("Vertical");

        Vector3 movementDirection = (xInput + zInput) * MovementSpeed;

        _rigidBody.velocity = movementDirection;
    }

    private void UpdateCameraMovement()
    {
        _pitch += Input.GetAxis("Mouse Y") * CameraSpeed;
        var yInput = Input.GetAxis("Mouse X");

        var playerRotation = new Vector3(0, yInput * -CameraSpeed, 0);

        //Player rotation
        transform.eulerAngles = transform.eulerAngles - playerRotation;


        _pitch = Mathf.Clamp(_pitch, -MaxCameraPitch, MaxCameraPitch);
        Camera.transform.localEulerAngles = new Vector3(-_pitch, 0, 0);
    }
}
