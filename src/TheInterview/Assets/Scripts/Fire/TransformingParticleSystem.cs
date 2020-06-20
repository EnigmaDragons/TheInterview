using System;
using UnityEngine;

public class TransformingParticleSystem : MonoBehaviour
{
    [SerializeField] private float sizeMuliplyFactor;
    [SerializeField] private float secondsToFinish;
    [SerializeField] private ParticleSystem[] particleSystems;

    private ParticleSystem.MainModule[] _particleMainModules;
    private ParticleSystem.MinMaxCurve[] _startSizes;
    private float _t;
    private bool _doneTransforming;

    private void Awake()
    {
        _particleMainModules = new ParticleSystem.MainModule[particleSystems.Length];
        _startSizes = new ParticleSystem.MinMaxCurve[particleSystems.Length];
        for (var i = 0; i < particleSystems.Length; i++)
        {
            _particleMainModules[i] = particleSystems[i].main;
            _startSizes[i] = particleSystems[i].main.startSize;
        }
    }

    public void Update()
    {
        if (_doneTransforming)
            return;
        _t = Math.Min(1, _t + Time.deltaTime / secondsToFinish);
        var multiplier = 1 + (_t * sizeMuliplyFactor);
        for (var i = 0; i < _particleMainModules.Length; i++)
        {
            var s = _startSizes[i];
            _particleMainModules[i].startSize = s.mode == ParticleSystemCurveMode.Constant 
                ? new ParticleSystem.MinMaxCurve(s.constant * multiplier)
                : new ParticleSystem.MinMaxCurve(s.constantMin * multiplier, s.constantMax * multiplier);
        }
        if (_t == 1)
            _doneTransforming = true;
    }
}