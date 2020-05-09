using UnityEngine;

public static class FloorSizing
{
    private static readonly int StartingFloorNumber = 3;
    private static readonly int HeightPerFloor = 6;
    
    public static int FloorNumber(this Transform t) 
        => Mathf.FloorToInt(t.position.y / HeightPerFloor + StartingFloorNumber);
}
