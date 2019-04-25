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

    private Animator _animator;

    private Rigidbody _rigidBody;
    private Entity _entity;

    private bool _holdMouseDown = false;
    private bool _holdSecondMouseDown = false;
    public enum Direction { Right, UpRight, Up, UpLeft, Left, Down, Center};

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _entity = GetComponent<Entity>();
        _animator = GetComponent<Animator>();
    }

    

    void Update()
    {
        UpdateKeyMovement();
        UpdateCameraMovement();
        UpdateAttack();
        UpdateShield();
    }

    private void UpdateKeyMovement()
    {
        var xInput = transform.right * Input.GetAxis("Horizontal");
        var zInput = transform.forward * Input.GetAxis("Vertical");

        Vector3 movementDirection = (xInput + zInput) * MovementSpeed;

        _animator.SetInteger("inputx", (int)Input.GetAxis("Horizontal"));
        _animator.SetInteger("inputy", (int)Input.GetAxis("Vertical"));
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
        }
            
        if (Input.GetKeyUp(KeyCode.Mouse0) && _holdMouseDown == true && _entity.LoseStamina(10))// normal attack cost 10 stamina
        {
            _holdMouseDown = false;
            Vector2 MousePos = Input.mousePosition;
            Cursor.lockState = CursorLockMode.Locked;
            Vector2 MousePosCenter = new Vector2(Screen.width / 2 , (Screen.height / 2) + 20); // +20 because unity is 20 off with mouse pos
            Debug.Log(WhichDirection4(MousePos, MousePosCenter));
            Direction direction = WhichDirection4(MousePos, MousePosCenter);
            _entity.Attack(direction);
            //We could change the attack functions to set the number of stamina in there of how much stamina it costs
        }
    }
    private void UpdateShield() // update attack function, which checks for attacks and what direction
    {
        if (Input.GetMouseButtonDown(1)) // if mouse button is pressed
        {
            _holdSecondMouseDown = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }

        if (_holdSecondMouseDown)
        {
            Vector2 MousePos = Input.mousePosition;
            Vector2 MousePosCenter = new Vector2(Screen.width / 2, (Screen.height / 2) + 20); // +20 because unity is 20 off with mouse pos
            Debug.Log("Shield " + WhichDirection4(MousePos, MousePosCenter));
            //TODO: shield animation, etc.
        }

        if (Input.GetMouseButtonUp(1) && _holdSecondMouseDown == true)
        {
            _holdSecondMouseDown = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private Direction WhichDirection6(Vector2 New_Pos, Vector2 MousePosCenter) // Function  to check direction
    {
        float radius = 50; // the radius of the center
        float diffx = New_Pos.x - MousePosCenter.x; // check distance between mouse x and center
        float diffy = New_Pos.y - MousePosCenter.y; // check distance between mouse y and center

        if (diffx > radius && diffy < radius && diffy > -radius)
            return Direction.Right;                   //                                         ----------------
        else if (diffx > radius && diffy > radius) //                                            | UL | U  | UR |
            return Direction.UpRight;               //                                           |----+----+-----
        else if (diffx < radius && diffx > -radius && diffy > radius) //                         | L  | C  | R  |
            return Direction.Up;                    //                                           |----+----+-----
        else if (diffx < -radius && diffy > radius) //                                                   |              |
            return Direction.UpLeft;                //                                           |      D       |
        else if (diffx < -radius && diffy < radius && diffy > -radius) //                                    ----------------
            return Direction.Left;
        else if (diffy < -radius)
            return Direction.Down;
        else
            return Direction.Center;
    }

    private Direction WhichDirection4(Vector2 New_Pos, Vector2 MousePosCenter) // Function  to check direction
    {
        float radius = 50; // the radius of the center
        float diffx = New_Pos.x - MousePosCenter.x; // check distance between mouse x and center
        float diffy = New_Pos.y - MousePosCenter.y; // check distance between mouse y and center

        //      -------------
        //      | \   U   / |
        //      |--\-----/--
        //      | L|  C  |R |
        //      |--/-+---\+--
        //      | /   D   \ |
        //      -------------

        float diffxPercentage = diffx / (Screen.width / 2);         // percentages compared to screensize
        float diffyPercentage = diffy / ((Screen.height / 2) + 20);
        
        if (diffx > radius && diffy < radius && diffy > -radius)
            return Direction.Right;
        else if (diffx > radius && diffy > radius && diffxPercentage > diffyPercentage)
            return Direction.Right;
        else if (diffx > radius && diffy > radius && diffyPercentage > diffxPercentage)
            return Direction.Up;
        else if (diffx < radius && diffx > -radius && diffy > radius)
            return Direction.Up;
        else if (diffx < -radius && diffy > radius && diffyPercentage > diffxPercentage * -1)
            return Direction.Up;
        else if (diffx < -radius && diffy > radius && diffxPercentage * -1 > diffyPercentage)
            return Direction.Left;
        else if (diffx < -radius && diffy < radius && diffy > -radius)
            return Direction.Left;
        else if (diffx < -radius && diffy < -radius && diffxPercentage < diffyPercentage)
            return Direction.Left;
        else if (diffx < -radius && diffy < -radius && diffyPercentage < diffxPercentage)
            return Direction.Down;
        else if (diffx < radius && diffx > -radius && diffy < -radius)
            return Direction.Down;
        else if (diffx > radius && diffy < -radius && diffyPercentage * -1 > diffxPercentage)
            return Direction.Down;
        else if (diffx > radius && diffy < -radius && diffxPercentage > diffyPercentage * -1)
            return Direction.Right;
        else
            return Direction.Center;
    }
}
