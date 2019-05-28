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
        private Camera _mainCamera;
        private NavMeshAgent _agent;
        private bool _checkforDest;

        public void Start()
        {
            _mainCamera = Camera.main;
            _agent = GetComponent<NavMeshAgent>();
            _agent.enabled = false;
        }

        public void Update()
        {
            if (_checkforDest)
            {
                var dist = Vector3.Distance(transform.position, _agent.destination);

                if (dist < 2)
                    _agent.enabled = false;
            }
        }

        public void SetDestination(Vector3 pos)
        {
            StartCoroutine(LerpFromTo(1f));

            _agent.enabled = true;
            _agent.SetDestination(pos);
            _checkforDest = true;

        }

        public IEnumerator LerpFromTo(float duration)
        {
            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                // Smoothly rotate towards the target point.
                _mainCamera.transform.localEulerAngles = Vector3.Slerp(_mainCamera.transform.localEulerAngles, new Vector3(0, 90, -3), t / duration);
                yield return 0;
            }
        }
    }
}
