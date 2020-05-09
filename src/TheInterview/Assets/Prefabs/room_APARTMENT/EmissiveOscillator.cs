using UnityEngine;
using System.Collections.Generic;

public sealed class EmissiveOscillator : MonoBehaviour 
{
   	
     [SerializeField] public float frequency ;
     [SerializeField] public float amplitude ;
     [SerializeField] public Material material;
     [SerializeField] public Color emissionColor;
	 private float glow;
 

     void Start () 
     {
		 emissionColor = material.GetColor("_EmissionColor");
     }
     

     void Update () 
     {
         glow = (2 + Mathf.Cos(Time.time * frequency)) * amplitude;
         material.SetColor("_EmissionColor", emissionColor * glow);
     }
}
