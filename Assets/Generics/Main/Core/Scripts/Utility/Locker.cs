using System;
using System.Collections.Generic;

namespace Generics.Utility.Lock
{

    public class Locker
    {
        /// <summary> Locked or not </summary>
        public event Action<bool> LockStatusChanged;

        private List<string> _lockers = new List<string>();

        public bool IsLocked => _lockers.Count > 0;

        public void Lock(bool value, string id)
        {
            if (value)
            {
                if (_lockers.Contains(id))
                {
                    return;
                }

                _lockers.Add(id);

                if (_lockers.Count == 1)
                    LockStatusChanged?.Invoke(true);
            }
            else
            {
                if (!_lockers.Remove(id))
                    return;

                if (_lockers.Count == 0)
                    LockStatusChanged?.Invoke(false);
            }
        }

        public void Clear()
        {
            _lockers.Clear();

            LockStatusChanged?.Invoke(false);
        }

    }

}