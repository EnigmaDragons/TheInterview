using UnityEngine;

public class OnHideOrbs : OnMessage<HideOrbs>
{
    [SerializeField] private Vector3 destination;
    [SerializeField] private float speed;

    private bool _isHiding;

    protected override void Execute(HideOrbs msg)
    {
        _isHiding = true;
    }

    private void Update()
    {
        if (!_isHiding)
            return;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, speed * Time.deltaTime);
        if (transform.localPosition == destination)
            _isHiding = false;
    }
}