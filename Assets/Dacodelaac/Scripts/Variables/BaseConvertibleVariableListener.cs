using Dacodelaac.Events;
using UnityEngine;

namespace Dacodelaac.Variables
{
    public class BaseConvertibleVariableListener<TType, TEvent> : BaseConvertibleEventListener<TType, TEvent>
        where TEvent : BaseVariable<TType>
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