using System.Collections.Generic;

public class Solution
{
    public bool solved = false;
    public bool cutoff;
    public LinkedList<Direction> steps;

    public Solution(bool solved)
    {
        this.solved = solved;
        steps = new LinkedList<Direction>();
    }

    public Solution(bool solved, bool cutoff)
    {
        this.solved = solved;
        this.cutoff = cutoff;
        steps = new LinkedList<Direction>();
    }
}