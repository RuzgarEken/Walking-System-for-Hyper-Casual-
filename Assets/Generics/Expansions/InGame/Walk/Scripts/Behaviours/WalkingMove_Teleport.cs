using MyBox;
using UnityEngine;

namespace Generics.Packages.Runner
{

    public class WalkingMove_Teleport : WalkingMoveBehaviour
    {
        [SerializeField] private bool _useRigidbody;
        [ConditionalField(nameof(_useRigidbody)), SerializeField] private Rigidbody _rigidbody;

        #region Utils

        public override void Move(float fixedDeltaTime)
        {
            //Debug.Log($"Rotate: {targetRotation.eulerAngles}");
            if (_useRigidbody)
            {
                _rigidbody.MovePosition(Walking.Destination);
                
            }
            else
            {
                transform.position = Walking.Destination;
            }
        }

        #endregion

    }

}