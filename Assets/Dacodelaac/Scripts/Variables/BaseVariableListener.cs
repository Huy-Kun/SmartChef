using Dacodelaac.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Dacodelaac.Variables
{
    public class BaseVariableListener<TType, TEvent, TResponse> : BaseEventListener<TType, TEvent, TResponse>
        where TEvent : BaseVariable<TType>
        where TResponse : UnityEvent<TType>
    {
        [SerializeField] bool setOnEnable;

        public override void Initialize()
        {
            base.Initialize();
            if (setOnEnable)
            {
                OnEventRaised(@event.Value);
            }
        }

        public override void DoEnable()
        {
            base.DoEnable();
            if (setOnEnable)
            {
                OnEventRaised(@event.Value);
            }
        }
    }
}