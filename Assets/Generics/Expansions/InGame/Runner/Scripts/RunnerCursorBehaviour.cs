using Cinemachine;
using Generics.Behaviours;
using Generics.Packages.InputSystem;
using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Generics.Packages.Runner
{

    public class RunnerCursorBehaviour : ComponentBase
    {
        public event Action MovementCompleted;
        public event Action<float> ProgressUpdated;

        [SerializeField] private CinemachinePath _path;

        [Header("Forward Movement")]
        [SerializeField] private Transform _pathTracker;
        [SerializeField] private float _forwardSpeed;

        [Header("Horizontal Movement")]
        [SerializeField] private Transform _horizontalCursor;
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private float _horizontalLerpSpeed;
        [SerializeField] private float _horizontalDeadzone;

        public Transform HorizontalCursor => _horizontalCursor;
        public Transform VerticalCursor => _pathTracker;

        private float _position;
        private float _horizontalOffset;
        private bool _touching;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            _path.EvaluatePositionAtUnit(0f, CinemachinePathBase.PositionUnits.Distance); //to eliminate first initialization overhead
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            InputController.Instance.Input.Player.Touch.performed += OnTouchPerformed;
            InputController.Instance.Input.Player.Touch.canceled  += OnTouchCanceled;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            InputController.Instance.Input.Player.Touch.performed += OnTouchPerformed;
            InputController.Instance.Input.Player.Touch.canceled += OnTouchCanceled;
        }

        protected virtual void FixedUpdate()
        {
            var fixedDeltaTime = Time.fixedDeltaTime;

            _position += _forwardSpeed * fixedDeltaTime;
            var unitPos = Math.Min(_position / _path.PathLength, 1f);
            ProgressUpdated?.Invoke(unitPos);

            //Forward movement
            var worldPos = _path.EvaluatePositionAtUnit(_position, CinemachinePathBase.PositionUnits.Distance);
            _pathTracker.position = worldPos;

            //Horizontal movement
            if (_touching)
            {
                var dif = InputController.Instance.Input.Player.Move.ReadValue<Vector2>();
                var movement = dif.x * _horizontalSpeed * fixedDeltaTime;

                _horizontalOffset = Mathf.Clamp(movement + _horizontalOffset, -_horizontalDeadzone, _horizontalDeadzone);

                var horizontalWorldPos = _pathTracker.TransformPoint(new Vector3(_horizontalOffset, 0f, 0f));
                _horizontalCursor.position = Vector3.Lerp(_horizontalCursor.position, horizontalWorldPos, _horizontalLerpSpeed * fixedDeltaTime);

            }

            if (unitPos == 1f)
            {
                CompleteMovement();
            }
        }

        #endregion

        #region Listeners

        private void OnTouchPerformed(CallbackContext context)
        {
            _touching = true;
        }

        private void OnTouchCanceled(CallbackContext context)
        {
            _touching = false;
        }

        #endregion

        #region Helpers

        protected virtual void CompleteMovement()
        {
            MovementCompleted?.Invoke();
            enabled = false;
        }

        public void SetDeadzoneDistance(float deadzoneDistance)
        {
            _horizontalDeadzone = deadzoneDistance;
        }

        #endregion

    }

}
