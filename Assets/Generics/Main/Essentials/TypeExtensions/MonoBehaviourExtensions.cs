using Generics.Behaviours;
using System;
using System.Collections;
using UnityEngine;

namespace Essentials.Extensions
{

    public static class MonoBehaviourExtensions
    {
        public static Action AddExtension(this ComponentBase source, ComponentBase extension)
        {
            void OnBaseEnableStateChanged(ComponentBase c, bool state)
            {
                extension.SetEnable(state);
            }

            extension.SetEnable(source.enabled);
            source.EnableStateChanged += OnBaseEnableStateChanged;

            return () => source.EnableStateChanged -= OnBaseEnableStateChanged;
        }

        public static Coroutine DoAfterCondition(this MonoBehaviour m, Func<bool> condition, Action action)
        {
            return m.StartCoroutine(DoAfterConditionRoutine());

            IEnumerator DoAfterConditionRoutine()
            {
                while(true)
                {
                    yield return null;

                    if (condition())
                    {
                        action?.Invoke();
                        break;
                    }
                }
            }
        }

        public static Coroutine DoAfterWait(this MonoBehaviour m, WaitForSeconds wait, Action action)
        {
            return m.StartCoroutine(DoAfterTimeRoutine());

            IEnumerator DoAfterTimeRoutine()
            {
                yield return wait;

                action?.Invoke();
            }
        }

        public static Coroutine DoAfterTime(this MonoBehaviour m, float time, Action action, bool ignoreTimeScale = true)
        {
            return m.StartCoroutine(DoAfterTimeRoutine());

            IEnumerator DoAfterTimeRoutine()
            {
                if(ignoreTimeScale)
                {
                    yield return BetterWaitForSeconds.WaitRealtime(time);
                }
                else
                {
                    yield return BetterWaitForSeconds.Wait(time);

                }

                action?.Invoke();
            }
        }

        public static Coroutine DoAfterFixedUpdate(this MonoBehaviour m, Action action)
        {
            return m.StartCoroutine(DoAfterFixedUpdateRoutine());

            IEnumerator DoAfterFixedUpdateRoutine()
            {
                yield return new WaitForFixedUpdate();

                action?.Invoke();
            }
        }

    }

}