using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Tosser.PlayerCore;
using Tosser.Generics;

namespace Tosser.AI
{
    public class BotAI : MonoBehaviour
    {
        [SerializeField] private CharacterStats enemyStats;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private StateController stateController;

        public enum CharacterState
        {
            Idle,
            Stunned,
            Carrying,
            Dragging,
            Carried,
            Flying
        }

        public BotAI otherBot;
        public PlayerManager playerManager;
        public CharacterState characterState;
        public GameObject targetPosition;
        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            stateController = GetComponent<StateController>();
        }

        public void SetDragState(bool value)
        {
            if (value)
                characterState = CharacterState.Carried;
        }

        void SetupAI()
        {
            if (playerManager == null)
            {
                GameObject[] otherAI = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < otherAI.Length; i++)
                {
                    if (otherAI[i] != gameObject)
                        otherAI[i].GetComponent<BotAI>().otherBot = this;
                }
            }
            stateController.SetupAI(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 12)
                other.GetComponent<Collectible>().PickUp(false);
        }

        private void OnEnable()
        {
            Invoke(nameof(SetupAI), 1);
        }
        /// 1. Look for collectible
        /// 2. When we find object, see if we are closer to it than our ally
        /// 3. If not, we throw friend there
    }
}
