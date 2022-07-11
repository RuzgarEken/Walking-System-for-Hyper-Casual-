using Essentials.Extensions;
using Generics.Behaviours;
using Generics.Packages.Runner;
using UnityEngine;

namespace Game.Behaviours
{

    public class PatrolBehaviour : ComponentBase
    {
        [SerializeField] private WalkingBehaviour _walker;
        [SerializeField] private Transform[] _points;

        [Header("Parameters")]
        [SerializeField] private float _waitTime = 0.75f;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            MoveToNextPoint();
        }

        #endregion

        #region Listeners

        private void OnArrive()
        {
            _walker.enabled = false;
            this.DoAfterTime(_waitTime, MoveToNextPoint);
        }

        #endregion

        #region Helpers

        private void MoveToNextPoint()
        {
            var point = _points.GetRandomElement();

            if (!_walker.SetDestination(point.position, OnArrive))
            {
                OnArrive();
                return;
            }

            _walker.enabled = true;
        }

        #endregion

    }

}