using Essentials.Extensions;
using Generics.Behaviours;
using System;
using UnityEngine;

namespace Generics.Packages.Runner
{

    public class WalkingExtension_FollowTargetInput : ComponentBase
    {
        [SerializeField] private WalkingBehaviour _runner;
        [SerializeField] private ComponentBase _followTarget;

        private Transform _followTargetTransform;
        private Action _extensionDisposer;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            SetTarget(_followTarget);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _runner.SetEnable(true, "FollowTarget");
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _runner.SetEnable(false, "FollowTarget");
        }

        private void FixedUpdate()
        {
            _runner.SetDestination(_followTargetTransform.position);
        }

        #endregion

        #region Utils

        public void SetTarget(ComponentBase target)
        {
            _extensionDisposer?.Invoke();

            _followTarget = target;
            if (_followTarget == null)
            {
                SetEnable(false, "FollowTarget");
                return;
            }

            _followTargetTransform = target.transform;

            SetEnable(true, "FollowTarget");
            target.AddExtension(this);
        }

        #endregion

    }

}