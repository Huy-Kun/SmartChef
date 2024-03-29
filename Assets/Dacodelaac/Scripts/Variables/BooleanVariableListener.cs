using Dacodelaac.Events;
using UnityEngine;

namespace Dacodelaac.Variables
{
    public class BooleanVariableListener : BaseVariableListener<bool, BooleanVariable, BooleanEventResponse>
    {
        [SerializeField] bool reverse;

        public override void OnEventRaised(bool value)
        {
            base.OnEventRaised(reverse ? !value : value);
        }
    }
}