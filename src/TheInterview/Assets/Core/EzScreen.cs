using UnityEngine;

public class EzScreen : MonoBehaviour
{
    private static int _counter;
    
    [SerializeField] private string filename;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            var n = $"{filename}_{_counter++}.png";
            ScreenCapture.CaptureScreenshot(n);
            Debug.Log($"Captured screenshot: {n}");
        }
    }
}