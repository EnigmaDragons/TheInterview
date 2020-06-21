using System;
using UnityEngine;

public class GrowingFire : MonoBehaviour
{
    [SerializeField] private GameObject[] vfx;
    [SerializeField] private TransformingParticleSystem transformingParticleSystem;
    [SerializeField] private float endSize;
    [SerializeField] private float secondsToFinish;
    [SerializeField] private BoxCollider collider;

    private float _t;
    private bool _transforming;
    private bool _doneTransforming;

    public void StartBurning()
    {
        vfx.ForEach(x => x.SetActive(true));
        transformingParticleSystem.enabled = true;
        _transforming = true;
    }

    private void Update()
    {
        if (_doneTransforming || !_transforming)
            return;
        _t = Math.Min(1, _t + Time.deltaTime / secondsToFinish);
        var multiplier = _t * endSize;
        collider.size = new Vector3(1 * multiplier, collider.size.y, 1 * multiplier);
        if (_t == 1)
            _doneTransforming = true;
    }
}