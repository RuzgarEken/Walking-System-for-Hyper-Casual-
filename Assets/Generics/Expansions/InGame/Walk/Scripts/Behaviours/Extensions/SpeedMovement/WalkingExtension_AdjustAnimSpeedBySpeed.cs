using Generics.Behaviours;
using UnityEngine;

namespace Generics.Packages.Runner
{

    public class WalkingExtension_AdjustAnimSpeedBySpeed : ComponentExtension<WalkingMove_Speed>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _walkAnimSpeedKey;
        [SerializeField] private Vector2 _animationSpeedMinMax;
        [SerializeField] private Vector2 _moveSpeedThresholds;

        private float _moveSpeedRange;
        private float _animationSpeedRange;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            _animationSpeedRange = _animationSpeedMinMax.y - _animationSpeedMinMax.x;
            _moveSpeedRange = _moveSpeedThresholds.y - _moveSpeedThresholds.x;
        }

        protected virtual void FixedUpdate()
        {
            UpdateAnimationSpeed();
        }

        #endregion

        #region Helpers

        private void UpdateAnimationSpeed()
        {
            float animationSpeed = 1f;
            float speed = BaseComponent.MoveSpeed;

            if (speed <= _moveSpeedThresholds.x)
            {
                animationSpeed = _animationSpeedMinMax.x;
            }
            else if (speed <= _moveSpeedThresholds.y)
            {
                var rate = (speed - _moveSpeedThresholds.x) / (_moveSpeedRange);
                animationSpeed = _animationSpeedMinMax.x + _animationSpeedRange* rate;
            }
            else //if (speed > _speed.y)
            {
                animationSpeed = _animationSpeedMinMax.y;
            }

            _animator.SetFloat(_walkAnimSpeedKey, animationSpeed);
        }

        #endregion

    }

}