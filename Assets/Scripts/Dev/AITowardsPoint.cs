using Assets.Scripts.Entities;
using UnityEngine;

namespace Assets.Scripts.Dev
{
    public class AITowardsPoint : MonoBehaviour
    {
        public Movement NPCMovement;

        void Update()
        {
            transform.position = NPCMovement.TowardsPosition;
        }
    }
}
