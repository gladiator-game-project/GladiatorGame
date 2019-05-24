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
        private Entity _player;
        private List<Entity> _enemyList;
        private bool BattleGoing;

        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
            _enemyList = new List<Entity>();

            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                SpawnEnemy(new Vector3(0, 0, 20));

            BattleGoing = true;
        }

        void Update()
        {
            if (BattleGoing)
                CheckGameStatus();
        }

        private void CheckGameStatus()
        {
            //We died
            if (_player.Alive == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            //Remove every NPC who is dead
            _enemyList.RemoveAll(enemy => enemy.GetComponent<Entity>().Alive == false);

            if (_enemyList.Count == 0)
            {
                //Win screen
                BattleGoing = false;
                StartCoroutine(_player.EndOfBattle());
            }
        }

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
