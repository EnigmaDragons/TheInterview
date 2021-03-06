﻿using System;
using UnityEngine;

public class PlayerInteraction : OnMessage<GameStateChanged>
{
    [SerializeField] private Camera eyes;
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private bool checkBeforePlayerTriesToInteract = true;

    private Transform _eyesTransform;
    
    private readonly RaycastHit[] _raycastHits = new RaycastHit[50];

    private bool _interactionSuspended;
    private bool _canInteract;
    private string _interactObjectName = "";
    private bool _interactChanged;
    private Action _interact = () => { };

    private void Awake() => _eyesTransform = eyes.transform;

    private void UpdatePossibleInteractions()
    {
        var canInteract = false; 
        _interact = () => { };
        _interactObjectName = "";
        var numHits = Physics.RaycastNonAlloc(_eyesTransform.position, _eyesTransform.forward, _raycastHits, maxDistance: interactRange);
        for (var i = 0; i < numHits; i++)
        {
            var hit = _raycastHits[i];
            var hitObj = hit.collider.gameObject;
            if (!hitObj.CompareTag("RaycastInteract"))
                continue;
            
            if (hit.distance <= interactRange)
            {
                var interactTrigger = hitObj.GetComponent<Trigger>();
                if (interactTrigger != null)
                { 
                    canInteract = interactTrigger.CanTrigger();
                    _interactChanged = _interactObjectName.Equals(hitObj.name);
                    _interactObjectName = hitObj.name;
                    _interact = () => interactTrigger.Execute();
                }

                var interactAction = hitObj.GetComponent<InteractAction>();
                if (interactAction != null && interactAction.enabled)
                {
                    canInteract = true;
                    _interactChanged = _interactObjectName.Equals(hitObj.name);
                    _interactObjectName = hitObj.name;
                    _interact = () => interactAction.Execute();
                }
            }
        }
        if (!string.IsNullOrWhiteSpace(_interactObjectName) && _interactChanged)
            Debug.Log($"Player- Can interact with {_interactObjectName}");
        SetInteractState(canInteract);
        _interactChanged = false;
    }
    
    private void Update()
    {
        if (_interactionSuspended)
            return;
        
        var shouldExecute = InteractionInputs.IsPlayerSignallingInteraction();
        
        if (shouldExecute)
            UpdatePossibleInteractions();
        else if (checkBeforePlayerTriesToInteract && Time.frameCount % 6 == 0)
            UpdatePossibleInteractions();
        
        if (shouldExecute) 
            Interact();
    }

    public void Interact()
    {
        if (!_canInteract)
            return;
        
        Debug.Log($"Player - Triggered Interaction on Object {_interactObjectName}");
        _interact();
    }

    private void SetInteractState(bool canInteract)
    {
        if (canInteract == _canInteract)
            return;

        _canInteract = canInteract;
        Message.Publish(new InteractionsPossible { Any = canInteract });
    }

    protected override void Execute(GameStateChanged msg) => _interactionSuspended = msg.State.HudIsFocused;
}
