using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Objective : ScriptableObject
{
    public string Description;
    public List<SubObjective> SubObjectives;
    public int Creds;
    public int XP;
}