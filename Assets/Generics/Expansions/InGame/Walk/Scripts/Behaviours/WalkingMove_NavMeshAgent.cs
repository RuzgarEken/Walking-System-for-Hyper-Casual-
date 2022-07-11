using Generics.Packages.Runner;
using UnityEngine;
using UnityEngine.AI;

namespace Generics.Packages.Walk
{

    public class WalkingMove_NavMeshAgent : WalkingMoveBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        private NavMeshPath _path;
        private bool _updateDestination;

        private NavMeshPath Path => _path ?? (Application.isPlaying ? (_path = new NavMeshPath()) : null);

        #region Base Methods

        public override bool IsDestinationValid(Vector3 destination)
        {
            return _agent.CalculatePath(Walking.Destination, Path) /*&& Path.corners.Length > 1*/;
        }

        public override void Move(float fixedDeltaTime) 
        { 
            if (!_updateDestination) return;

            _updateDestination = false;
            _agent.SetDestination(BaseComponent.Destination);
        }

        #endregion

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _updateDestination = true;
            BaseComponent.DestinationChanged += OnDestinationChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            BaseComponent.DestinationChanged -= OnDestinationChanged;
            _updateDestination = false;
        }

        #endregion

        #region Listeners

        private void OnDestinationChanged(Vector3 destination)
        {
            _updateDestination = true;
        }

        #endregion


    }

}