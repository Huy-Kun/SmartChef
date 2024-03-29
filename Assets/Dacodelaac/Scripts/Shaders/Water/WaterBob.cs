using Dacodelaac.Core;
using UnityEngine;

public class WaterBob : BaseMono
{
    [SerializeField] float height = 0.1f;
    [SerializeField] float period = 1;

    Vector3 initialPosition;
    float offset;

    public override void Initialize()
    {
        initialPosition = transform.position;

        offset = 1 - (Random.value * 2);
    }

    public override void Tick()
    {
        transform.position = initialPosition - Vector3.up * Mathf.Sin((Time.time + offset) * period) * height;
    }
}
