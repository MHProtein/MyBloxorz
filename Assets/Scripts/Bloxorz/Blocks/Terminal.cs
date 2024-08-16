using Bloxorz.Player;
using UnityEngine;

namespace Bloxorz.Blocks
{
    [ExecuteAlways]
    public class Terminal : Block
    {

        private void Start()
        {
            Ground.instance.terminalPos = data.intPosition;
        }

        public override BlockData GetData()
        {
            return data;
        }
        public override void SetData(BlockData data)
        {
            this.data = data;
        }
        public override int OnBlock(CubeData cubeData)
        {
            int cnt = base.OnBlock(cubeData);
            if (cnt == 2)
            {
                if (GameManager.instance.State == GameState.AUTOSOLVE)
                    return cnt;
                GameManager.instance.ChangeGameState(GameState.WIN);
            }
            return cnt;
        }
    }
}