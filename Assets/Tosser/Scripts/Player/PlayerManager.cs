using UnityEngine;
using Tosser.AI;
using Tosser.Generics;
using Tosser.Core;
using Tosser.Controls;

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

        private BotAI allyBot;
        private DragCharacter allyDrag;
        private DragCharacter dragCharacter;

        void Awake()
        {
            dragCharacter = GetComponent<DragCharacter>();
        }

        void DragInput()
        {
            if (characterState == CharacterState.Idle)
            {
                dragCharacter.DragEvent(allyDrag);
            }
            else
            {

            }
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
        }

        public void TakeDamage(int amount, Transform origin)
        {
            throw new System.NotImplementedException();
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

        public void DragStopped()
        {
            throw new System.NotImplementedException();
        }

        public void DragCharacter(DragCharacter draggingCharacter)
        {
            characterState = CharacterState.Carried;
        }
    }
}
