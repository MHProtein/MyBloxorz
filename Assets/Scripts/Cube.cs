using System;
using UnityEngine;
using UnityEngine.UIElements;

public enum CubeState
{
    STAND,
    LIE_HORIZONTAL,
    LIE_VERTICAL
}

[Serializable]
public class Cube
{
    public CubeState state;
    public Vector3 axis_y = Vector3.up;
    public Vector3 axis_z = Vector3.forward;
    public Vector3 position1;
    public Vector3 position2;

    public void EndRotation(CubeState newState, Vector3 targetAngle, Vector3 position)
    {
        this.state = newState;
        switch (newState)
        {
            case CubeState.STAND:
                position1 = position;
                position2 = position;
                break;
            case CubeState.LIE_HORIZONTAL:
                position1 = position;
                position1.x -= 0.5f;
                position2 = position;
                position2.x += 0.5f;
                break;
            case CubeState.LIE_VERTICAL:
                position1 = position;
                position1.z -= 0.5f;
                position2 = position;
                position2.z += 0.5f;
                break;
        }
        position1.y = 0.0f;
        position2.y = 0.0f;
        var mat = Matrix4x4.Rotate(Quaternion.Euler(targetAngle));
        axis_y = mat * axis_y;
        axis_z = mat * axis_z;
    }
}
