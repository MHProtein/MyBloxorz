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
        if (cube.rotationState == RotationState.STATIONARY)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                cube.BeginRotation(Direction.FORWARD);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                cube.BeginRotation(Direction.BACK);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                cube.BeginRotation(Direction.LEFT);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                cube.BeginRotation(Direction.RIGHT);
            }
        }
    }
}
