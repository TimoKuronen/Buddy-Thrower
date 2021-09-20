using UnityEngine;
using Tosser.AI;
using Tosser.Generics;
using Tosser.Core;
using Tosser.Controls;
using System.Collections;

namespace Tosser.PlayerCore
{
    public class PlayerManager : MonoBehaviour, IDamageHandler, IDragCharacter
    {
        public enum CharacterState
        {
            Idle,
            Stunned,
            Carrying,
            Dragging,
            Carried,
            Flying
        }

        public CharacterState characterState;

        public static PlayerManager Instance;
        [SerializeField] private CharacterStats characterStats;
        public bool ThrowCoolDownOn
        {
            get { return throwCoolDownOn; }
            private set { throwCoolDownOn = value; }
        }
        private bool throwCoolDownOn;
        private BotAI allyBot;
        private DragCharacter allyDrag;
        private DragCharacter dragCharacter;
        private TossMechanic tossMechanic;

        void Awake()
        {
            dragCharacter = GetComponent<DragCharacter>();
        }

        public void ChangeCharacterState(CharacterState state)
        {
            characterState = state;
        }

        void DragInput()
        {
            if (characterState == CharacterState.Idle)
            {
                if (ThrowCoolDownOn)
                {
                    Debug.Log("cooldown on!");
                    return;
                }
                dragCharacter.DragEvent(allyDrag);
            }
        }

        public IEnumerator ThrowCoolDown()
        {
            ThrowCoolDownOn = true;
            yield return new WaitForSeconds(characterStats.dragCoolDown);
            ThrowCoolDownOn = false;
        }

        public void SetAlly(BotAI botAI, DragCharacter dragCharacter)
        {
            allyBot = botAI;
            allyDrag = dragCharacter;
        }

        void ThrowInput()
        {
            if (characterState != CharacterState.Carrying)
                return;
            tossMechanic.ThrowEvent(allyDrag);
            StartCoroutine(ThrowCoolDown());
        }

        public void TakeDamage(int amount, Transform origin)
        {
            throw new System.NotImplementedException();
        }

        public void DragStopped()
        {
            ChangeCharacterState(CharacterState.Idle);
        }

        public void DragCharacter(DragCharacter draggingCharacter)
        {
            characterState = CharacterState.Carried;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 12)
                other.GetComponent<Collectible>().PickUp(true);
        }

        private void OnEnable()
        {
            Instance = this;
            PlayerInput.instance.dragEvent += DragInput;
            PlayerInput.instance.throwEvent += ThrowInput;
        }

        void OnDisable()
        {
            PlayerInput.instance.dragEvent -= DragInput;
            PlayerInput.instance.throwEvent -= ThrowInput;
        }
    }
}
