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
    [SerializeField] private int defaultChoiceIndex = 0;
    [SerializeField] private string[] choices;

    private IndexSelector<string> _choices;

    private void Awake()
    {
        if (label != null)
            label.text = fieldType + ":";
        _choices = new IndexSelector<string>(choices, defaultChoiceIndex);
        left.onClick.AddListener(() => UpdateAfter(() => _choices.MovePrevious()));
        right.onClick.AddListener(() => UpdateAfter(() => _choices.MoveNext()));
        UpdateLabel();
    }

    private void UpdateAfter(Action a)
    {
        Debug.Log("Clicked Button");
        a();
        UpdateLabel();
        Debug.Log($"New choice is {_choices.Current}");
    }
    
    private void UpdateLabel() => choiceLabel.text = _choices.Current;
}
