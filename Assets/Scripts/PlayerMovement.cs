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

    private void UpdateAttack() // update attack function, which checks for attacks and what direction
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0)) // if mouse button is pressed
        {
            _holdMouseDown = true; // then mouse is being hold
            Cursor.lockState = CursorLockMode.None; // mouse should be moved freely
            Cursor.visible = false; // but not visible to the player
        }
            
        if (Input.GetKeyUp(KeyCode.Mouse0) && _holdMouseDown == true && _entity.LoseStamina(10)) // if mouse button let go, normal attack cost 10 stamina
        {
            _holdMouseDown = false; // then mouse is not holding down anymore
            Vector2 MousePos = Input.mousePosition; // check the new pos for direction
            Cursor.lockState = CursorLockMode.Locked; // set mouse locked agin
            Vector2 MousePosCenter = new Vector2(Screen.width / 2 , (Screen.height / 2) + 20);
            Debug.Log(Direction(MousePos, MousePosCenter)); // put in console direction
            _entity.Attack(); // attack
            //We could change the attack functions to set the number of stamina in there of how much stamina it costs
        }
    }

    private string Direction(Vector2 New_Pos, Vector2 MousePosCenter) // Functio  to check direction
    {
        float diffx = New_Pos.x - MousePosCenter.x; // check distance x
        float diffy = New_Pos.y - MousePosCenter.y; // check distance y

        if (diffx > 50 && diffy < 50 && diffy > -50) // right
            return "Right";                   //                                                 ----------------
        else if (diffx > 50 && diffy > 50) //upright                                             | UL | U  | UR |
            return "UpRight";               //                                                   |----+----+-----
        else if (diffx < 50 && diffx > -50 && diffy > 50) // up                                  | L  | C  | R  |
            return "Up";                    //                                                   |----+----+-----
        else if (diffx < -50 && diffy > 50) // upleft                                            |              |
            return "UpLeft";                //                                                   |      D       |
        else if (diffx < -50 && diffy < 50 && diffy > -50) // left                               ----------------
            return "Left";
        else if (diffy < -50) // down
            return "Down";
        else // center
            return "Center";
    }
}
