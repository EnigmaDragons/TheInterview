using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppInstallUI : OnMessage<AppInstallRefused, Finished<AppInstallRefused>>
{
    [SerializeField] private RectTransform layoutGroup;
    [SerializeField] private Button1 installButton;
    [SerializeField] private TextMeshProUGUI installText;
    [SerializeField] private Button2 refuseButton;
    [SerializeField] private Text refuseText;
    [SerializeField] private float scaleSecondsTransition;
    [SerializeField] private float scaleGrowMultiplier;
    [SerializeField] private float scaleShrinkMultiplier;

    private Vector3 _installScale;
    private Vector3 _refuseScale;
    private Button.ButtonClickedEvent _installOnClick;
    private Button.ButtonClickedEvent _refuseOnClick;

    private int _timesRefused;
    private bool _changingSize;
    private float _t;

    private void Update()
    {
        if (_changingSize && _timesRefused == 1)
        {
            _t = Math.Min(1, _t + Time.deltaTime / scaleSecondsTransition);
            installButton.transform.localScale = _installScale * (1 + scaleGrowMultiplier * _t);
            refuseButton.transform.localScale = _refuseScale * (1 - scaleShrinkMultiplier * _t);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup);
            if (_t == 1)
                _changingSize = false;
        }
        else if (_changingSize && _timesRefused == 2)
        {
            _t = Math.Max(0, _t - Time.deltaTime / scaleSecondsTransition);
            installButton.transform.localScale = _installScale * (1 + scaleGrowMultiplier * _t);
            refuseButton.transform.localScale = _refuseScale * (1 - scaleShrinkMultiplier * _t);
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup);
            if (_t == 0)
                _changingSize = false;
        }
    }

    protected override void Execute(AppInstallRefused msg)
    {
        installButton.Button.interactable = false;
        refuseButton.Button.interactable = false;
        _timesRefused++;
        if (_timesRefused == 1)
        {
            _installScale = installButton.transform.localScale;
            _refuseScale = refuseButton.transform.localScale;
            _changingSize = true;
            _t = 0;
        }
        else if (_timesRefused == 2)
        {
            installButton.SetToRed();
            refuseButton.SetToBlue();
            _changingSize = true;
            _t = 1;
        }
        else if (_timesRefused == 3)
        {
            installButton.SetToBlue();
            refuseButton.SetToRed();
            _installOnClick = installButton.Button.onClick;
            _refuseOnClick = refuseButton.Button.onClick;
            installButton.Button.onClick = _refuseOnClick;
            refuseButton.Button.onClick = _installOnClick;
            installText.text = "Refuse";
            refuseText.text = "Install";
        }
    }

    protected override void Execute(Finished<AppInstallRefused> msg)
    {
        installButton.Button.interactable = true;
        refuseButton.Button.interactable = true;
    }
}