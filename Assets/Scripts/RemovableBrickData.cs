using UnityEngine;

public class RemovableBrickData : BrickData
{
    public bool activited;
    public RemovableBrickData(Vector3 pos) : base(pos)
    {
        activited = false;
    }
    
    public RemovableBrickData(RemovableBrickData data) : base(data)
    {
        activited = data.activited;
    }
    
    public void SetActive(bool active)
    {
        activited = active;
    }
    
}