using Dacodelaac.Core;
using UnityEngine;

namespace Dacodelaac.DebugUtils
{
    public class DebugCanvas : BaseMono
    {
        [SerializeField] GameObject[] editorOnly;
        
        public override void Initialize()
        {
#if UNITY_EDITOR
            DontDestroyOnLoad(gameObject);
#else
            DontDestroyOnLoad(gameObject);
            for (var i = 0; i < editorOnly.Length; i++)
            {
                Destroy(editorOnly[i].gameObject);
            }
#endif
        }
    }
}