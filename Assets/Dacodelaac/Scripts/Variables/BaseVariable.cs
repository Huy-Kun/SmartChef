using System;
using System.Reflection;
using Dacodelaac.DataStorage;
using Dacodelaac.Events;
using Dacodelaac.Utils;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Dacodelaac.Variables
{
    public class BaseVariable<TType> : BaseEvent<TType>, IVariable<TType>, ISerializationCallbackReceiver
    {
        [SerializeField] TType initializeValue;
        [SerializeField] bool isSavable;
        [SerializeField] bool isRaiseEvent;
        [NonSerialized] TType runtimeValue;

        public TType Value
        {
            get => isSavable ? GameData.Get(Id, initializeValue) : runtimeValue;
            set
            {
                if (isSavable)
                {
                    GameData.Set(Id, value);
                }
                else
                {
                    runtimeValue = value;
                }
                if (isRaiseEvent)
                {
                    Raise(value);
                }
            }
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            runtimeValue = initializeValue;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(BaseVariable<>), true)]
    public class TypedBaseVariableEditor : TypedBaseEventEditor
    {
        protected override void DrawMethods()
        {
            if (GUILayout.Button("Raise"))
            {
                var targetType = target.GetType();
                var targetField =
                    targetType.GetFieldRecursive("debugValue", BindingFlags.Instance | BindingFlags.NonPublic);
                var debugValue = targetField.GetValue(target);

                var property = targetType.GetProperty("Value", BindingFlags.Instance | BindingFlags.Public);
                property?.SetValue(target, debugValue, null);
            }
        }
    }
#endif
}