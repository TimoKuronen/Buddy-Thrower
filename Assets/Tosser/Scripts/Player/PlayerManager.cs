using UnityEngine;
using Tosser.AI;
using Tosser.Generics;
using Tosser.Core;

namespace Tosser.PlayerCore
{
    public class PlayerManager : MonoBehaviour, IDamageHandler
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
        public static PlayerManager Instance;
        public BotAI allyBot;
        DragCharacter dragCharacter;
        public CharacterState characterState;
        Character character;

        public void SetupPlayer()
        {
           // allyBot.playerManager = this;
        }

        public void SetDragState(bool value)
        {
            if (value)
                characterState = CharacterState.Carried;
        }

        public void DragInputGiven()
        {
            dragCharacter.DragEvent(true, allyBot.transform, character);
            characterState = CharacterState.Dragging;
        }

        public void DragInputStopped()
        {
            dragCharacter.DragEvent(false, allyBot.transform, character);
            characterState = CharacterState.Idle;
        }

        private void OnEnable()
        {
            Instance = this;
            Invoke(nameof(SetupPlayer), 1);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 12)
                other.GetComponent<Collectible>().PickUp(true);
        }

        public void TakeDamage(int amount, Transform origin)
        {
            throw new System.NotImplementedException();
        }
    }
}
