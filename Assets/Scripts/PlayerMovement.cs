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

    float seconds = 0;
    public float fireDelay = 1.0f; // Seconds to wait
    private float fireTimestamp = 0.0f;
    private float cooldown = 3;
    private Animator _animator;

    private Rigidbody _rigidBody;
    private Entity _entity;

    private bool _holdMouseDown = false;
    private bool _holdSecondMouseDown = false;
    public enum Direction { Right, UpRight, Up, UpLeft, Left, Down, Center};
    private Vector2 _mousePosCenter;

    public GameObject SwordIndication;
    public GameObject ShieldIndication;
    public bool DebugMode = false;

    private bool lShiftDown = false;
    // Speed amplifier when the shift key is down.
    public float movementAmp = 1.6f;

    void Start()
    {
        _mousePosCenter = new Vector2(Screen.width / 2, (Screen.height / 2) + 20); // +20 because unity is 20 off with mouse pos
        _rigidBody = GetComponent<Rigidbody>();
        _entity = GetComponent<Entity>();
        fireTimestamp = Time.realtimeSinceStartup + fireDelay;
        _animator = GetComponent<Animator>();
    }

    

    void Update()
    {
        UpdateKeyMovement();
        UpdateCameraMovement();
        UpdateAttack();
        UpdateShield();
    }

    /// <summary>
    /// Checks if the player is holding down the shift key to sprint.
    /// Moreover it will speed up the running animation when the player
    /// is holding down the shift key.
    /// </summary>
    /// <returns>The movement speed if whilst keeping sprinting in account.</returns>
    private float HandleSprinting()
    {
        float movementSpeed = MovementSpeed;

        // Change the boolean of the left shift button being down.
        if (Input.GetKeyDown(KeyCode.LeftShift))
            lShiftDown = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            lShiftDown = false;

        float sprintStamReq = 0.3f; // Stamina requirement for sprinting.
        // If left shift is down increase movement speed and anim speed.
        if (lShiftDown && _entity.Stamina - sprintStamReq > 0f)
        {
            movementSpeed *= movementAmp;
            _animator.SetFloat("MovementAmp", movementAmp);
            _entity.Stamina -= sprintStamReq;
        }
        else _animator.SetFloat("MovementAmp", 1.0f);

        return movementSpeed;
    }

    private void UpdateKeyMovement()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var xInput = transform.right * horizontal;
        var zInput = transform.forward * vertical;

        Vector3 movementDirection = (xInput + zInput) * HandleSprinting();

        _animator.SetInteger("inputx", (int)horizontal);
        _animator.SetInteger("inputy", (int)vertical);
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
            if (DebugMode)
                SwordIndication.transform.position = _mousePosCenter;
        }
            
        if (Input.GetKeyUp(KeyCode.Mouse0) && _holdMouseDown == true && _entity.LoseStamina(10))// normal attack cost 10 stamina
        {
            _holdMouseDown = false;
            Vector2 MousePos = Input.mousePosition;
            Cursor.lockState = CursorLockMode.Locked;
            Vector2 MousePosCenter = new Vector2(Screen.width / 2 , (Screen.height / 2) + 20); // +20 because unity is 20 off with mouse pos
            Direction direction = WhichDirection4(MousePos, MousePosCenter);
            _entity.Attack(direction);
            Debug.Log("Sword " + WhichDirection6(MousePos, _mousePosCenter));
            if (DebugMode)
                ChangeCirclePosition(WhichDirection6(MousePos, _mousePosCenter), SwordIndication, 0f);
            //We could change the attack functions to set the number of stamina in there of how much stamina it costs
        }
    }

    private void ChangeCirclePosition(Direction direction, GameObject Swordorshield, float Offside)
    {
        if (direction == Direction.Down)
            Swordorshield.transform.position = new Vector2(_mousePosCenter.x, _mousePosCenter.y - 200 - Offside);
        else if (direction == Direction.Left)
            Swordorshield.transform.position = new Vector2(_mousePosCenter.x - 200, _mousePosCenter.y - Offside);
        else if (direction == Direction.UpLeft)
            Swordorshield.transform.position = new Vector2(_mousePosCenter.x - 200, _mousePosCenter.y + 200 - Offside);
        else if (direction == Direction.Up)
            Swordorshield.transform.position = new Vector2(_mousePosCenter.x, _mousePosCenter.y + 200 - Offside);
        else if (direction == Direction.UpRight)
            Swordorshield.transform.position = new Vector2(_mousePosCenter.x + 200, _mousePosCenter.y + 200 - Offside);
        else if (direction == Direction.Right)
            Swordorshield.transform.position = new Vector2(_mousePosCenter.x + 200, _mousePosCenter.y - Offside);
    }

    private void UpdateShield() // update attack function, which checks for attacks and what direction
    {
        //check if mouse was raised previous frame
        if (_holdSecondMouseDown == false)
        {
            _entity.LowerDefense();
            cooldown -= Time.deltaTime;
            if (cooldown <= 0f)
                seconds = 0f;
        }

        if (Input.GetMouseButtonDown(1)) // if mouse button is pressed
        {
            cooldown = 3f;
            _holdSecondMouseDown = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            _entity.RaiseDefense();
            if (DebugMode)
                ShieldIndication.transform.position = new Vector2(_mousePosCenter.x, _mousePosCenter.y - 40);
        }

        if (_holdSecondMouseDown) //if mouse still down
        {
            Vector2 MousePos = Input.mousePosition;
            Vector2 MousePosCenter = new Vector2(Screen.width / 2, (Screen.height / 2) + 20); // +20 because unity is 20 off with mouse pos
            Debug.Log("Shield " + WhichDirection4(MousePos, MousePosCenter));
            if(DebugMode)
                ChangeCirclePosition(WhichDirection4(MousePos, _mousePosCenter), ShieldIndication, 40f);

            // If absolute time is later than the timestamp, we know 1 seconds have passed
            if (Time.realtimeSinceStartup > fireTimestamp)
            {
                fireTimestamp = Time.realtimeSinceStartup + fireDelay;
                //energy consumption
                float factor = 2f;
                seconds++;
                float BlockStaminaLoss = seconds * factor;
                Debug.Log(BlockStaminaLoss);
                _entity.LoseStamina((int)BlockStaminaLoss);
            }
        }

        if (Input.GetMouseButtonUp(1) && _holdSecondMouseDown == true)//if mouse not down
        {
            _holdSecondMouseDown = false;
            Cursor.lockState = CursorLockMode.Locked;
            ShieldIndication.transform.position = new Vector2(_mousePosCenter.x, _mousePosCenter.y - 40);
        }
    }

    private Direction WhichDirection6(Vector2 New_Pos, Vector2 MousePosCenter) // Function  to check direction
    {
        float radius = 40; // the radius of the center
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
        float radius = 40; // the radius of the center
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
