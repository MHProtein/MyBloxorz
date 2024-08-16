
using System.Collections.Generic;
using Bloxorz.Player;
using Bloxorz.CommandControl;

namespace Bloxorz.LevelSolver
{
    public class Node
    {
        public CubeData cubeData;
        public Node parent;
        public MovementCommand command;
        public int heuristicValue;
        public int cost;
        public List<BlockData> data;
    
        public Node()
        {
            cost = 0;
            heuristicValue = 0;
            data = new List<BlockData>();
        }
    
        public Node(CubeData cubeData)
        {
            this.cubeData = cubeData;
            cost = 0;
            heuristicValue = 0;
            data = new List<BlockData>();
        }
    }
}


