
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HudImageOpacityFactor : OnMessage<GameStateChanged>
{
    [SerializeField] private float focusedOpacity = 255 * 0.6f;
    [SerializeField] private float unfocusedOpacity = 0;

    private Image _image;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
        UpdateColor(false);
    }

    private void UpdateColor(bool isFocused) 
        => _image.color = ColorWithAlpha(isFocused ? focusedOpacity : unfocusedOpacity);

    protected override void Execute(GameStateChanged msg) 
        => UpdateColor(msg.State.HudIsFocused);

    private Color ColorWithAlpha(float alpha) 
        => new Color(_image.color.r, _image.color.g, _image.color.b, alpha / 255f);
}
