using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tosser.Core
{
    public class TossMechanic : MonoBehaviour
    {
        public GameObject throwAssistantObject;
        [SerializeField] private CharacterStats characterStats;
        [SerializeField] private Vector3 throwTargetPosition;
        [SerializeField] private Vector3 offset;
        [SerializeField] private Transform tosserObject;
        [SerializeField] private bool aiming;
        [SerializeField] private float tossDistance;

        private void Update()
        {
            if (aiming)
            {
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

        public void TossActivated()
        {

        }

        public void TossAimStopped()
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