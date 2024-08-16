using System;
using System.Collections;
using System.Collections.Generic;
using Bloxorz.Blocks;
using Bloxorz.CommandControl;
using Bloxorz.Player;
using UnityEngine;
using Utils;

namespace Bloxorz.LevelSolver
{
    public class Solver : MonoBehaviour
    {
        public void Solve()
        {
            GameManager.instance.ChangeGameState(GameState.AUTOSOLVE);
            var results = AStar();

            StartCoroutine(ExecuteSolution(results.steps));
            
            Debug.Log("done");
            GameManager.instance.ChangeGameState(GameState.PLAYING);
        }
        
        private IEnumerator ExecuteSolution(LinkedList<MovementCommand> commands)
        {
            foreach (var command in commands)
            {
                Cube.instance.Execute(command);
                
                yield return new WaitForSeconds(0.5f);
            }
        }

        private List<BlockData> GetGroundData()
        {
            List<BlockData> groundData = new List<BlockData>();
            foreach (var brick in Ground.instance.blocks)
            {
                groundData.Add(brick.GetData());
            }

            return groundData;
        }

        private List<BlockData> GetCopy(List<BlockData> data)
        {
            List<BlockData> copyData = new List<BlockData>(data.Count);

            foreach (var item in data)
            {
                switch (item)
                {
                    case RemovableBlockData removableBlockData:
                        copyData.Add(new RemovableBlockData(removableBlockData));
                        break;
                    case SwitchData switchData:
                        copyData.Add(new SwitchData(switchData));
                        break;
                    default:
                        copyData.Add(new BlockData(item));
                        break;
                }
            }

            return copyData;
        }

        private void SetGroundData(List<BlockData> data)
        {
            for (int i = 0; i < data.Count; ++i)
            {
                Ground.instance.blocks[i].SetData(data[i]);
            }
        }

        private Solution AStar()
        {
            Node currentNode = new Node(Cube.instance.cubeData);
            Node tempNode = new Node(Cube.instance.cubeData);
            currentNode.cost = 0;
            currentNode.parent = null;
            currentNode.heuristicValue = currentNode.cost + currentNode.cubeData.manhattanDistance;
            currentNode.data = GetGroundData();
            List<Node> explored = new List<Node>();

            Solution solution = new Solution(false);
            List<BlockData> oriGroundData = GetGroundData();
            for (int i = 0; i < 10000; i++)
            {
                currentNode.data = GetCopy(oriGroundData);
                solution = Recursion(currentNode, ref explored, i);
                var t = Ground.instance.blocks;
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

        private Solution Recursion(Node node, ref List<Node> explored, Int64 depth)
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

                if (action.cubeData.position1 == Ground.instance.terminalPos 
                    && action.cubeData.state == CubeState.STAND)
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

        private bool InExplored(ref Node node, ref List<Node> explored)
        {
            foreach (Node nd in explored)
            {
                if (node.cubeData.position == nd.cubeData.position && node.command.direction == nd.command.direction)
                {
                    return true;
                }
            }

            return false;
        }

        private PriorityQueue<Node, int> GetActions(Node node)
        {
            Vector3 position = new Vector3();
            Vector3 tempPosition = new Vector3();
            PriorityQueue<Node, int> actions = new PriorityQueue<Node, int>();
            CubeData temp;
            var moves = new List<Direction> { Direction.FORWARD, Direction.BACK, Direction.LEFT, Direction.RIGHT };
            Node newState;
            foreach (var move in moves)
            {
                SetGroundData(GetCopy(node.data));
                temp = new CubeData(node.cubeData);
                tempPosition = temp.position;
                var rotationInfo = Utilities.BeginRotation(temp, move);
                var mat = Utilities.Rotate(temp, rotationInfo.targetAngle, rotationInfo.offset);

                if (temp.state == CubeState.STAND)
                    position = mat.GetPosition() + new Vector3(tempPosition.x, 1.0f, tempPosition.z);
                else
                    position = mat.GetPosition() + new Vector3(tempPosition.x, 0.5f, tempPosition.z);
                temp.EndRotation(rotationInfo.nextState, rotationInfo.targetAngle, position);

                if (!Ground.instance.PlayerOnGround(temp))
                    continue;
                
                newState = new Node(temp);
                newState.parent = node;
                newState.data = GetGroundData();
                newState.cubeData.GetHeuristicValue();
                newState.command = new MovementCommand(move);
                newState.cost = node.cost + 1;
                newState.heuristicValue = newState.cost + newState.cubeData.manhattanDistance;
                actions.Enqueue(newState, newState.heuristicValue);
            }

            return actions;
        }

        private LinkedList<MovementCommand> GetPath(Node node)
        {
            var path = new LinkedList<MovementCommand>();
            GetPathRecursion(node, ref path);
            return path;
        }

        private void GetPathRecursion(Node node, ref LinkedList<MovementCommand> path)
        {
            if (node.parent != null)
            {
                path.AddFirst(node.command);
                GetPathRecursion(node.parent, ref path);

            }
        }
    }
}