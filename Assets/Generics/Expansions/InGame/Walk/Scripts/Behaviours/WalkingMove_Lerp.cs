using MyBox;
using UnityEngine;

namespace Generics.Packages.Runner
{

    public class WalkingMove_Lerp : WalkingMoveBehaviour
    {
        [SerializeField] private bool _useRigidbody;
        [ConditionalField(nameof(_useRigidbody)), SerializeField] private Rigidbody _rigidbody;

        [SerializeField] protected float _moveLerpSpeed;

        #region Utils

        public override void Move(float fixedDeltaTime)
        {
            var pos = Vector3.Lerp(_rigidbody.position, Walking.Destination, fixedDeltaTime * _moveLerpSpeed);

            if (_useRigidbody)
            {
                _rigidbody.MovePosition(pos);
            }
            else
            {
                transform.position = pos;
            }
        }

        #endregion

        #region Unity Editor

#if UNITY_EDITOR

        protected override void OnValidate()
        {
            base.OnValidate();

            if (_useRigidbody) _rigidbody ??= GetComponent<Rigidbody>();
        }

#endif

        #endregion

    }

}