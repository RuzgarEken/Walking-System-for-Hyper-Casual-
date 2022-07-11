using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

namespace Generics.Packages.InputSystem
{

    public class ScreenJoystickExtension_TeleportToPointer : MonoBehaviour
    {
        [SerializeField] private Image[] _joystickImages;
        [SerializeField] private ScreenJoystick _screenStick;

        private Vector2 _touchPosition;

        protected bool IsInputValid => !EventSystem.current.IsPointerOverGameObject();

        #region Unity Methods

        private void OnEnable()
        {
            InputController.Instance.Input.Player.Touch.performed += OnTouchPerformed;
            InputController.Instance.Input.Player.Touch.canceled += OnTouchCanceled;
            InputController.Instance.Input.Player.TouchPosition.performed += OnTouchPositionUpdated;
        }

        private void OnDisable()
        {
            InputController.Instance.Input.Player.Touch.performed -= OnTouchPerformed;
            InputController.Instance.Input.Player.Touch.canceled -= OnTouchCanceled;
            InputController.Instance.Input.Player.TouchPosition.performed -= OnTouchPositionUpdated;
        }

        #endregion

        #region Listeners

        private void OnTouchPerformed(CallbackContext context)
        {
            if (!IsInputValid) return;

            //Debug.Log("Touch Performed");
            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (
                    _screenStick.Parent,
                    _touchPosition,
                    null,
                    out var pos
                );

            _screenStick.m_StartPos = pos;
            _screenStick.Rect.anchoredPosition = pos;
            _screenStick.OnPointerDown(new PointerEventData(EventSystem.current));

            foreach (var image in _joystickImages)
            {
                image.enabled = true;
            }
        }

        private void OnTouchPositionUpdated(CallbackContext context)
        {
            _touchPosition = context.ReadValue<Vector2>();
        }

        private void OnTouchCanceled(CallbackContext context)
        {
            foreach (var image in _joystickImages)
            {
                image.enabled = false;
            }
        }

        #endregion

    }

}