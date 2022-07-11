using Generics.Behaviours;
using UnityEngine;

namespace Generics.Packages.Runner
{

    public abstract class WalkingRotationBehaviour : ComponentExtension<WalkingBehaviour>
    {

        protected WalkingBehaviour Runner => BaseComponent;

        #region Utils

        public abstract void Rotate(float fixedDeltaTime);

        #endregion

    }

}