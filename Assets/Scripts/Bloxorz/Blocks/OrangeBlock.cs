using System;
using Bloxorz.Player;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Bloxorz.Blocks
{
    [ExecuteAlways]
    public class OrangeBlock : Block
    {
        private Rigidbody rigidbody;
        private bool fall;
        private float fallingTimer;

        public override int OnBlock(CubeData cubeData)
        {
            int cnt = base.OnBlock(cubeData);
            if (cnt == 2)
            {
                Cube.instance.isFalling = true;
                GameManager.instance.ChangeGameState(GameState.DEAD);
                fall = true;
            }
            return cnt;
        }

        public override int TestOnBrick(CubeData cubeData)
        {
            int cnt = base.OnBlock(cubeData);
            return cnt == 2 ? 0 : cnt;
        }

        protected override void Update()
        {
            base.Update();

            if (GameManager.instance && GameManager.instance.State == GameState.AUTOSOLVE)
                return;
            if (fall)
            {
                if (fallingTimer < 5.0f)
                {
                    transform.Translate(0.0f, -(fallingTimer / 5.0f) * 20.0f, 0.0f);
                }
                fallingTimer += Time.deltaTime;
            }
        }

    }
 
}

