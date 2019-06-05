using UnityEngine;

namespace Assets.Scripts.Entities.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public int MovementSpeed = 6;
        public int CameraSpeed = 10;
        public int MaxCameraPitch = 90;

        public float Pitch;
        public GameObject Camera; // TODO Change to Head object

        private float _seconds;
        private readonly float _fireDelay = 1.0f; // Seconds to wait
        private float _fireTimestamp;
        private float _cooldown = 3;

        private Rigidbody _rigidBody;
        private Entity _entity;
        private AnimationHandler _animHandler;

        private bool _holdMouseDown;
        private bool _holdSecondMouseDown;
        public enum Direction { RIGHT, UP_RIGHT, UP, UP_LEFT, LEFT, DOWN, CENTER };
        private Vector2 _mousePosCenter;

        public GameObject SwordIndication;
        public GameObject ShieldIndication;
        public bool DebugMode = false;

        private bool _lShiftDown;
        // Speed amplifier when the shift key is down.
        public float MovementAmp = 1.6f;

        void Start()
        {
            _mousePosCenter = new Vector2(Screen.width / 2f, (Screen.height / 2f) + 20); // +20 because unity is 20 off with mouse pos
            _animHandler = GetComponent<AnimationHandler>();
            _rigidBody = GetComponent<Rigidbody>();
            _entity = GetComponent<Entity>();
            _fireTimestamp = Time.realtimeSinceStartup + _fireDelay;
            Cursor.lockState = CursorLockMode.Locked;
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
                _lShiftDown = true;
            else if (Input.GetKeyUp(KeyCode.LeftShift))
                _lShiftDown = false;

            const float sprintStamReq = 0.3f; // Stamina requirement for sprinting.

            // If left shift is down increase movement speed and anim speed.
            if (_lShiftDown && _entity.Stamina - sprintStamReq > 0f)
            {
                movementSpeed *= MovementAmp;
                _animHandler.SetAnimationSpeed(MovementAmp);
                _entity.Stamina -= sprintStamReq;
            }
            else
                _animHandler.SetAnimationSpeed(1.0f);

            return movementSpeed;
        }

        private void UpdateKeyMovement()
        {
            //if attacking, disable movement
            if (_animHandler.IsAnimationRunning("attack"))
            {
                _rigidBody.velocity = new Vector3();
                return;
            }

            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            var xInput = transform.right * horizontal;
            var zInput = transform.forward * vertical;

            Vector3 movementDirection = (xInput + zInput) * HandleSprinting();

            _animHandler.SetMovement(horizontal, vertical);
            _rigidBody.velocity = movementDirection;
        }

        private void UpdateCameraMovement()
        {
            Pitch += Input.GetAxis("Mouse Y") * CameraSpeed;
            var yInput = Input.GetAxis("Mouse X");

            var playerRotation = new Vector3(0, yInput * -CameraSpeed, 0);

            //Player rotation
            transform.eulerAngles = transform.eulerAngles - playerRotation;


            Pitch = Mathf.Clamp(Pitch, -MaxCameraPitch, MaxCameraPitch);
            Camera.transform.localEulerAngles = new Vector3(-Pitch, Camera.transform.localEulerAngles.y, Camera.transform.localEulerAngles.z);
        }

        private void UpdateAttack() // update attack function, which checks for attacks and what direction
        {
            if (_animHandler.IsAnimationRunning("attack"))
                return;

            if (Input.GetMouseButtonDown(0)) // if the left mouse button is pressed
            {
                _holdMouseDown = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
                if (DebugMode)
                    SwordIndication.transform.position = _mousePosCenter;
            }

            if (Input.GetMouseButtonUp(0) && _holdMouseDown && _entity.LoseStamina(10))// normal attack cost 10 stamina
            {
                Cursor.lockState = CursorLockMode.Locked;

                _holdMouseDown = false;
                Vector2 mousePos = Input.mousePosition;
                var mousePosCenter = new Vector2(Screen.width / 2f, (Screen.height / 2f) + 20); // +20 because unity is 20 off with mouse pos
                var direction = WhichDirection4(mousePos, mousePosCenter);

                _entity.Attack(direction);

                if (DebugMode)
                    ChangeCirclePosition(WhichDirection6(mousePos, _mousePosCenter), SwordIndication, 0f);
                //We could change the attack functions to set the number of stamina in there of how much stamina it costs
            }
        }

        private void ChangeCirclePosition(Direction direction, GameObject swordOrShield, float offside)
        {
            if (direction == Direction.DOWN)
                swordOrShield.transform.position = new Vector2(_mousePosCenter.x, _mousePosCenter.y - 200 - offside);
            else if (direction == Direction.LEFT)
                swordOrShield.transform.position = new Vector2(_mousePosCenter.x - 200, _mousePosCenter.y - offside);
            else if (direction == Direction.UP_LEFT)
                swordOrShield.transform.position = new Vector2(_mousePosCenter.x - 200, _mousePosCenter.y + 200 - offside);
            else if (direction == Direction.UP)
                swordOrShield.transform.position = new Vector2(_mousePosCenter.x, _mousePosCenter.y + 200 - offside);
            else if (direction == Direction.UP_RIGHT)
                swordOrShield.transform.position = new Vector2(_mousePosCenter.x + 200, _mousePosCenter.y + 200 - offside);
            else if (direction == Direction.RIGHT)
                swordOrShield.transform.position = new Vector2(_mousePosCenter.x + 200, _mousePosCenter.y - offside);
        }

        private void UpdateShield() // update attack function, which checks for attacks and what direction
        {
            //check if mouse was raised previous frame
            if (_holdSecondMouseDown == false)
            {
                _entity.LowerDefense();
                _cooldown -= Time.deltaTime;
                if (_cooldown <= 0f)
                    _seconds = 0f;
            }

            if (Input.GetMouseButtonDown(1)) // if mouse button is pressed
            {
                if (_entity.LoseStamina(_seconds * 2f))
                    _entity.RaiseDefense();
                else
                    return;
                _cooldown = 3f;
                _holdSecondMouseDown = true;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;


                if (DebugMode)
                    ShieldIndication.transform.position = new Vector2(_mousePosCenter.x, _mousePosCenter.y - 40);
            }

            if (_holdSecondMouseDown) //if mouse still down
            {
                if (!_entity.UsingStamina.Contains("blocking"))
                    _entity.UsingStamina.Add("blocking");
                Vector2 mousePos = Input.mousePosition;
                var mousePosCenter = new Vector2(Screen.width / 2f, (Screen.height / 2f) + 20); // +20 because unity is 20 off with mouse pos

                if (DebugMode)
                    ChangeCirclePosition(WhichDirection4(mousePos, _mousePosCenter), ShieldIndication, 40f);

                // If absolute time is later than the timestamp, we know 1 seconds have passed
                if (Time.realtimeSinceStartup > _fireTimestamp)
                {
                    _fireTimestamp = Time.realtimeSinceStartup + _fireDelay;

                    const float factor = 2f; //energy consumption

                    _seconds++;
                    float blockStaminaLoss = _seconds * factor;
                    if (!_entity.LoseStamina((int)blockStaminaLoss))
                    {
                        EndShield();
                        return;
                    }

                }
            }

            if (Input.GetMouseButtonUp(1) && _holdSecondMouseDown)//if mouse not down
            {
                EndShield();
            }
        }

        private void EndShield()
        {
            _entity.LowerDefense();
            _entity.UsingStamina.Remove("blocking");
            _holdSecondMouseDown = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (DebugMode)
                ShieldIndication.transform.position = new Vector2(_mousePosCenter.x, _mousePosCenter.y - 40);
        }

        private static Direction WhichDirection6(Vector2 newPos, Vector2 mousePosCenter) // Function  to check direction
        {
            const float radius = 40; // the radius of the center
            float diffx = newPos.x - mousePosCenter.x; // check distance between mouse x and center
            float diffy = newPos.y - mousePosCenter.y; // check distance between mouse y and center

            if (diffx > radius && diffy < radius && diffy > -radius)
                return Direction.RIGHT;                   //                                         ----------------
            if (diffx > radius && diffy > radius) //                                            | UL | U  | UR |
                return Direction.UP_RIGHT;               //                                           |----+----+-----
            if (diffx < radius && diffx > -radius && diffy > radius) //                         | L  | C  | R  |
                return Direction.UP;                    //                                           |----+----+-----
            if (diffx < -radius && diffy > radius) //                                                   |              |
                return Direction.UP_LEFT;                //                                           |      D       |
            if (diffx < -radius && diffy < radius && diffy > -radius) //                                    ----------------
                return Direction.LEFT;
            if (diffy < -radius)
                return Direction.DOWN;

            return Direction.CENTER;
        }

        private static Direction WhichDirection4(Vector2 newPos, Vector2 mousePosCenter) // Function  to check direction
        {
            float radius = 40; // the radius of the center
            float diffx = newPos.x - mousePosCenter.x; // check distance between mouse x and center
            float diffy = newPos.y - mousePosCenter.y; // check distance between mouse y and center

            //      -------------
            //      | \   U   / |
            //      |--\-----/--
            //      | L|  C  |R |
            //      |--/-+---\+--
            //      | /   D   \ |
            //      -------------

            float diffxPercentage = diffx / (Screen.width / 2f);         // percentages compared to screensize
            float diffyPercentage = diffy / ((Screen.height / 2f) + 20);

            if (diffx > radius && diffy < radius && diffy > -radius)
                return Direction.RIGHT;
            if (diffx > radius && diffy > radius && diffxPercentage > diffyPercentage)
                return Direction.RIGHT;
            if (diffx > radius && diffy > radius && diffyPercentage > diffxPercentage)
                return Direction.UP;
            if (diffx < radius && diffx > -radius && diffy > radius)
                return Direction.UP;
            if (diffx < -radius && diffy > radius && diffyPercentage > diffxPercentage * -1)
                return Direction.UP;
            if (diffx < -radius && diffy > radius && diffxPercentage * -1 > diffyPercentage)
                return Direction.LEFT;
            if (diffx < -radius && diffy < radius && diffy > -radius)
                return Direction.LEFT;
            if (diffx < -radius && diffy < -radius && diffxPercentage < diffyPercentage)
                return Direction.LEFT;
            if (diffx < -radius && diffy < -radius && diffyPercentage < diffxPercentage)
                return Direction.DOWN;
            if (diffx < radius && diffx > -radius && diffy < -radius)
                return Direction.DOWN;
            if (diffx > radius && diffy < -radius && diffyPercentage * -1 > diffxPercentage)
                return Direction.DOWN;
            if (diffx > radius && diffy < -radius && diffxPercentage > diffyPercentage * -1)
                return Direction.RIGHT;

            return Direction.CENTER;
        }
    }
}
