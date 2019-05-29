using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class BattleManager : MonoBehaviour
    {
        private GateManager _gateManager;
        private Entity _player;

        private List<Entity> _enemyList;        
        private bool _battleGoing;


        void Start()
        {
            _gateManager = GetComponent<GateManager>();
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
            _enemyList = new List<Entity>();

            StartBattle();
        }

        void Update()
        {
            if (_battleGoing)
                CheckGameStatus();
        }

        /// <summary>
        /// Start a new battle
        /// </summary>
        public void StartBattle()
        {
            //We can not start a new battle if there is one already
            if (_battleGoing)
                return;

            SpawnEnemy(new Vector3(0, 0, 20));
            _battleGoing = true;
        }

        /// <summary>
        /// Check the status of the ongoing battle. 
        /// </summary>
        private void CheckGameStatus()
        {
            //Is the player Dead? end game
            if (_player.Alive == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _battleGoing = false;
            }

            //Remove every NPC who is dead
            _enemyList.RemoveAll(enemy => enemy.GetComponent<Entity>().Alive == false);

            //No more enemies? end game
            if (_enemyList.Count == 0)
            {
                //Win screen
                _battleGoing = false;
                _gateManager.SetGates(true, false);
                StartCoroutine(_player.EndOfBattle());
            }
        }

        /// <summary>
        /// Spawns an enemy at the given location
        /// </summary>
        /// <param name="SpawnLocation"></param>
        private void SpawnEnemy(Vector3 SpawnLocation)
        {
            GameObject enemy = (GameObject)Instantiate(Resources.Load("Prefabs/NPC"));
            enemy.transform.position = SpawnLocation;
            enemy.GetComponent<Movement>().Target = _player.gameObject;
            enemy.GetComponent<BehaviorExecutor>().SetBehaviorParam("Target", _player);
            _enemyList.Add(enemy.GetComponent<Entity>());
        }
    }
}
