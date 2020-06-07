using UnityEngine;

[CreateAssetMenu]
public class SubObjective : ScriptableObject
{
    public string Description;
    public bool Hidden;
    public int RewardCreds;
    public int PenaltyCreds;
    public int RewardXP;
    public int PenaltyXP;
}