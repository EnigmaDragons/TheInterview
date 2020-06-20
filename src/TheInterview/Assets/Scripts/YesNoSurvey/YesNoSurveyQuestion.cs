using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class YesNoSurveyQuestion : ScriptableObject
{
    public string Prompt;
    public UnityEvent OnYes;
    public UnityEvent OnNo;
}
