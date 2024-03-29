using Dacodelaac.Core;
using Dacodelaac.Utils;
using UnityEngine;

namespace Dacodelaac.Common
{
    public class DelayDespawn : BaseMono
    {
        [SerializeField] float delay;
        public override void DoEnable()
        {
            base.DoEnable();
            this.Delay(delay, false, () =>
            {
                if (gameObject.activeInHierarchy)
                {
                    pools.Despawn(gameObject);
                }
            });
        }
    }
}