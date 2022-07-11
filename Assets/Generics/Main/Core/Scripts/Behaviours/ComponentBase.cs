using Generics.Utility.Lock;
using System;
using UnityEngine;

namespace Generics.Behaviours
{

    public abstract class ComponentBase : MonoBehaviour
    {
        public event Action<ComponentBase, bool> EnableStateChanged;

        /// <summary>EditorOnly</summary>
        public bool IsSRTarget;

        protected bool IsInitialized;

        private Locker _enableLocker = new Locker();

        public int InstanceId { get; private set; }

        #region Unity Methods

        protected virtual void Awake()
        {
            if (!IsInitialized) Initialize();
        }

        protected virtual void OnDestroy()
        {
            _enableLocker.LockStatusChanged -= OnEnableLockStatusChanged;
        }

        protected virtual void OnEnable()
        {
            EnableStateChanged?.Invoke(this, true);
        }

        protected virtual void OnDisable()
        {
            EnableStateChanged?.Invoke(this, false);
        }

        #endregion

        #region Utils

        public virtual void SetEnable(bool value, string key = "default")
        {
            if (!IsInitialized) Initialize();

            if (value && !_enableLocker.IsLocked)
            {
                OnEnableLockStatusChanged(false);
                return;
            }
            
            _enableLocker.Lock(!value, key);
        }

        #endregion

        #region Listeners

        protected virtual void OnEnableLockStatusChanged(bool locked)
        {
            InternalEnable(!locked);
        }

        #endregion

        #region Helpers

        protected virtual void Initialize()
        {
            _enableLocker.LockStatusChanged += OnEnableLockStatusChanged;

            InstanceId = GetInstanceID();
            IsInitialized = true;
        }

        protected virtual void InternalEnable(bool enable)
        {
            enabled = enable;
        }

        #endregion

        #region Editor

#if UNITY_EDITOR

        protected virtual void OnValidate() { }

#endif

        #endregion

    }

}