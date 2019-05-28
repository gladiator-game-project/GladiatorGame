using System;
using System.Collections;
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
        private PlayerMovement _playerMovement;
        private NavMeshAgent _agent;
        private Camera _mainCamera;
        private bool _checkforDest;

        public void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _agent = GetComponent<NavMeshAgent>();
            _mainCamera = Camera.main;
            _agent.enabled = false;
        }

        public void Update()
        {
            //No destination? return
            if (_checkforDest == false)
                return;

            var dist = Vector3.Distance(transform.position, _agent.destination);

            //Distance to target > 1? keep moving
            if (dist > 1)
                return;

            //Restore control to the enity
            _agent.enabled = false;
            _checkforDest = false;
            _playerMovement.enabled = true;
            _playerMovement.Pitch = 0;
        }

        public void SetDestination(Vector3 pos)
        {
            var rotation = new Vector3(0, 90, -3);
            StartCoroutine(LerpFromTo(rotation, 1f));

            _playerMovement.enabled = false;
            _agent.enabled = true;
            _agent.SetDestination(pos);
            _checkforDest = true;
        }

        public IEnumerator LerpFromTo(Vector3 rotation, float duration)
        {
            for (var t = 0f; t < duration; t += Time.deltaTime)
            {
                _mainCamera.transform.localRotation = Quaternion.Slerp(_mainCamera.transform.localRotation, Quaternion.Euler(rotation), t / duration);
                yield return 0;
            }
        }
    }
}
