using UnityEngine;
using Tosser.PlayerCore;

namespace Tosser.Core
{
    public class TossMechanic : MonoBehaviour
    {
        [SerializeField] private CharacterStats characterStats;

        [SerializeField] private GameObject throwAssistantPrefab;
        [SerializeField] private Vector3 throwTargetPosition;
        [SerializeField] private Vector3 offset;
        [SerializeField] private Transform tosserObject;
        [SerializeField] private bool aiming;
        [SerializeField] private float tossDistance;

        private GameObject throwAssistantObject;
        private RaycastHit hit;

        private void Awake()
        {
            throwAssistantObject = Instantiate(throwAssistantPrefab, transform.position, transform.rotation, transform);
            HideThrowTarget();
        }

        private void Update()
        {
            if (aiming)
            {
                Vector3 rayPoint = transform.forward * characterStats.throwDistance + (Vector3.up * 10);
                if (Physics.Raycast(rayPoint, Vector3.down, out hit, 20))
                {
                    throwAssistantObject.transform.position = hit.point;
                }

                throwTargetPosition = tosserObject.forward * tossDistance;
                DrawThrowTarget(throwTargetPosition);
            }
        }

        public void TossAimStarted(Transform tosser, float distance)
        {
            tosserObject = tosser;
            tossDistance = distance;
            aiming = true;   
        }

        public void ThrowEvent(DragCharacter throwableCharacter)
        {
            //Vector3 startPos = draggableCharacter.transform.position;
            //Vector3 endPos = buddyPosition.position;

            //float dragDuration = GetDragDuration(startPos, endPos);
            //float dragSpeed = GetDragSpeed(startPos, endPos);

            //ThrowMethod.Instance.StartCoroutine(ThrowMethod.Throwing(endPos, startPos, 3, dragSpeed, draggableCharacter.transform));
            PlayerManager.Instance.ChangeCharacterState(PlayerManager.CharacterState.Idle);
            TossAimStopped();
        }

        void TossAimStopped()
        {
            aiming = false;
        }

        public void DrawThrowTarget(Vector3 pos)
        {
            if (!throwAssistantObject.activeInHierarchy)
                throwAssistantObject.SetActive(true);
            throwAssistantObject.transform.position = pos + offset;
        }

        public void HideThrowTarget()
        {
            if (throwAssistantObject.activeInHierarchy)
                throwAssistantObject.SetActive(false);
        }
    }
}