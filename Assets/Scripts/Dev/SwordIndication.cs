
using Assets.Scripts.Entities.Player;
using UnityEngine;
using static Assets.Scripts.Entities.Player.PlayerMovement;

namespace Assets.Scripts.Dev
{
    public class SwordIndication : MonoBehaviour
    {
        public PlayerMovement PlayerMovement;
        public GameObject SwordIndicationObject;

        public void Update()
        {
            //For testing the sword direction
            if (SwordIndicationObject != null)
                ChangeCirclePosition(PlayerMovement.MouseDirection);
        }

        /// <summary>
        /// Changes the position of the circle indication
        /// </summary>
        /// <param name="direction"></param>
        private void ChangeCirclePosition(Vector2 direction) =>
            SwordIndicationObject.transform.position = new Vector3(direction.x, 3.18f, direction.y + 5);

    }
}
