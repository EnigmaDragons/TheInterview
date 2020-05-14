using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class Page : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;

    public void Init(Action prev, Action next)
    {
        if (previousButton != null)
            previousButton.onClick.AddListener(() => prev());
        if (nextButton != null)
            nextButton.onClick.AddListener(() => next());
    }
}
