using System.Collections.Generic;
using Bloxorz.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bloxorz.Blocks
{
    public class OSwitch : Block
    {
        [FormerlySerializedAs("data1")] public SwitchData dataRef;
        [FormerlySerializedAs("bricks")] public List<RemovableBlock> blocks;
        protected override void Awake()
        {
            base.Awake();
            data = new SwitchData(transform.position);
            dataRef = (SwitchData)data;
        }

        public override BlockData GetData()
        {
            return dataRef;
        }
        public override void SetData(BlockData newData)
        {
            this.data = newData;
            dataRef = (SwitchData)newData;
        }
        public override int OnBlock(CubeData cubeData)
        {
            int cnt = base.OnBlock(cubeData);
            if (cnt > 0)
            {
                foreach (var block in blocks)
                {
                    block.Trigger();
                }
            }
            return cnt;
        }
    }

}

