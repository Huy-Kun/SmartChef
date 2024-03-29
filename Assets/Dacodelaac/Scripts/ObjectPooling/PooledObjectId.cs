using Dacodelaac.DebugUtils;
using UnityEngine;

namespace Dacodelaac.ObjectPooling
{
    public class PooledObjectId : MonoBehaviour
    {
        public GameObject prefab;

// #if UNITY_EDITOR
//         void OnDestroy()
//         {
//             Dacoder.LogError("Destroying pooled object!", gameObject.name);
//         }
// #endif
    }
}