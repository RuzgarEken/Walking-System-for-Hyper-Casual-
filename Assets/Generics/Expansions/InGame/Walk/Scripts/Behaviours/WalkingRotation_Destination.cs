using MyBox;
using UnityEngine;

namespace Generics.Packages.Runner
{

    public class WalkingRotation_Destination : WalkingRotationBehaviour
    {
        [SerializeField] protected float _rotationLerpSpeed;
        [SerializeField] private bool _useRigidbody;
        [ConditionalField(nameof(_useRigidbody)), SerializeField] private Rigidbody _rigidbody;

        #region Utils

        public override void Rotate(float fixedDeltaTime)
        {
            if (Runner.Direction.magnitude <= Mathf.Epsilon) return;

            var yRotation =
                Quaternion.LookRotation(
                    Runner.Direction,
                    Vector3.up
                );

            var targetRotation = yRotation;
            targetRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f); //todo: fix here later

            //Debug.Log($"Rotate: {targetRotation.eulerAngles}");
            if (_useRigidbody)
            {
                _rigidbody.MoveRotation(
                    Quaternion.Lerp(transform.rotation, targetRotation, _rotationLerpSpeed * fixedDeltaTime)
                );
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationLerpSpeed * fixedDeltaTime);
            }

        }

        #endregion

    }

}