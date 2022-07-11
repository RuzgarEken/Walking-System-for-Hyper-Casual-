using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Generics.Editor.Inspector
{

    public class ComponentInspectorBase<T> : UnityEditor.Editor
        where T : Component
    {
        protected bool FoldoutActive = true;
        protected T Target { get; private set; }
        protected SerializedObject SerializedTarget { get; private set; }

        private static GUIStyle _foldoutStyle;
        private static GUIStyle FoldoutStyle =>
            _foldoutStyle ??= new GUIStyle()
            {
                fontStyle = FontStyle.Bold,
                
                normal = new GUIStyleState()
                {
                    textColor = new Color(0.9490196f, 0.9294118f, 0.6352941f)
                }
            };

        private static GUIStyle _headerStyle;
        private static GUIStyle HeaderStyle =>
            _headerStyle ??= new GUIStyle()
            {
                fontStyle = FontStyle.Bold,

                normal = new GUIStyleState()
                {
                    textColor = new Color(0.4196078f, 0.8f, 0.9490196f),
                }
            };

        private static GUIStyle _buttonStyle;
        private static GUIStyle ButtonStyle =>
            _buttonStyle ??= new GUIStyle(GUI.skin.button)
            {
                fixedWidth = 200f,
                
                //normal = new GUIStyleState()
                //{
                //    textColor = new Color(0.9490196f, 0.854902f, 0.5882353f)
                //}
            };

        private static Dictionary<T, bool> _foldoutStates = new Dictionary<T, bool>();
        private Dictionary<string, UnityEngine.Object> _buttonReferences = new Dictionary<string, UnityEngine.Object>();

        #region Unity Methods

        private void OnEnable()
        {
            Target = (T)target;
            SerializedTarget = new SerializedObject(Target);
            if (!_foldoutStates.TryGetValue(Target, out var state))
            {
                _foldoutStates.Add(Target, true);
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space(15f);
            _foldoutStates[Target] = EditorGUILayout.Foldout(_foldoutStates[Target], "Options", true, FoldoutStyle);
            if (!_foldoutStates[Target])
            {
                return;
            }

            DrawOptions();
        }

        #endregion

        #region Helpers

        protected virtual void DrawHeader(string text)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(text, HeaderStyle);
        }

        protected virtual void DrawOptions() { }

        protected void DrawButton(string text, Action onClick = null)
        {
            if (GUILayout.Button(text, ButtonStyle))
            {
                onClick?.Invoke();
            }
        }

        protected void DrawButtonWithReference<TComponent>(string text, Action<TComponent> onClick = null)
            where TComponent : UnityEngine.Object
        {
            if (!_buttonReferences.TryGetValue(text, out var reference))
            {
                _buttonReferences.Add(text, null);
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(text, ButtonStyle))
            {
                onClick.Invoke(_buttonReferences[text] as TComponent);
            }
            _buttonReferences[text] = EditorGUILayout.ObjectField("", _buttonReferences[text], typeof(TComponent));
            EditorGUILayout.EndHorizontal();
        }

        protected void DrawComponentOption<TComponent>(string toggleText, Predicate<T> validator = null, Action<TComponent> onAdd = null, Action onRemove = null)
            where TComponent : Component
        {
            bool status, cachedStatus;
            var component = Target.gameObject.GetComponent<TComponent>();
            status = cachedStatus = component != null;

            if(validator != null && !(bool)(validator?.Invoke(Target)))
            {
                if (status) DestroyImmediate(component);
                return;
            }

            status = EditorGUILayout.Toggle(toggleText, status);

            EditorGUI.BeginChangeCheck();
            if (status != cachedStatus)
            {
                if (status)
                {
                    Undo.RecordObject(Target.gameObject, Target.name);
                    component = Target.gameObject.AddComponent<TComponent>();
                    onAdd?.Invoke(component);
                }
                else
                {
                    DestroyImmediate(component);
                    component = null;
                    onRemove?.Invoke();
                }
            }
            EditorGUI.EndChangeCheck();
        }

        protected void DrawCreateChildOption<TComponent>(string toggleText, GameObject prefab, Transform target = null, bool defaultActive = true, Action<TComponent> onAdd = null, Action onRemove = null)
            where TComponent : Component
        {
            bool status, cachedStatus;
            target = target ?? Target.transform;
            var component = target.GetComponentInChildren<TComponent>();
            status = cachedStatus = component != null;

            status = EditorGUILayout.Toggle(toggleText, status);

            EditorGUI.BeginChangeCheck();
            if (status != cachedStatus)
            {
                if (status)
                {
                    component = (PrefabUtility.InstantiatePrefab(prefab, target) as GameObject).GetComponent<TComponent>();
                    component.gameObject.SetActive(defaultActive);
                    onAdd?.Invoke(component);
                }
                else
                {
                    DestroyImmediate(component.gameObject);
                    component = null;
                    onRemove?.Invoke();
                }

                var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
                if (prefabStage != null)
                {
                    EditorSceneManager.MarkSceneDirty(prefabStage.scene);
                }

                EditorUtility.SetDirty(Target);
            }
            EditorGUI.EndChangeCheck();
        }

        protected void DrawCreateChildOption(string toggleText, GameObject prefab, Transform parent = null, bool defaultActive = true, Action<GameObject> onAdd = null, Action onRemove = null)
        {
            bool status, cachedStatus;
            parent = parent ?? Target.transform;
            //var transform = parent.GetChildren().ToList().Find(t => PrefabUtility.GetCorrespondingObjectFromSource(t.gameObject) == prefab);
            var transform = parent.Cast<Transform>().ToList().Find(t => PrefabUtility.GetCorrespondingObjectFromOriginalSource(t.gameObject) == prefab);
            var obje = transform != null ? transform.gameObject : null;
            status = cachedStatus = obje != null;
            status = EditorGUILayout.Toggle(toggleText, status);

            EditorGUI.BeginChangeCheck();
            if (status != cachedStatus)
            {
                if (status)
                {
                    obje = PrefabUtility.InstantiatePrefab(prefab, parent) as GameObject;
                    obje.gameObject.SetActive(defaultActive);
                    onAdd?.Invoke(obje);
                }
                else
                {
                    DestroyImmediate(obje);
                    obje = null;
                    onRemove?.Invoke();
                }

                var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
                if (prefabStage != null)
                {
                    EditorSceneManager.MarkSceneDirty(prefabStage.scene);
                }
            }
            EditorGUI.EndChangeCheck();
        }

        protected GameObject CreateChild(GameObject prefab, Transform parent = null, bool defaultActive = true, Action<GameObject> onAdd = null)
        {
            parent = parent ?? Target.transform;  
            var obje = PrefabUtility.InstantiatePrefab(prefab, parent) as GameObject;
            obje.gameObject.SetActive(defaultActive);
            onAdd?.Invoke(obje);

            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null)
            {
                EditorSceneManager.MarkSceneDirty(prefabStage.scene);
            }

            return obje;
        }

        #endregion

    }

}