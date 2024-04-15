using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SwitchData : BrickData
{
    public bool active = false;
    public List<RemovableBrickData> bricks;
    public SwitchData(Vector3 pos) : base(pos)
    {
        bricks = new List<RemovableBrickData>();
    }

    public SwitchData(SwitchData data) : base(data)
    {
        bricks = new List<RemovableBrickData>(data.bricks);
    }
    
}