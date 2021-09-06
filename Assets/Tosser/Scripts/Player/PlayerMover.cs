using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tosser.Controls
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private CharacterStats characterStats;
        [SerializeField] private Vector3 heading;

        private Vector3 forward, right;
        private float deadZone = 0.01f;
        private bool notReady = true;
        private CharacterController characterController;
        private List<Component> moveBlockers = new List<Component>();

        IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);

            characterController = GetComponent<CharacterController>();

            // Set direction parameters
            forward = Camera.main.transform.forward;
            forward.y = 0;
            forward = Vector3.Normalize(forward);
            right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

            notReady = false;
        }

        public void AddMoveBlocker(Component component)
        {
            if (!moveBlockers.Contains(component))
                moveBlockers.Add(component);
        }

        public void RemoveMoveBlocker(Component component)
        {
            if (moveBlockers.Contains(component))
                moveBlockers.Remove(component);
        }

        bool CanMove()
        {
            return moveBlockers.Count == 0;
        }

        void Update()
        {
            if (notReady || !CanMove())
                return;

            InputSystem();
        }

        void InputSystem()
        {
            // Movement
            Vector3 rightMovement = PlayerInput.instance.joystickInputVertical * forward.normalized + PlayerInput.instance.joystickInputHorizontal * right.normalized;

            // Rotation
            Vector3 rightRotation = right * characterStats.turnSpeed * Time.deltaTime * PlayerInput.instance.joystickRotateHorizontal;
            Vector3 upRotation = forward * characterStats.turnSpeed * Time.deltaTime * PlayerInput.instance.joystickRotateVertical;

            if (rightRotation.magnitude > deadZone || upRotation.magnitude > deadZone)
            {
                heading = Vector3.Normalize(rightRotation + upRotation);
                Rotate(heading);
            }
            characterController.Move(rightMovement * Time.deltaTime * characterStats.walkSpeed);
        }

        public void Rotate(Vector3 target)
        {
            Quaternion new_rotation = Quaternion.LookRotation(target);
            Quaternion LookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, new_rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, LookAtRotationOnly_Y, Time.deltaTime * characterStats.turnSpeed);
        }
    }
}