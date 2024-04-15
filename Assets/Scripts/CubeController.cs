using System;
using UnityEngine;
using UnityEngine.UIElements;

public enum RotationState
{
    STATIONARY,
    ROTATING
}

[RequireComponent(typeof(CubeAppr))]
public class CubeController : MonoBehaviour
{
    private CubeAppr cube;

    private void Awake()
    {
        cube = GetComponent<CubeAppr>();
    }

    private void Update()
    {
        if (GameManager.instance.State != GameState.PLAYING)
            return;
        if (CubeAppr.instance.isFalling)
            return;
        if (cube.rotationState == RotationState.STATIONARY)
        {
            if (Input.GetKey(KeyCode.W))
            {
                cube.BeginRotation(Direction.FORWARD);
            }
            if (Input.GetKey(KeyCode.S))
            {
                cube.BeginRotation(Direction.BACK);
            }
            if (Input.GetKey(KeyCode.A))
            {
                cube.BeginRotation(Direction.LEFT);
            }
            if (Input.GetKey(KeyCode.D))
            {
                cube.BeginRotation(Direction.RIGHT);
            }
        }
    }
}
