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

        public delegate void DragInputEvent();
        public event DragInputEvent dragEvent;

        public delegate void ThrowInputEvent();
        public event ThrowInputEvent throwEvent;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            re_Player = ReInput.players.GetPlayer(0);
        }

        void Update()
        {
            joystickInputHorizontal = re_Player.GetAxis("Move Horizontal");
            joystickInputVertical = re_Player.GetAxis("Move Vertical");

            joystickRotateHorizontal = re_Player.GetAxis("Rotate Horizontal");
            joystickRotateVertical = re_Player.GetAxis("Rotate Vertical");

            if (re_Player.GetButtonDown("Drag") || Input.GetKeyDown(KeyCode.D))
                dragEvent?.Invoke();
            if (re_Player.GetButtonDown("Throw") || Input.GetKeyDown(KeyCode.T))
                throwEvent?.Invoke();
        }
    }
}