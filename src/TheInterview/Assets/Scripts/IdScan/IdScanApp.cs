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
    [SerializeField] private GameObject scanButtonParent;
    [SerializeField] private Button scanButton;

    private IdAccessRequirement _req;

    private void Awake() => scanButton.onClick.AddListener(Scan);

    public void Init(IdAccessRequirement r)
    {
        _req = r;
        locationLabel.text = r.Location;
        scanButtonParent.SetActive(true);
    }

    private void Scan()
    {
        var scanSucceeded = gameState.ReadOnly.InventoryItems.Contains(_req.RequiredId.Name);
        if (scanSucceeded)
        {
            sfx.Play(scanSuccessSound);
            locationLabel.text = "Access Granted";
            scanButtonParent.SetActive(false);
            gameState.UpdateState(gs => gs.TransientTriggers.Add(_req.AccessTarget.Value));
            Message.Publish(new RemoveItem(_req.RequiredId.Name));
            StartCoroutine(ExecuteAfterDelay(1f, () => gameState.UpdateState(gs => gs.HudIsFocused = false)));
        }
        else
        {
            sfx.Play(scanFailureSound);
            locationLabel.text = "Missing Id";
        }
    }
    
    private IEnumerator ExecuteAfterDelay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}
