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
    public Vector3Int position1;
    public Vector3Int position2;
    public Vector3 tempPosition1;
    public Vector3 tempPosition2;

    public void EndRotation(CubeState newState, Vector3 targetAngle, Vector3 position)
    {
        this.state = newState;
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
        var mat = Matrix4x4.Rotate(Quaternion.Euler(targetAngle));
        axis_y = mat * axis_y;
        axis_z = mat * axis_z;
    }
}
