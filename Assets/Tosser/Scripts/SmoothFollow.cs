using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tosser.Controls
{
    public class SmoothFollow : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] private Vector3 target_Offset;
        [SerializeField] private Vector3 HeightLockVector;
        [SerializeField] private bool notReady = true;
        IEnumerator Start()
        {
            yield return new WaitForSeconds(0.3f);
            target = GameObject.FindGameObjectWithTag("Player").transform;
            transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
            //  target_Offset = transform.position - target.position;
            yield return new WaitForSeconds(0.3f);
            notReady = false;
        }

        void LateUpdate()
        {
            if (notReady)
            {
                return;
            }
            HeightLockVector = new Vector3(target.position.x + target_Offset.x, transform.position.y, target.position.z + target_Offset.z);
            transform.position = Vector3.Lerp(transform.position, HeightLockVector, 0.1f);
        }
    }
}