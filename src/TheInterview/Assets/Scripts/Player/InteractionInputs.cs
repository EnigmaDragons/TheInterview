using UnityEngine;

public static class InteractionInputs
{
    public static bool IsPlayerSignallingInteraction() => Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0);
}
