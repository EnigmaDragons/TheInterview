using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneRegionManager : OnMessage<DisableRegion, EnableRegion>
{
    [SerializeField] private SceneArea[] areas;

    private Dictionary<SceneRegion, GameObject> _regions;

    private void Awake() => _regions = areas.ToDictionary(a => a.Region, a => a.Object);
    
    protected override void Execute(DisableRegion msg)
    {
        if (_regions.TryGetValue(msg.Region, out var area))
            area.SetActive(false);
    }

    protected override void Execute(EnableRegion msg)
    {
        if (_regions.TryGetValue(msg.Region, out var area))
            area.SetActive(true);
    }
}
