using Dacodelaac.Core;
using UnityEngine;

namespace Dacodelaac.Common
{
    public class KeepFollow : BaseMono
    {
        [SerializeField] Transform target;
        [SerializeField] Vector3 offset;

        public override void Tick()
        {
            transform.position = target.position + offset;
        }
    }
}