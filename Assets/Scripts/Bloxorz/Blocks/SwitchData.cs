using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bloxorz.Blocks
{
    [Serializable]
    public class SwitchData : BlockData
    {
        public List<RemovableBlockData> bricks;
        public SwitchData(Vector3 pos) : base(pos)
        {
            bricks = new List<RemovableBlockData>();
        }

        public SwitchData(SwitchData data) : base(data)
        {
            bricks = new List<RemovableBlockData>(data.bricks);
        }
    
    }
}

