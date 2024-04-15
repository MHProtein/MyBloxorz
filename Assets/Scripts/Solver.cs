using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Solver : MonoBehaviour
{
    public void Solve()
    {
       GameManager.instance.ChangeGameState(GameState.AUTOSOLVE);
       var results = AStar();
       
       StartCoroutine(Execute(results.steps));
       
       Debug.Log("done");
       GameManager.instance.ChangeGameState(GameState.PLAYING);
    }

    IEnumerator Execute(LinkedList<Direction> steps)
    {
        foreach (var step in steps)
        {
            CubeAppr.instance.BeginRotation(step);   
            yield return new WaitForSeconds(0.5f);
        }
    }

    List<BrickData> GetGroundData()
    {
        List<BrickData> groundData = new List<BrickData>();
        foreach (var brick in Ground.instance.bricks)
        {
            groundData.Add(brick.GetData());
        }

        return GetCopy(groundData);
    }
    
    List<BrickData> GetCopy(List<BrickData> data)
    {
        List<BrickData> copyData = new List<BrickData>(data.Count);
        
        foreach (var item in data)
        {
            if (item is RemovableBrickData)
            {
                copyData.Add(new RemovableBrickData(item as RemovableBrickData));
            }
            else if (item is SwitchData)
            {
                copyData.Add(new SwitchData(item as SwitchData));
            }
            else
            {
                copyData.Add(new BrickData(item));
            }
        }
        return copyData;
    }

    void SetGroundData(List<BrickData> data)
    {
        for (int i = 0; i < data.Count; ++i)
        {
            Ground.instance.bricks[i].SetData(data[i]);
        }
    }
    
    Solution AStar()
    {
        Node currentNode = new Node(CubeAppr.instance.cube);
        Node tempNode = new Node(CubeAppr.instance.cube);
        currentNode.cost = 0;
        currentNode.parent = null;
        currentNode.heuristicValue = currentNode.cost + currentNode.cube.manhattanDistance;
        currentNode.data = GetGroundData();
        List<Node> explored = new List<Node>();

        Solution solution = new Solution(false);
        List<BrickData> oriGroundData = GetGroundData();
        for (int i = 0; i < 10000; i++)
        {
            currentNode.data = GetCopy(oriGroundData);
            solution = Recursion(currentNode, ref explored, i);
            var t = Ground.instance.bricks;
            if (!solution.cutoff)
            {
                SetGroundData(oriGroundData);
                return solution;
            }
            currentNode = tempNode;
            explored.Clear();
        }
        
        return solution;
    }

    Solution Recursion(Node node, ref List<Node> explored, Int64 depth)
    {
        if (depth <= 1)
        {
            return new Solution(false, true);
        }
        depth--;
        if (InExplored(ref node, ref explored))
            return new Solution(false);
        explored.Add(node);
        
        var actions = GetActions(node);
        
        while (actions.Count != 0)
        {
            var action = actions.Dequeue();

            if (action.cube.position1 == Ground.instance.terminalPos)
            {
                Debug.Log("Goal Reached!");
                var solution = new Solution(true);
                solution.steps = GetPath(action);
                return solution;
            }
            
            var sol = Recursion(action, ref explored, depth);
            
            if (sol.solved)
                return sol;
            if (sol.cutoff)
                return sol;
        }
        
        return new Solution(false);
    }

    bool InExplored(ref Node node, ref List<Node> explored)
    {
        foreach (Node nd in explored)
        {
            if(node.cube.position == nd.cube.position && node.action == nd.action)
            {
                return true;
            }
        }
        return false;
    }
    
    PriorityQueue<Node, int> GetActions(Node node)
    {
        Vector3 position = new Vector3();
        Vector3 tempPosition = new Vector3();
        PriorityQueue<Node, int> actions = new PriorityQueue<Node, int>();
        Cube temp;
        var moves = new List<Direction>{ Direction.FORWARD, Direction.BACK, Direction.LEFT, Direction.RIGHT };
        Node newState;
        
        foreach (var move in moves)
        {
            //SetGroundData(node.data);
            temp = new Cube(node.cube);
            tempPosition = temp.position;
            var rotationInfo = Utilities.BeginRotation(temp, move);
            var mat = Utilities.Rotate(temp, rotationInfo.targetAngle, rotationInfo.offset);
            
            if(temp.state == CubeState.STAND)
                position = mat.GetPosition() + new Vector3(tempPosition.x, 1.0f, tempPosition.z);
            else
                position = mat.GetPosition() + new Vector3(tempPosition.x, 0.5f, tempPosition.z);
            temp.EndRotation(rotationInfo.nextState, rotationInfo.targetAngle, position);

            if (!Ground.instance.PlayerOnGround(temp))
                continue;
            
            newState = new Node(temp);
            newState.parent = node;
            newState.data = GetGroundData();
            newState.cube.GetHeuristicValue();
            newState.action = move;
            newState.cost = node.cost + 1;
            newState.heuristicValue = newState.cost + newState.cube.manhattanDistance;
            actions.Enqueue(newState, newState.heuristicValue);
        }
        return actions;
    }
    
    LinkedList<Direction> GetPath(Node node)
    {
        var path = new LinkedList<Direction>();
        GetPathRecursion(node, ref path);
        return path;
    }
    
    void GetPathRecursion(Node node, ref LinkedList<Direction> path)
    {
        if (node.parent != null)
        {
            path.AddFirst(node.action);
            GetPathRecursion(node.parent, ref path);
        }
    }
    
}
