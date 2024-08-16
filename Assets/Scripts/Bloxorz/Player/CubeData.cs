using System;
using Bloxorz.Blocks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Bloxorz.Player
{
    public enum CubeState
    {
        STAND,
        LIE_HORIZONTAL,
        LIE_VERTICAL
    }

    [Serializable]
    public class CubeData
    {
        public CubeState state;
        public Vector3 axis_y = Vector3.up;
        public Vector3 axis_z = Vector3.forward;
        public Vector3 position;
        public Vector3Int position1 = new Vector3Int();
        public Vector3Int position2 = new Vector3Int();
        public Vector3 tempPosition1 = new Vector3();
        public Vector3 tempPosition2 = new Vector3();
        public int manhattanDistance = 0;

        public CubeData()
        {
            position = new Vector3();
        }

        public CubeData(CubeData cubeData)
        {
            this.state = cubeData.state;
            this.axis_y = cubeData.axis_y;
            this.axis_z = cubeData.axis_z;
            this.position1 = cubeData.position1;
            this.position2 = cubeData.position2;
            position = cubeData.position;
            this.manhattanDistance = cubeData.manhattanDistance;
        }
        
        public void EndRotation(CubeState newState, Vector3 targetAngle, Vector3 position)
        {
            this.position = position;
            this.state = newState;
            SolvePosition(newState, position);
            
            var mat = Matrix4x4.Rotate(Quaternion.Euler(targetAngle));
            axis_y = mat * axis_y;
            axis_z = mat * axis_z;
            
            GetHeuristicValue();
        }

        public void SolvePosition(CubeState newState, Vector3 position)
        {
            switch (newState)
            {
                case CubeState.STAND:
                    tempPosition1 = position;
                    tempPosition2 = position;
                    break;
                case CubeState.LIE_HORIZONTAL:
                    tempPosition1 = position;
                    tempPosition1.x -= 0.5f;
                    tempPosition2 = position;
                    tempPosition2.x += 0.5f;
                    break;
                case CubeState.LIE_VERTICAL:
                    tempPosition1 = position;
                    tempPosition1.z -= 0.5f;
                    tempPosition2 = position;
                    tempPosition2.z += 0.5f;
                    break;
            }

            position1.x = Mathf.RoundToInt(tempPosition1.x);
            position1.z = Mathf.RoundToInt(tempPosition1.z);
            position1.y = 0;
            position2.x = Mathf.RoundToInt(tempPosition2.x);
            position2.z = Mathf.RoundToInt(tempPosition2.z);
            position2.y = 0;
        }

        public void GetHeuristicValue()
        {
            manhattanDistance = Math.Max(Math.Abs(position1.x - Ground.instance.terminalPos.x),
                                         Math.Abs(position1.z - Ground.instance.terminalPos.z));
        }
    }
}

