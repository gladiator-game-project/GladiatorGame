using UnityEngine;

namespace Assets.Scripts.GameManagers
{
    public class GateManager : MonoBehaviour
    {
        public GameObject Player;
        public Animator Gate_Outer;
        public Animator Gate_Inner;

        private BattleManager _battleManager;
        private bool _gateInnerOpen;
        private bool _canToggle;

        public void Start()
        {
            _battleManager = GetComponent<BattleManager>();
            SetGates(false, true);
        }

        public void Update()
        {
            if (Player.transform.position.z > -56 && Player.transform.position.z < -44)
            {
                if (_canToggle)
                {
                    ToggleGates();
                    _canToggle = false;
                }
            }
            else if (_canToggle == false)
                _canToggle = true;
        }

        public void ToggleGates()
        {
            if (_gateInnerOpen == false)            
                _battleManager.StartBattle();            

            SetGates(!_gateInnerOpen, _gateInnerOpen);
        }

        public void SetGates(bool innerOpen, bool outerOpen)
        {
            _gateInnerOpen = innerOpen;
            Gate_Inner.SetBool("open", innerOpen);
            Gate_Outer.SetBool("open", outerOpen);
        }

    }
}