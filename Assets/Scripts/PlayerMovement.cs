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
    private Entity _entity;

    private bool _holdMouseDown = false;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _entity = GetComponent<Entity>();
    }

    

    void Update()
    {
        UpdateKeyMovement();
        UpdateCameraMovement();
        UpdateAttack();
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

    private void UpdateAttack()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _holdMouseDown = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
            
        if (Input.GetKeyUp(KeyCode.Mouse0) && _holdMouseDown == true && _entity.LoseStamina(20))
        {
            _holdMouseDown = false;
            Vector2 MousePos = Input.mousePosition;
            Cursor.lockState = CursorLockMode.Locked;
            Vector2 MousePosCenter = new Vector2(Screen.width / 2 , (Screen.height / 2) + 20);
            Cursor.visible = false;
            Debug.Log(Direction(MousePos, MousePosCenter));
            _entity.Attack();
        }
    }

    private string Direction(Vector2 New_Pos, Vector2 MousePosCenter)
    {
        float diffx = New_Pos.x - MousePosCenter.x;
        float diffy = New_Pos.y - MousePosCenter.y;

        if (diffx > 50 && diffy < 50 && diffy > -50)
            return "Right";
        else if (diffx > 50 && diffy > 50)
            return "UpRight";
        else if (diffx < 50 && diffx > -50 && diffy > 50)
            return "Up";
        else if (diffx < -50 && diffy > 50)
            return "UpLeft";
        else if (diffx < -50 && diffy < 50 && diffy > -50)
            return "Left";
        else if (diffy < -50)
            return "Down";
        else
            return "Center";
    }
}
