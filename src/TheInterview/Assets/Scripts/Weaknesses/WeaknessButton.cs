using UnityEngine;

public class WeaknessButton : OnMessage<WeaknessSelected, Finished<WeaknessSelected>>
{
    [SerializeField] private Speech speech;
    [SerializeField] private CurrentGameState gameState;

    private bool _isWeaknessPlaying;

    public void Click()
    {
        if (_isWeaknessPlaying)
            return;
        Message.Publish(new WeaknessSelected());
        speech.Play();
    }

    protected override void Execute(WeaknessSelected msg)
    {
        _isWeaknessPlaying = true;
    }

    protected override void Execute(Finished<WeaknessSelected> msg)
    {
        _isWeaknessPlaying = false;
    }
}