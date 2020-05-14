using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceField : MonoBehaviour
{
    [SerializeField] private string fieldType;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Button left;
    [SerializeField] private Button right;
    [SerializeField] private TextMeshProUGUI choiceLabel;
    [SerializeField] private bool useRandomStartingIndex = false;
    [SerializeField] private int defaultChoiceIndex = 0;
    [SerializeField] private string[] choices;

    private IndexSelector<string> _choices;

    private void Awake()
    {
        if (label != null)
            label.text = fieldType + ":";
        var startingIndex = useRandomStartingIndex ? Rng.Int(0, choices.Length - 1) : defaultChoiceIndex;
        _choices = new IndexSelector<string>(choices, startingIndex);
        left.onClick.AddListener(() => UpdateAfter(() => _choices.MovePrevious()));
        right.onClick.AddListener(() => UpdateAfter(() => _choices.MoveNext()));
        UpdateLabel();
    }

    private void UpdateAfter(Action a)
    {
        a();
        UpdateLabel();
        Message.Publish(new SelectedResumeChoice { Field = fieldType, Value = _choices.Current });
    }
    
    private void UpdateLabel() => choiceLabel.text = _choices.Current;
}
