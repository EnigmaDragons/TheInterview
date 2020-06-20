using UnityEngine;

public class RandomlyStartFire : OnMessage<StartFire>
{
    [SerializeField] private Flammable[] flammables;

    protected override void Execute(StartFire msg)
    {
        Rng.Random(flammables).Burn();
    }
}