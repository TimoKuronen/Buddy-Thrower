using UnityEngine;

namespace Tosser.Core
{
    public class DragCharacter : MonoBehaviour
    {
        [SerializeField] private CharacterStats characterStats;
        [SerializeField] private GameObject throwTargetVisual;
        [SerializeField] private Transform buddyPosition;

        private Vector3 startPos;
        private Vector3 endPos;
        private RaycastHit hit;
        private ThrowMethod throwMethod;

        private void Awake()
        {
            throwMethod = GetComponent<ThrowMethod>();
        }

        public void DragEvent(DragCharacter draggableCharacter)
        {
            startPos = draggableCharacter.transform.position;
            endPos = buddyPosition.position;

            float dragDuration = GetDragDuration(startPos, endPos);
            float dragSpeed = GetDragSpeed(startPos, endPos);
            Debug.Log(dragSpeed);
           // draggableCharacter.throwMethod.StartCoroutine(draggableCharacter.throwMethod.Throwing(endPos, startPos, 3, dragDuration));
            draggableCharacter.throwMethod.StartCoroutine(draggableCharacter.throwMethod.Throwing(endPos, startPos, 3, dragSpeed));
        }

        void DrawTargetVisual(bool value)
        {
            if (throwTargetVisual == null)
                return;

            throwTargetVisual.SetActive(value);
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

        public void ThrowEvent(DragCharacter throwableCharacter)
        {

        }

        private void FixedUpdate()
        {
            if (throwTargetVisual == null)
                return;

            if (throwTargetVisual.activeInHierarchy)
            {
                Vector3 rayPoint = transform.forward * characterStats.throwDistance + (Vector3.up * 10);
                if (Physics.Raycast(rayPoint, Vector3.down, out hit, 20))
                {
                    throwTargetVisual.transform.position = hit.point;
                }
            }
        }

        void StopDrag()
        {
            DrawTargetVisual(true);
        }
    }
}