
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera eyes;
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private float interactLongRange = 20f;

    private Transform eyesTransform;

    private RaycastHit[] raycastHits = new RaycastHit[50];

    private void Awake() => eyesTransform = eyes.transform;
    
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E))
            return;
        
        var numHits = Physics.RaycastNonAlloc(eyesTransform.position, eyesTransform.forward, raycastHits, maxDistance: interactRange);
        for (var i = 0; i < numHits; i++)
        {
            var hit = raycastHits[i];
            var hitObj = hit.collider.gameObject;
            if (hitObj.CompareTag("RaycastInteract"))
            {
                var interactTrigger = hitObj.GetComponent<InteractTrigger>();
                if (interactTrigger != null)
                {
                    Debug.Log($"Player- Triggered Interaction on Object {hitObj.name}");
                    interactTrigger.Execute();
                    return;
                }
            }
        }

        numHits = Physics.RaycastNonAlloc(eyesTransform.position, eyesTransform.forward, raycastHits, maxDistance: interactLongRange);
        for (var i = 0; i < numHits; i++)
        {
            var hit = raycastHits[i];
            var hitObj = hit.collider.gameObject;
            if (hitObj.CompareTag("RaycastInteract"))
            {
                var interactTrigger = hitObj.GetComponent<LongRangeInteractTrigger>();
                if (interactTrigger != null)
                {
                    Debug.Log($"Player- Triggered Interaction on Object {hitObj.name}");
                    interactTrigger.Execute();
                    return;
                }
            }
        }
    }
}
