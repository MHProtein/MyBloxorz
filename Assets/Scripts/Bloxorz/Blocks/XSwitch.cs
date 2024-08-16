using System.Collections.Generic;
using Bloxorz.Player;

namespace Bloxorz.Blocks
{
    public class XSwitch : Block
    {
        public SwitchData data1;
        public List<RemovableBlock> bricks;

        protected override void Awake()
        {
            base.Awake();
            data = new SwitchData(transform.position);
            data1 = (SwitchData)data;
        }

        public override BlockData GetData()
        {
            return data1;
        }

        public override void SetData(BlockData data)
        {
            this.data = data;
            data1 = (SwitchData)data;
        }

        public override int OnBlock(CubeData cubeData)
        {
            int cnt = base.OnBlock(cubeData);
            if (cnt == 2)
            {
                foreach (var brick in bricks)
                {
                    brick.Trigger();
                }
            }

            return cnt;
        }
    }
}