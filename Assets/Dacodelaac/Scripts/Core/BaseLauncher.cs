using System.Collections.Generic;
using UnityEngine;

namespace Dacodelaac.Core
{
    public class BaseLauncher : BaseMono
    {
        [SerializeField] GameObject[] prefabs;

        public override void Initialize()
        {
            base.Initialize();
            
            pools.Initialize();

            var list = new List<BaseMono>();
            // spawn, bind variables, listen to events
            foreach (var prefab in prefabs)
            {
                var monoes = Instantiate(prefab).GetComponentsInChildren<BaseMono>(true);
                
                list.AddRange(monoes);
            }
            // initialize
            foreach (var mono in list)
            {
                mono.Initialize();
            }
        }
    }
}