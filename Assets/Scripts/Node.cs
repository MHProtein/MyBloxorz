
using System.Collections.Generic;

public class Node
{
    public Cube cube;
    public Node parent;
    public Direction action;
    public int heuristicValue;
    public int cost;
    public List<BrickData> data;
    
    public Node()
    {
        cost = 0;
        heuristicValue = 0;
        data = new List<BrickData>();
    }
    
    public Node(Cube cube)
    {
        this.cube = cube;
        cost = 0;
        heuristicValue = 0;
        data = new List<BrickData>();
    }
}
