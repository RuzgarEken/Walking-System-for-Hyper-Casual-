using UnityEngine;

namespace Generics.Behaviours
{

    public class ComponentExtension<T> : ComponentBase
        where T : ComponentBase
    {
        [SerializeField] private T _baseComponent;

        protected T BaseComponent => _baseComponent;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            if (_baseComponent)
            {
                OnBaseComponentEnableStateChanged(_baseComponent, _baseComponent.enabled);
                _baseComponent.EnableStateChanged += OnBaseComponentEnableStateChanged;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _baseComponent.EnableStateChanged -= OnBaseComponentEnableStateChanged;
        }

        #endregion

        #region Listeners

        private void OnBaseComponentEnableStateChanged(ComponentBase componentBase, bool isEnabled)
        {
            SetEnable(isEnabled);
        }

        #endregion

        #region Unity Editor

#if UNITY_EDITOR

        protected override void OnValidate()
        {
            base.OnValidate();

            if (!_baseComponent)
            {
                _baseComponent = GetComponent<T>();
            }
        }
#endif

        #endregion
    }

}