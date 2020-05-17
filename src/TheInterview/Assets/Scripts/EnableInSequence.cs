using System.Collections;
using UnityEngine;

public class EnableInSequence : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    [SerializeField] private float[] delay;
    
    private void Start() => StartCoroutine(Execute());

    private IEnumerator Execute()
    {
        for (var i = 0; i < objects.Length; i++)
        {
            yield return new WaitForSeconds(delay[i]);
            objects[i].SetActive(true);
        }
    }
}
