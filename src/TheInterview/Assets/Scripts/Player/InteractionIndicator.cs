using System;
using UnityEngine;

public sealed class InteractionIndicator : OnMessage<InteractionsPossible>
{
    [SerializeField] private GameObject indicator;

    private void Awake() => indicator.SetActive(false);

    protected override void Execute(InteractionsPossible msg) => indicator.SetActive(msg.Any);
}
