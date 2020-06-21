using TMPro;
using UnityEngine;

public sealed class EmailApp : HudAppView
{
    [SerializeField] private TextMeshProUGUI senderLabel;
    [SerializeField] private TextMeshProUGUI recipientLabel;
    [SerializeField] private TextMeshProUGUI subjectLabel;
    [SerializeField] private TextMeshProUGUI bodyText;

    public void Init(Email e)
    {
        senderLabel.text = e.Sender;
        recipientLabel.text = e.Recipient;
        subjectLabel.text = e.Subject;
        bodyText.text = e.Body;
    }
}
