using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tosser.AI
{
    public class StateController : MonoBehaviour
    {
        public State currentState;
        public State remainState;
        public State previousState;

        NavMeshAgent navMeshAgent;
        public float stateTimeElapsed;
        private bool aiActive;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void SetupAI(bool activate)
        {
            if (activate)
                aiActive = true;
            else aiActive = false;

            if (aiActive)
            {
                navMeshAgent.enabled = true;
            }
            else
            {
                navMeshAgent.enabled = false;
            }
        }

        void Update()
        {
            if (!aiActive)
                return;
            currentState.UpdateState(this);
        }

        public void TransitionToState(State nextState)
        {
            if (nextState != remainState)
            {
                if (currentState != null)
                    previousState = currentState;
                else previousState = nextState;
                currentState = nextState;
                remainState = currentState;
                OnExitState();
            }
        }

        public bool CheckIfCountDownElapsed(float duration)
        {
            stateTimeElapsed += Time.deltaTime;
            return (stateTimeElapsed >= duration);
        }

        private void OnExitState()
        {
            stateTimeElapsed = 0;
        }
    }
}