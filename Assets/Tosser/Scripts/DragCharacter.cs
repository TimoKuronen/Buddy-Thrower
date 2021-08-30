using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tosser.Core
{
    public class DragCharacter : MonoBehaviour
    {
        [SerializeField] private float dragSpeed;
        [SerializeField] private Transform dragPosition;
        [SerializeField] private CharacterStats characterStats;
        public void DragEvent(bool value, Transform targetTransform, Character character)
        {
            dragPosition = targetTransform;

            if (character.botAI != null)
                character.botAI.SetDragState(value);
            else character.playerManager.SetDragState(value);
        }

        private void FixedUpdate()
        {
            
        }
    }
}