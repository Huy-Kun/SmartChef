using Dacodelaac.Core;
using UnityEngine;

namespace Dacodelaac.Common
{
    public class Rotator : Core.BaseMono
    {
        [SerializeField] public float speed;
        [SerializeField] Vector3 axis;
        [SerializeField] Space space = Space.Self;

        public override void Tick()
        {
            transform.Rotate(axis, speed * Time.deltaTime, space);
        }
    }
}