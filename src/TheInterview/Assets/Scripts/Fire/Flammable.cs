﻿using UnityEngine;
using UnityEngine.Events;

public class Flammable : MonoBehaviour
{
    [SerializeField] private UnityAction onBurn;

    public bool IsBurning;

    public void Burn()
    {
        if (IsBurning)
            return;
        IsBurning = true; 
        onBurn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsBurning)
            return;
        var flammableComponent = other.GetComponent<Flammable>();
        if (flammableComponent != null)
            flammableComponent.Burn();
    }
}