using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class HudCanvasGroupOpacityFactor : OnMessage<GameStateChanged>
{
    [SerializeField] private float focusedOpacity = 1f;
    [SerializeField] private float unfocusedOpacity = 0f;

    private CanvasGroup _group;
    
    private void Awake()
    {
        _group = GetComponent<CanvasGroup>();
        UpdateOpacity(false);
    }

    private void UpdateOpacity(bool isFocused) 
        => _group.alpha = isFocused ? focusedOpacity : unfocusedOpacity;

    protected override void Execute(GameStateChanged msg) 
        => UpdateOpacity(msg.State.HudIsFocused);
}
