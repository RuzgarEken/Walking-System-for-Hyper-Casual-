
using UnityEngine;

namespace Generics.Packages.Runner
{


    public class WalkingRotation_LookAt : WalkingRotationBehaviour
    {
        private Transform _lookAt;

        #region Utils

        public override void Rotate(float fixedDeltaTime)
        {
            transform.LookAt(_lookAt, Vector3.up);
        }

        public void SetLookAt(Transform lookAt)
        {
            _lookAt = lookAt;
        }

        #endregion

    }

}