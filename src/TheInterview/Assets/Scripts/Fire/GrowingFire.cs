using System;
using UnityEngine;

public class GrowingFire : MonoBehaviour
{
    [SerializeField] private GameObject[] vfx;
    [SerializeField] private TransformingParticleSystem transformingParticleSystem;
    [SerializeField] private float sizeMuliplyFactor;
    [SerializeField] private float secondsToFinish;

    private float _t;
    private bool _doneTransforming;

    public void StartBurning()
    {
        vfx.ForEach(x => x.SetActive(true));
        transformingParticleSystem.enabled = true;
    }

    private void Update()
    {
        if (_doneTransforming)
            return;
        _t = Math.Min(1, _t + Time.deltaTime / secondsToFinish);
        var multiplier = 1 + (_t * sizeMuliplyFactor);
        transform.localScale = Vector3.one * multiplier;
        if (_t == 1)
            _doneTransforming = true;
    }
}