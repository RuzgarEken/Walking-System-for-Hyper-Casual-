
using UnityEngine;

namespace Generics.Packages.Runner
{

    public class WalkingMove_CharacterController : WalkingMoveBehaviour, ISpeedMovement
    {
        [SerializeField] private CharacterController _characterController;

        [Header("Parameters")]
        [SerializeField] private float _speed;

        public float MoveSpeed => _speed;

        #region Utils

        public override void Move(float fixedDeltaTime)
        {
            _characterController.Move(BaseComponent.Direction * _speed * fixedDeltaTime);
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        #endregion

    }
    
}