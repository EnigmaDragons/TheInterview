using UnityEngine;

[CreateAssetMenu(menuName = "OnlyOnce/AllEndings")]
public class AllEndings : ScriptableObject
{
    [SerializeField] private Ending[] Endings;

    public int Count => Endings.Length;
}
