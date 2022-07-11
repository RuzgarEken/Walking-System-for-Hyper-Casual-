using UnityEngine;

namespace Essentials.Utilities
{

    public class SingletonBehaviour<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if(_instance == null)
                    {
                        Debug.Log($"There is no {typeof(T).Name} in the scene");
                    }
                }

                return _instance;
            }
        }

    }

}