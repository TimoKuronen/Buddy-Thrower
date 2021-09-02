using UnityEngine;
using Rewired;
using UnityEngine.Events;

namespace Tosser.Controls
{
    public class PlayerInput : MonoBehaviour
    {
        public static PlayerInput instance;
        public Player re_Player; // The Rewired Player

        public float joystickInputHorizontal;
        public float joystickInputVertical;

        public float joystickRotateHorizontal;
        public float joystickRotateVertical;

        public UnityEvent DragButtonPressed;
        public UnityEvent DragButtonReleased;

        private bool controlsLocked;

        public bool ControlsLocked => controlsLocked;

        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            re_Player = ReInput.players.GetPlayer(0);
        }

        // Update is called once per frame
        void Update()
        {
            joystickInputHorizontal = re_Player.GetAxis("Move Horizontal");
            joystickInputVertical = re_Player.GetAxis("Move Vertical");

            joystickRotateHorizontal = re_Player.GetAxis("Rotate Horizontal");
            joystickRotateVertical = re_Player.GetAxis("Rotate Vertical");

            // DRAG INPUT IS HANDLED BY UNITY EVENT
        }

        public void LockControls(bool value)
        {
            controlsLocked = value;
        }
    }
}