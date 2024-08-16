using System;
using Bloxorz.Player;
using UnityEngine;

namespace Bloxorz
{
    public class Utilities
    {
        public static (Vector3 offset, Vector3 targetAngle, CubeState nextState) BeginRotation(CubeData cubeData,
            Direction direction)
        {
            Vector3 offset = Vector3.zero;
            Vector3 targetAngle = Vector3.zero;
            CubeState nextState = CubeState.STAND;
            switch (cubeData.state)
            {
                case CubeState.STAND:
                    switch (direction)
                    {
                        case Direction.FORWARD:
                            offset = new Vector3(0.0f, 1.0f, -0.5f);
                            targetAngle.x = 90.0f;
                            nextState = CubeState.LIE_VERTICAL;
                            break;
                        case Direction.BACK:
                            offset = new Vector3(0.0f, 1.0f, 0.5f);
                            targetAngle.x = -90.0f;
                            nextState = CubeState.LIE_VERTICAL;
                            break;
                        case Direction.LEFT:
                            offset = new Vector3(0.5f, 1.0f, 0.0f);
                            targetAngle.z = 90.0f;
                            nextState = CubeState.LIE_HORIZONTAL;
                            break;
                        case Direction.RIGHT:
                            offset = new Vector3(-0.5f, 1.0f, 0.0f);
                            targetAngle.z = -90.0f;
                            nextState = CubeState.LIE_HORIZONTAL;
                            break;
                    }

                    break;
                case CubeState.LIE_HORIZONTAL:
                    switch (direction)
                    {
                        case Direction.FORWARD:
                            offset = new Vector3(0.0f, 0.5f, -0.5f);
                            targetAngle.x = 90.0f;
                            nextState = CubeState.LIE_HORIZONTAL;
                            break;
                        case Direction.BACK:
                            offset = new Vector3(0.0f, 0.5f, 0.5f);
                            targetAngle.x = -90.0f;
                            nextState = CubeState.LIE_HORIZONTAL;
                            break;
                        case Direction.LEFT:
                            offset = new Vector3(1.0f, 0.5f, 0.0f);
                            targetAngle.z = 90.0f;
                            nextState = CubeState.STAND;
                            break;
                        case Direction.RIGHT:
                            offset = new Vector3(-1.0f, 0.5f, 0.0f);
                            targetAngle.z = -90.0f;
                            nextState = CubeState.STAND;
                            break;
                    }

                    break;
                case CubeState.LIE_VERTICAL:
                    switch (direction)
                    {
                        case Direction.FORWARD:
                            offset = new Vector3(0.0f, 0.5f, -1.0f);
                            targetAngle.x = 90.0f;
                            nextState = CubeState.STAND;
                            break;
                        case Direction.BACK:
                            offset = new Vector3(0.0f, 0.5f, 1.0f);
                            targetAngle.x = -90.0f;
                            nextState = CubeState.STAND;
                            break;
                        case Direction.LEFT:
                            offset = new Vector3(0.5f, 0.5f, 0.0f);
                            targetAngle.z = 90.0f;
                            nextState = CubeState.LIE_VERTICAL;
                            break;
                        case Direction.RIGHT:
                            offset = new Vector3(-0.5f, 0.5f, 0.0f);
                            targetAngle.z = -90.0f;
                            nextState = CubeState.LIE_VERTICAL;
                            break;
                    }

                    break;
            }

            return (offset, targetAngle, nextState);
        }

        public static Matrix4x4 Rotate(CubeData cubeData, Vector3 angle, Vector3 offset)
        {
            return Matrix4x4.Translate(-offset) * Matrix4x4.Rotate(Quaternion.Euler(angle)) *
                   Matrix4x4.Translate(offset) *
                   Matrix4x4.Rotate(Quaternion.LookRotation(cubeData.axis_z, cubeData.axis_y));
        }

    }
}
