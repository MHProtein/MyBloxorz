using System.Collections.Generic;
using Bloxorz.CommandControl;
using Bloxorz.Player;

namespace Bloxorz.LevelSolver
{
    public class Solution
    {
        public bool solved = false;
        public bool cutoff;
        public LinkedList<MovementCommand> steps;

        public Solution(bool solved)
        {
            this.solved = solved;
            steps = new LinkedList<MovementCommand>();
        }

        public Solution(bool solved, bool cutoff)
        {
            this.solved = solved;
            this.cutoff = cutoff;
            steps = new LinkedList<MovementCommand>();
        }
    }
}