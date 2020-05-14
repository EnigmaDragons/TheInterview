using System;
using UnityEngine;

public sealed class ResumeUI : MonoBehaviour
{
    private void OnEnable() => Message.Publish(new ResumeActivated());
    private void OnDisable() => Message.Publish(new ResumeDeactivated());
}
