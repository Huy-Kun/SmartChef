using Dacodelaac.Core;
using Dacodelaac.Utils;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Dacodelaac.EditorUtils
{
    public class PrefabReplacer : BaseMono
    {
        [SerializeField] GameObject[] prefabs;

#if UNITY_EDITOR
        [ContextMenu("Replace")]
        public void Replace()
        {
            var gameObjects = Selection.gameObjects;
            foreach (var go in gameObjects)
            {
                var newGo = PrefabUtility.InstantiatePrefab(prefabs[Random.Range(0, prefabs.Length)], go.transform.parent) as GameObject;
                newGo.transform.Copy(go.transform, true, true, true, false);
                go.SetActive(false);
                EditorUtility.SetDirty(newGo);
                EditorUtility.SetDirty(go);
            }
        }
#endif
    }
}