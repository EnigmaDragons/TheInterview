using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ComplexProgerssBar : MonoBehaviour
{
    [SerializeField] private Image[] parts;
    [SerializeField] [Range(0, 1)] private float fraction;
    [SerializeField] private bool whole;

    public float Fraction
    {
        get => fraction;
        set
        {
            fraction = value;
            UpdateParts();
        }
    }

    public Image[] Parts { get => parts; set => parts = value; }

    private void OnValidate()
    {
        UpdateParts();
    }

    private void UpdateParts()
    {
        if(Parts != null)
        {
            float indexDeltaFraction = 1f / Parts.Length;

            for(int i = 0; i < Parts.Length; i++)
            {
                float indexFraction = indexDeltaFraction * i;
                float nextIndexFraction = indexFraction + indexDeltaFraction;

                float localFraction = Mathf.InverseLerp(indexFraction, nextIndexFraction, fraction);

                if (whole)
                {
                    Parts[i].enabled = localFraction >= 0.5f;
                }
                else
                {
                    Parts[i].fillAmount = localFraction;
                }
            }
        }
    }
}