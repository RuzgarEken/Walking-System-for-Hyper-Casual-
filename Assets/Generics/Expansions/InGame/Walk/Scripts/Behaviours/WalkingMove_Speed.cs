using MyBox;
using UnityEngine;

namespace Generics.Packages.Runner
{

    public interface ISpeedMovement
    {
        float MoveSpeed { get; }
        void SetSpeed(float speed);
    }

    public class WalkingMove_Speed : WalkingMoveBehaviour, ISpeedMovement
    {
        [SerializeField] private bool _useRigidbody;
        [ConditionalField(nameof(_useRigidbody)), SerializeField] private Rigidbody _rigidbody;

        [Header("Parameters")]
        [SerializeField] protected float _moveSpeed;

        public float MoveSpeed => _moveSpeed;

        private Vector3 _targetPos;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _targetPos = transform.position;
        }

        #endregion

        #region Helpers

        public override void Move(float fixedDeltaTime)
        {
            _targetPos += Walking.Direction * _moveSpeed * fixedDeltaTime;

            if (_useRigidbody)
            {
                _rigidbody.MovePosition(_targetPos);

            }
            else
            {
                transform.position = _targetPos;
            }

            Walking.Direction = (Walking.Destination - transform.position).normalized;
        }

        #endregion

        #region Utils

        public virtual void SetSpeed(float moveSpeed) => _moveSpeed = moveSpeed;

        #endregion

    }

}