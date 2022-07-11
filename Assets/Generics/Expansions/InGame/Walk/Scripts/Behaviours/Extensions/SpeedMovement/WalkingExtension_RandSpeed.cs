using Essentials.Extensions;
using Generics.Behaviours;
using Generics.Packages.Runner;
using UnityEngine;

namespace Generics.Packages.Walk
{

    public class WalkingExtension_RandSpeed : ComponentExtension<WalkingBehaviour>
    {
        [SerializeField] private Vector2 _speedRange;

        #region Unity Methods

        protected override void Awake()
        {
            var speedMovement = BaseComponent.MoveBehaviour as ISpeedMovement;
            if (speedMovement != null)
            {
                speedMovement.SetSpeed(_speedRange.Random());
            }
        }

        #endregion

    }

}