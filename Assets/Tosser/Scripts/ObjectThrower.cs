using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectThrower : MonoBehaviour
{
    [SerializeField] private float dragSpeed = 18;
    [SerializeField] private float distanceToTarget;
    CharacterController charControl;
    NavMeshAgent navMeshAgent;
    ThrowMethod throwMethod;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        charControl = GetComponent<CharacterController>();
        throwMethod = GetComponent<ThrowMethod>();
    }

    private void Start()
    {
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit myNavHit, 5, -1))
        {
            navMeshAgent.Warp(myNavHit.position);
        }
    }

    public void StartThrowEvent(Vector3 targetPosition)
    {
        navMeshAgent.enabled = false;
        charControl.enabled = false;
        if (gameObject.activeInHierarchy)
            ThrowMethod.Instance.StartCoroutine(ThrowMethod.Throwing(targetPosition, dragSpeed, false, 1, true, true, transform));
        else Debug.Log("fish wasn't active when it was supposed to jump to boat");
    }

    public void ResetObject()
    {
        navMeshAgent.enabled = true;
        charControl.enabled = true;
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit myNavHit, 5, -1))
        {
            navMeshAgent.Warp(myNavHit.position);
        }
    }
}
