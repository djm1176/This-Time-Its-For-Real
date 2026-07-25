using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour, StarterAssetsInputActions.IPlayerActions
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool attack;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		[Header("Weapons")]
		public Animator gripAnimator;

		[Space()]

		StarterAssetsInputActions _inputActions;


		void Start()
        {
               if (_inputActions == null)
        {
            _inputActions = new StarterAssetsInputActions();
        }
        _inputActions.Player.SetCallbacks(this);
        _inputActions.Player.Enable();     
        }

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

        public void OnMove(InputAction.CallbackContext context)
		{
			move = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (cursorInputForLook)
			{
				look = context.ReadValue<Vector2>();
			}
        }

        public void OnJump(InputAction.CallbackContext context)
		{
			bool isPressed = context.ReadValue<float>() == 1;
			jump = isPressed;

        }

        public void OnSprint(InputAction.CallbackContext context)
		{
			bool isPressed = context.ReadValue<float>() == 1;
			sprint = isPressed;
        }

        public void OnAttack(InputAction.CallbackContext context)
		{
			bool isPressed = context.ReadValue<float>() == 1 && context.phase == InputActionPhase.Started;
			gripAnimator.SetTrigger("Attack");
        }
    }

}