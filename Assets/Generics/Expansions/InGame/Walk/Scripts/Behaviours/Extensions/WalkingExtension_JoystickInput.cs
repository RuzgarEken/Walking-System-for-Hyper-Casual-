using Generics.Behaviours;
using Generics.Packages.InputSystem;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Generics.Packages.Runner
{

    public class WalkingExtension_JoystickInput : ComponentBase
    {
        [SerializeField] private WalkingBehaviour _walker;
        
        [Header("Parameters")]
        [SerializeField] private bool _allignWithCameraY = true;

        private Transform _mainCamera;

        protected Transform MainCamera => _mainCamera ??= Camera.main.transform;
        protected bool Touching { get; private set; }

        protected Vector2 CursorMovement = Vector2.zero;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            InputController.Instance.Input.Player.JoystickMove.performed += OnJoystickMovePerformed;
            InputController.Instance.Input.Player.JoystickMove.canceled += OnJoystickMoveCanceled;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            InputController.Instance.Input.Player.JoystickMove.performed -= OnJoystickMovePerformed;
            InputController.Instance.Input.Player.JoystickMove.canceled -= OnJoystickMoveCanceled;
        }

        protected virtual void FixedUpdate()
        {
            if (!Touching) return;

            var movement = new Vector3(CursorMovement.x, 0f, CursorMovement.y) * 10f * Time.fixedDeltaTime;
            if (_allignWithCameraY)
            {
                movement = Quaternion.Euler(0f, MainCamera.eulerAngles.y, 0f) * movement;
            }

            var destination = _walker.transform.position + movement;
            _walker.SetDestination(destination);
        }

        #endregion

        #region Input Listeners

        protected virtual void OnJoystickMovePerformed(CallbackContext context)
        {
            _walker.enabled = true;
            CursorMovement = context.ReadValue<Vector2>();
            Touching = true;

        }

        protected virtual void OnJoystickMoveCanceled(CallbackContext context)
        {
            _walker.enabled = false;
            CursorMovement = Vector2.zero;
            Touching = false;
        }

        #endregion

    }

}