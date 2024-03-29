using Dacodelaac.DataType;
using UnityEngine;

namespace Dacodelaac.Events
{
    [CreateAssetMenu(menuName = "Event/Fly Coin Event")]
    public class FlyCoinEvent : BaseEvent<FlyCoinData>
    {
    }

    public class FlyCoinData
    {
        public Vector2 Position;
        public int Count = -1;
        public bool Animated = false;
        public System.Action OnCompleted;
    }
}