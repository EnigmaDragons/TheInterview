using UnityEngine;

[CreateAssetMenu]
public class AppQuestInfo : ScriptableObject
{
    [SerializeField] private string description;
    [SerializeField] private int currencyReward;
    [SerializeField] private int xpReward;

    public string Description => description;
    public int CurrencyReward => currencyReward;
    public int XpReward => xpReward;
}
