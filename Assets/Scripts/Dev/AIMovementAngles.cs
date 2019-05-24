using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Dev
{
    public class AIMovementAngles : MonoBehaviour
    {
        private Movement _movement;

        void Start()
        {
            _movement = GetComponent<Movement>();
        }

        void Update()
        {
            var fromPos = _movement.TowardsPosition - transform.position;
            Debug.DrawRay(transform.position, fromPos, Color.black);

            //Forward lines
            DrawRay(15, Color.red);
            DrawRay(-15, Color.red);

            //left strafe lines
            DrawRay(65, Color.blue);
            DrawRay(105, Color.blue);

            //Right strafe lines
            DrawRay(-65, Color.cyan);
            DrawRay(-105, Color.cyan);

            DrawCircle();
        }

        /// <summary>
        /// Draw a circle around the transform
        /// </summary>
        private void DrawCircle()
        {
            for (int i = 0; i < 360; i++)
                Debug.DrawLine(transform.position + GetPoint(i) * 5, transform.position + GetPoint(i + 1) * 5, Color.green);
        }
               
        /// <summary>
        /// Draw a ray from the local transform to a certain degree
        /// </summary>
        /// <param name="degrees"></param>
        /// <param name="color"></param>
        private void DrawRay(float degrees, Color color)
        {            
            var point = GetPoint(degrees); //Get the Unit Circle point
            Debug.DrawRay(transform.position, point * 4, color);
        }
        
        /// <summary>
        /// Get a point in the Unit Circle with the given degree
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        private Vector3 GetPoint(float degree)
        {
            //negative transform for the correct rotation
            var cos = Mathf.Cos((-transform.localEulerAngles.y + degree + 90) * Mathf.Deg2Rad);
            var sin = Mathf.Sin((-transform.localEulerAngles.y + degree + 90) * Mathf.Deg2Rad);
            return new Vector3(cos, 0.1f, sin);
        }

    }
}