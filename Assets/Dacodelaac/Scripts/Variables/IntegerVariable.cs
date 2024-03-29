using UnityEngine;

namespace Dacodelaac.Variables
{
    [CreateAssetMenu(menuName = "Variables/Integer")]
    public class IntegerVariable : BaseVariable<int>
    {
        public void Add(int add) => Value += add;
    }
}