using Generics.Behaviours;
using UnityEngine;

namespace Generics.Packages.Runner
{

    public class WalkingExtension_AdjustSpeedByDistanceToDestination : ComponentExtension<WalkingMove_Speed>
    {
        [SerializeField, Tooltip("Uzakliga gore kosma hizlari")] 
        private Vector2 _speedMinMax;
        
        [SerializeField, Tooltip("X degeri min hizda kalma uzakligi, Y degeri max hiz esigi")] 
        private Vector2 _distanceThresholds;

        private float _speedRange;
        private float _distanceRange;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            _speedRange = _speedMinMax.y - _speedMinMax.x;
            _distanceRange = _distanceThresholds.y - _distanceThresholds.x;
        }

        protected virtual void FixedUpdate()
        {
            UpdateSpeed();
        }

        #endregion

        #region Helpers

        private void UpdateSpeed()
        {
            var distance = (BaseComponent.Walking.Destination - transform.position).magnitude;

            if (distance <= _distanceThresholds.x)
            {
                BaseComponent.SetSpeed(_speedMinMax.x);
            }
            else if(distance <= _distanceThresholds.y)
            {
                var rate = (distance - _distanceThresholds.x) / (_distanceRange);
                BaseComponent.SetSpeed(_speedMinMax.x + _speedRange * rate);
            }
            else //if (distance > _speedThresholds.y)
            {
                BaseComponent.SetSpeed(_speedMinMax.y);
            }
        }

        #endregion

    }

}