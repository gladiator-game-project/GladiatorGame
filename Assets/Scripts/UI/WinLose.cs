using System.Linq;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class WinLose : MonoBehaviour
    {
        private GameObject _player;
        private Entities.Entity _entityscript;
        private PlayerMovement _movementscript;
        private TakeWeapon _takeWeaponscript;
        public GameObject YouWinText;
        private Text _youWinLose;
        public GameObject RestartButton;

        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _entityscript = _player.GetComponent<Entities.Entity>();
            _youWinLose = YouWinText.GetComponent<Text>();
            _movementscript = _player.GetComponent<PlayerMovement>();
            _takeWeaponscript = _player.GetComponent<TakeWeapon>();
        }
    
        void Update()
        {
            CheckGameStatus();
        }

        private void CheckGameStatus()
        {
            if (_entityscript.Alive == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _youWinLose.text = "Game Over";
                _movementscript.enabled = false;
                _takeWeaponscript.enabled = false;
                RestartButton.SetActive(true);
            }

            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            bool allDeath = enemies.All(enemy => enemy.GetComponent<Entities.Entity>().Alive != true); // assume all are death

            if (allDeath)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _youWinLose.text = "You win!";
                _movementscript.enabled = false;
                _takeWeaponscript.enabled = false;
                RestartButton.SetActive(true);
            }
        }
    }
}
