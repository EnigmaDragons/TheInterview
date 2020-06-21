using System;
using UnityEngine;

public class TransformingParticleSystem : MonoBehaviour
{
    [SerializeField] private float endingSize;
    [SerializeField] private float secondsToFinish;
    [SerializeField] private ParticleSystem[] particleSystems;

    private ParticleSystem.ShapeModule[] _particleShapeModules;
    private ParticleSystem.EmissionModule[] _particleEmissionModules;
    private float[] _emissionCounts;
    private float _t;
    private bool _doneTransforming;

    private void Awake()
    {
        _particleShapeModules = new ParticleSystem.ShapeModule[particleSystems.Length];
        _particleEmissionModules = new ParticleSystem.EmissionModule[particleSystems.Length];
        _emissionCounts = new float[particleSystems.Length];
        for (var i = 0; i < particleSystems.Length; i++)
        {
            _particleShapeModules[i] = particleSystems[i].shape;
            _particleEmissionModules[i] = particleSystems[i].emission;
            _emissionCounts[i] = particleSystems[i].emission.rateOverTime.constant;
        }
    }

    public void Update()
    {
        if (_doneTransforming)
            return;
        _t = Math.Min(1, _t + Time.deltaTime / secondsToFinish);
        var size = (_t * endingSize);
        for (var i = 0; i < particleSystems.Length; i++)
        {
            _particleShapeModules[i].scale = new Vector3(size, 0.1f, size);
            _particleEmissionModules[i].rateOverTime = new ParticleSystem.MinMaxCurve(_emissionCounts[i] * size * size);
        }
        if (_t == 1)
            _doneTransforming = true;
    }
}