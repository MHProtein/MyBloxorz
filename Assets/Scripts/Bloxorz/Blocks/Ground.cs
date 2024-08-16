using System.Collections.Generic;
using Bloxorz.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bloxorz.Blocks
{
    public class Ground : MonoBehaviour
    {
        [FormerlySerializedAs("blocks")] public List<Block> blocks;
        public static Ground instance;
        public Vector3Int terminalPos;
        private void Awake()
        {
            blocks = new List<Block>();
            blocks.AddRange(GetComponentsInChildren<Block>());
            instance = this;
        }

        public bool TestOnGround(CubeData cubeData)
        {
            int counter = 0;
            foreach (var block in blocks)
            {
                counter += block.TestOnBrick(cubeData);
                if (counter == 2)
                    return true;
            }
            return false;
        }
        
        public bool PlayerOnGround(CubeData cubeData)
        {
            int counter = 0;
            foreach (var brick in blocks)
            {
                counter += brick.OnBlock(cubeData);
                if (counter == 2)
                    return true;
            }
            return false;
        }
    
    }
}
