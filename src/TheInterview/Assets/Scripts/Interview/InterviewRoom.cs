using System;
using TMPro;
using UnityEngine;

public class InterviewRoom : OnMessage<PresentNextQuestion, PresentAnswers>
{
    [SerializeField] private GameObject screen1;
    [SerializeField] private TextMeshPro screen1Text;
    [SerializeField] private GameObject screen2;
    [SerializeField] private TextMeshPro screen2Text;
    [SerializeField] private GameObject screen3;
    [SerializeField] private TextMeshPro screen3Text;
    [SerializeField] private InterviewQuestion[] questions;
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private Speech rejected;
    [SerializeField] private Speech hired;

    private int _index;

    public void ChooseOption(int i)
    {
        screen1.SetActive(false);
        screen2.SetActive(false);
        screen3.SetActive(false);
        if (i == 1)
            questions[_index].option1.Play();
        else if (i == 2)
            questions[_index].option2.Play();
        else if (i == 3)
            questions[_index].option3.Play();
        _index++;
    }

    protected override void Execute(PresentNextQuestion msg)
    {
        if (_index >= questions.Length)
        {
            if (gameState.ShouldBeHired)
                hired.Play();
            else
                rejected.Play();
        }
        else
            questions[_index].speech.Play();
    }

    protected override void Execute(PresentAnswers msg)
    {
        screen1Text.text = questions[_index].option1Text;
        screen1.SetActive(true);
        screen2Text.text = questions[_index].option2Text;
        screen2.SetActive(true);
        screen3Text.text = questions[_index].option3Text;
        screen3.SetActive(true);
    }
}

[Serializable]
public class InterviewQuestion
{
    public Speech speech;
    public string option1Text;
    public Speech option1;
    public string option2Text;
    public Speech option2;
    public string option3Text;
    public Speech option3;
}