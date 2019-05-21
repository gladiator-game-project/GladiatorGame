using System.Linq;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class BattleManager : MonoBehaviour
    {
        private GameObject _player;
        private Entity _entityscript;
        private PlayerMovement _movementscript;
        private WeaponHandler _weaponHandler;
        public GameObject YouWinText;
        private Text _youWinLose;
        public GameObject RestartButton;

        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _entityscript = _player.GetComponent<Entity>();
            _youWinLose = YouWinText.GetComponent<Text>();
            _movementscript = _player.GetComponent<PlayerMovement>();
            _weaponHandler = _player.GetComponent<WeaponHandler>();
        }
    
        void Update()
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                SpawnEnemy(new Vector3(0,0,20));
            CheckGameStatus();
        }

        //Check if player has won or lost yet
        private void CheckGameStatus()
        {
            if (_entityscript.Alive == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _youWinLose.text = "Game Over";
                _movementscript.enabled = false;
                _weaponHandler.enabled = false;
                RestartButton.SetActive(true);
            }

            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            bool allDeath = enemies.All(enemy => enemy.GetComponent<Entity>().Alive == false); // assume all are death

            if (allDeath)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _youWinLose.text = "You win!";
                _movementscript.enabled = false;
                _weaponHandler.enabled = false;
                RestartButton.SetActive(true);
            }
        }

        //This will grab an NPC from the resources folder and places it in game
        private void SpawnEnemy(Vector3 SpawnLocation)
        {
            GameObject Enemy = Instantiate(Resources.Load("Prefabs/NPC", typeof(GameObject))) as GameObject;
            Enemy.transform.position = SpawnLocation;
            Enemy.transform.SetParent(GameObject.Find("Entities").transform);
            Enemy.GetComponent<Movement>().Target = _player;
            Enemy.GetComponent<BehaviorExecutor>().SetBehaviorParam("Target",_player);
        }
    }
}
