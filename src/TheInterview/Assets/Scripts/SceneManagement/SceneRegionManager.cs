using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneRegionManager : OnMessage<DisableRegion, EnableRegion>
{
    [SerializeField] private SceneArea[] areas;

    private Dictionary<string, GameObject> _regions;

    private void Awake() => _regions = areas.ToDictionary(a => a.RegionName, a => a.Object);
    
    protected override void Execute(DisableRegion msg)
    {
        if (_regions.TryGetValue(msg.RegionName, out var area))
            area.SetActive(false);
        else
            Debug.LogWarning($"Scene Regions - Unknown Region {msg.RegionName}");
    }

    protected override void Execute(EnableRegion msg)
    {
        if (_regions.TryGetValue(msg.RegionName, out var area))
            area.SetActive(true);
        else
            Debug.LogWarning($"Scene Regions - Unknown Region {msg.RegionName}");
    }
}
