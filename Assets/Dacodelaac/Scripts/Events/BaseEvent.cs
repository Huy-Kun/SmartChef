using System.Collections.Generic;
using System.Reflection;
using Dacodelaac.Core;
using Dacodelaac.DebugUtils;
using Dacodelaac.EditorUtils;
using Dacodelaac.Utils;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Dacodelaac.Events
{
    public class BaseEvent : BaseSO, IEvent
    {
        readonly List<IEventListener> listeners = new List<IEventListener>();

        public void Raise()
        {
#if UNITY_EDITOR
            // Dacoder.Log($"===> {name}");
#endif
            for (var i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void AddListener(IEventListener listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        public void RemoveListener(IEventListener listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }

        public void RemoveAll()
        {
            listeners.Clear();
        }
    }

    public class BaseEvent<TType> : BaseSO, IEvent<TType>
    {
        [SerializeField, HideInInspector] TType debugValue;
        
        readonly List<IEventListener<TType>> listeners = new List<IEventListener<TType>>();

        public virtual void Raise(TType value)
        {
#if UNITY_EDITOR
            // Dacoder.Log($"===> {name}");
#endif
            for (var i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(value);
            }
            
        }

        public void AddListener(IEventListener<TType> listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        public void RemoveListener(IEventListener<TType> listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }

        public void RemoveAll()
        {
            listeners.Clear();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(BaseEvent), true), CanEditMultipleObjects]
    public class BaseEventEditor : Editor
    {
        BaseEvent baseEvent;

        void OnEnable()
        {
            baseEvent = target as BaseEvent;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Raise"))
            {
                baseEvent.Raise();
            }
        }
    }
    
    [CustomEditor(typeof(BaseEvent<>), true), CanEditMultipleObjects]
    public class TypedBaseEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
    
            var debugProperty = serializedObject.FindProperty("debugValue");
            if (debugProperty != null)
            {
                using (new EditorGUIUtils.VerticalHelpBox())
                {
                    using (var scope = new EditorGUI.ChangeCheckScope())
                    {
                        EditorGUILayout.PropertyField(debugProperty, new GUIContent("Debug"));
    
                        if (scope.changed)
                        {
                            serializedObject.ApplyModifiedProperties();
                        }
                    }
                    DrawMethods();
                }
            }
        }

        protected virtual void DrawMethods()
        {
            if (GUILayout.Button("Raise"))
            {
                var targetType = target.GetType();
                var targetField = targetType.GetFieldRecursive("debugValue", BindingFlags.Instance | BindingFlags.NonPublic);
                var debugValue = targetField.GetValue(target);

                while (targetType != null)
                {
                    var raiseMethod = targetType.GetMethod("Raise", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
                    if (raiseMethod != null)
                    {
                        raiseMethod?.Invoke(target, new[] {debugValue});
                        break;
                    }
                    else
                    {
                        targetType = targetType.BaseType;
                    }
                }
            }
        }
    }
#endif
}