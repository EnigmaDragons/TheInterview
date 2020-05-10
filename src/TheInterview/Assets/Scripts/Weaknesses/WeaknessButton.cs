using UnityEngine;

public class WeaknessButton : OnMessage<WeaknessSelected, Finished<WeaknessSelected>>
{
    [SerializeField] private Speech speech;
    [SerializeField] private CurrentGameState gameState;

    private bool _isWeaknessPlaying;
    private bool _isDisabled;

    public void Click()
    {
        if (_isWeaknessPlaying || _isDisabled)
            return;
        Message.Publish(new WeaknessSelected { Name = speech.name });
        speech.Play();
    }

    protected override void Execute(WeaknessSelected msg)
    {
        _isWeaknessPlaying = true;
        if (msg.Name == speech.name)
            _isDisabled = true;
    }

    protected override void Execute(Finished<WeaknessSelected> msg)
    {
        _isWeaknessPlaying = false;
    }
}