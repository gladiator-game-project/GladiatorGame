using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevPlayerMovement : MonoBehaviour
{
    public int MovementSpeed = 6;
    public int CameraSpeed = 10;
    public int MaxCameraPitch = 90;

    public GameObject Camera; // TODO Change to Head object
    private float _pitch;

    private Rigidbody _rigidBody;
    private Entity _entity;

    private bool _holdMouseDown = false;
    public enum Direction { Right, UpRight, Up, UpLeft, Left, Down, Center };
    private Vector2 _mousePosCenter;

    public GameObject Circle;

    
    void Start()
    {
        _mousePosCenter = new Vector2(Screen.width / 2, (Screen.height / 2) + 20); // +20 because unity is 20 off with mouse pos
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

    private void UpdateAttack() // update attack function, which checks for attacks and what direction
    {

        if (Input.GetKeyDown(KeyCode.Mouse0)) // if mouse button is pressed
        {
            _holdMouseDown = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            Circle.transform.position = _mousePosCenter;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && _holdMouseDown == true && _entity.LoseStamina(10))// normal attack cost 10 stamina
        {
            _holdMouseDown = false;
            Vector2 MousePos = Input.mousePosition;
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log(WhichDirection(MousePos, _mousePosCenter));
            ChangeCirclePosition(WhichDirection(MousePos, _mousePosCenter));
            _entity.Attack();
            //We could change the attack functions to set the number of stamina in there of how much stamina it costs
        }
    }

    private void ChangeCirclePosition(Direction direction)
    {
        if (direction == Direction.Down)
            Circle.transform.position = new Vector2(Circle.transform.position.x,Circle.transform.position.y - 200);
        else if (direction == Direction.Left)
            Circle.transform.position = new Vector2(Circle.transform.position.x - 200, Circle.transform.position.y);
        else if (direction == Direction.UpLeft)
            Circle.transform.position = new Vector2(Circle.transform.position.x - 200, Circle.transform.position.y + 200);
        else if (direction == Direction.Up)
            Circle.transform.position = new Vector2(Circle.transform.position.x, Circle.transform.position.y + 200);
        else if (direction == Direction.UpRight)
            Circle.transform.position = new Vector2(Circle.transform.position.x + 200, Circle.transform.position.y + 200);
        else if (direction == Direction.Right)
            Circle.transform.position = new Vector2(Circle.transform.position.x + 200, Circle.transform.position.y);
    }

    private Direction WhichDirection(Vector2 New_Pos, Vector2 MousePosCenter) // Function  to check direction
    {
        float diffx = New_Pos.x - MousePosCenter.x; // check distance between mouse x and center
        float diffy = New_Pos.y - MousePosCenter.y; // check distance between mouse y and center

        if (diffx > 50 && diffy < 50 && diffy > -50)
            return Direction.Right;                   //                                         ----------------
        else if (diffx > 50 && diffy > 50) //                                                    | UL | U  | UR |
            return Direction.UpRight;               //                                           |----+----+-----
        else if (diffx < 50 && diffx > -50 && diffy > 50) //                                     | L  | C  | R  |
            return Direction.Up;                    //                                           |----+----+-----
        else if (diffx < -50 && diffy > 50) //                                                   |              |
            return Direction.UpLeft;                //                                           |      D       |
        else if (diffx < -50 && diffy < 50 && diffy > -50) //                                    ----------------
            return Direction.Left;
        else if (diffy < -50)
            return Direction.Down;
        else
            return Direction.Center;
    }
}
