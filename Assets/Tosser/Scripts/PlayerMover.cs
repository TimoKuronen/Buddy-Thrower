using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tosser.Controls
{
    public class PlayerMover : MonoBehaviour
    {
        public CharacterStats characterStats;
        Vector3 forward, right;
        [SerializeField] private Vector3 heading;
        bool notReady = true;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);

            forward = Camera.main.transform.forward;
            forward.y = 0;
            forward = Vector3.Normalize(forward);
            right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
            notReady = false;
        }

        void Update()
        {
            if (notReady || PlayerInput.instance.ControlsLocked)
                return;
            InputSystem();
        }

        void InputSystem()
        {
            Vector3 direction = new Vector3(PlayerInput.instance.joystickInputHorizontal, 0, PlayerInput.instance.joystickInputVertical);
            Vector3 rightMovement = right * characterStats.walkSpeed * Time.deltaTime * PlayerInput.instance.joystickInputHorizontal;
            Vector3 upMovement = forward * characterStats.walkSpeed * Time.deltaTime * PlayerInput.instance.joystickInputVertical;
            heading = Vector3.Normalize(rightMovement + upMovement);

            Vector3 rightRotation = right * PlayerInput.instance.joystickRotateHorizontal;
            Vector3 upRotation = forward * PlayerInput.instance.joystickInputVertical;

            Rotate(heading);

            transform.position += rightMovement;
            transform.position += upMovement;
        }

        public void Rotate(Vector3 target)
        {
            Quaternion new_rotation = Quaternion.LookRotation(target);
            Quaternion LookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, new_rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, LookAtRotationOnly_Y, Time.deltaTime * characterStats.turnSpeed);
        }
    }
}