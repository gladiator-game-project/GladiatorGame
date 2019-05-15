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

            //Forward
            DrawRay(15, Color.red);
            DrawRay(-15, Color.red);

            //left strafe
            DrawRay(65, Color.blue);
            DrawRay(105, Color.blue);

            //Right strafe
            DrawRay(-65, Color.cyan);
            DrawRay(-105, Color.cyan);

            DrawCircle();
        }

        private void DrawCircle()
        {
            for (int i = 0; i < 360; i++)
                Debug.DrawLine(transform.position + GetPoint(i) * 6, transform.position + GetPoint(i + 1) * 6, Color.green);
        }

        private Vector3 GetPoint(int i)
        {
            var cos = Mathf.Cos((-transform.localEulerAngles.y + i + 90) * Mathf.Deg2Rad);
            var sin = Mathf.Sin((-transform.localEulerAngles.y + i + 90) * Mathf.Deg2Rad);
            return new Vector3(cos, 0.1f, sin);
        }

        private void DrawRay(float degrees, Color color)
        {
            var cos = Mathf.Cos((-transform.localEulerAngles.y + degrees + 90) * Mathf.Deg2Rad);
            var sin = Mathf.Sin((-transform.localEulerAngles.y + degrees + 90) * Mathf.Deg2Rad);
            Debug.DrawRay(transform.position, new Vector3(cos, 0.1f, sin) * 4, color);
        }

    }
}