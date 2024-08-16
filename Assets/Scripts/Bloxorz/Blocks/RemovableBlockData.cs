using UnityEngine;

namespace Bloxorz.Blocks
{
    public class RemovableBlockData : BlockData
    {
        public bool activited;
        public RemovableBlockData(Vector3 pos) : base(pos)
        {
            activited = false;
        }
    
        public RemovableBlockData(RemovableBlockData data) : base(data)
        {
            activited = data.activited;
        }
    
        public void SetActive(bool active)
        {
            activited = active;
        }
    
    }
}

