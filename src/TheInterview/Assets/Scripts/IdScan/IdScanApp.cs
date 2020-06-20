using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IdScanApp : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private TextMeshProUGUI locationLabel;
    [SerializeField] private UiSfxPlayer sfx;
    [SerializeField] private AudioClip scanSuccessSound;
    [SerializeField] private AudioClip scanFailureSound;
    [SerializeField] private Button scanButton;

    private IdAccessRequirement _req;

    private void Awake() => scanButton.onClick.AddListener(Scan);

    public void Init(IdAccessRequirement r)
    {
        _req = r;
        locationLabel.text = r.Location;
    }

    private void Scan()
    {
        var scanSucceeded = gameState.ReadOnly.InventoryItems.Contains(_req.RequiredId.Name);
        if (scanSucceeded)
        {
            sfx.Play(scanSuccessSound);
            StartCoroutine(ExecuteAfterDelay(1f, 
                () => gameState.UpdateState(gs =>
                {
                    gs.TransientTriggers.Add(_req.AccessTarget.Value);
                    gs.HudIsFocused = false;
                })));
        }
        else
        {
            sfx.Play(scanFailureSound);
        }
    }
    
    private IEnumerator ExecuteAfterDelay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}
