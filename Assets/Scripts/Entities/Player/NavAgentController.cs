using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Entities.Player
{
    public class NavAgentController : MonoBehaviour
    {
        private NavMeshAgent _agent;

        public void Start()
        {
            _agent = gameObject.AddComponent<NavMeshAgent>();
            _agent.SetDestination(new Vector3(-1, 0, -43));
        }

        public void Update()
        {

        }
    }
}
