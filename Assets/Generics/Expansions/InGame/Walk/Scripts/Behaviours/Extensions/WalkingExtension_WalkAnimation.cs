using Generics.Behaviours;
using UnityEngine;

namespace Generics.Packages.Runner
{

    public class WalkingExtension_WalkAnimation : ComponentExtension<WalkingBehaviour>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _runAnimationId = "Walking";

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _animator.SetBool(_runAnimationId, true);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _animator.SetBool(_runAnimationId, false);
        }

        #endregion

        #region Unity Editor

#if UNITY_EDITOR

        protected override void OnValidate()
        {
            base.OnValidate();

            if (_animator == null) _animator = GetComponentInChildren<Animator>();
        }
#endif

        #endregion

    }

}