using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private GameObject _objectiveUI;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI creds;
    [SerializeField] private TextMeshProUGUI xp;
    [SerializeField] private float fadeSeconds;

    private bool _shouldFade;

    private void OnEnable()
    {
        Message.Subscribe<ObjectiveGained>(_ => ObjectiveGained(), this);
        Message.Subscribe<ObjectiveFailed>(_ => ObjectiveFailed(), this);
        Message.Subscribe<ObjectiveSucceeded>(_ => ObjectiveSucceeded(), this);
        Message.Subscribe<SubObjectiveFailed>(_ => SubObjectiveFailed(), this);
        Message.Subscribe<SubObjectiveSucceeded>(_ => SubObjectiveSucceeded(), this);
    }

    private void OnDisable() => Message.Unsubscribe(this);

    private void ObjectiveGained()
    {
        _shouldFade = false;
        _objectiveUI.SetActive(true);
        OnObjectiveChanged();
    }

    private void ObjectiveFailed()
    {
        OnObjectiveChanged();
        StartCoroutine(FadeOut());
    }

    private void ObjectiveSucceeded()
    {
        OnObjectiveChanged();
        StartCoroutine(FadeOut());
    }

    private void SubObjectiveFailed()
    {
        OnObjectiveChanged();
    }

    private void SubObjectiveSucceeded()
    {
        OnObjectiveChanged();
    }

    private void OnObjectiveChanged()
    {
        if (gameState.Objective.IsMissing)
            return;

        var objectiveDescription = $"<b>{gameState.Objective.Value.Objective.Description}</b>";
        if (gameState.Objective.Value.Status == ObjectiveStatus.Failed)
            objectiveDescription = $"<s><color=\"red\">{objectiveDescription}</color></s>";
        if (gameState.Objective.Value.Status == ObjectiveStatus.Succeeded)
            objectiveDescription = $"<s><color=\"green\">{objectiveDescription}</color></s>";
        description.text = string.Join("\n\n", new string [] { objectiveDescription }
            .Concat(gameState.Objective.Value.SubObjectives
                .Where(x => !x.SubObjective.Hidden || x.Status != ObjectiveStatus.Uncompleted)
                .Select(x =>
                {
                    if (x.Status == ObjectiveStatus.Failed)
                        return $" - <s><color=\"red\">{x.SubObjective.Description}</color></s>";
                    else if (x.Status == ObjectiveStatus.Succeeded)
                        return $" - <s><color=\"green\">{x.SubObjective.Description}</color></s>";
                    else
                        return $" - {x.SubObjective.Description}";

                })));
        creds.text = (gameState.Objective.Value.Objective.Creds 
            + gameState.Objective.Value.SubObjectives.Where(x => x.Status == ObjectiveStatus.Failed).Sum(x => x.SubObjective.PenaltyCreds) 
            + gameState.Objective.Value.SubObjectives.Where(x => x.Status == ObjectiveStatus.Succeeded).Sum(x => x.SubObjective.RewardCreds)).ToString();
        xp.text = (gameState.Objective.Value.Objective.XP
            + gameState.Objective.Value.SubObjectives.Where(x => x.Status == ObjectiveStatus.Failed).Sum(x => x.SubObjective.PenaltyXP)
            + gameState.Objective.Value.SubObjectives.Where(x => x.Status == ObjectiveStatus.Succeeded).Sum(x => x.SubObjective.RewardXP)).ToString();
    }

    private IEnumerator FadeOut()
    {
        _shouldFade = true;
        yield return new WaitForSeconds(fadeSeconds);
        _objectiveUI.SetActive(true);
    }
}