using UnityEngine;
using Tosser.PlayerCore;

namespace Tosser.Core
{
    public class DragCharacter : MonoBehaviour
    {
        [SerializeField] private Transform buddyPosition;

        public void DragEvent(DragCharacter draggableCharacter)
        {
            Vector3 startPos = draggableCharacter.transform.position;
            Vector3 endPos = buddyPosition.position;

            float dragDuration = GetDragDuration(startPos, endPos);
            float dragSpeed = GetDragSpeed(startPos, endPos);

            ThrowMethod.Instance.StartCoroutine(ThrowMethod.Throwing(endPos, startPos, 3, dragSpeed, draggableCharacter.transform, this));
            PlayerManager.Instance.ChangeCharacterState(PlayerManager.CharacterState.Carrying);     
        }

        public void DragCompleted(DragCharacter draggableCharacter)
        {
            draggableCharacter.transform.parent = buddyPosition;
        }

        float GetDragSpeed(Vector3 startPos, Vector3 endPos)
        {
            float dist = Vector3.Distance(startPos, endPos);
            return Mathf.Clamp(dist, 10, 30);
        }

        float GetDragDuration(Vector3 startPos, Vector3 endPos)
        {
            float dist = Vector3.Distance(startPos, endPos);
            return Mathf.Clamp(dist / 10, 0.2f, 2);
        }
    }
}