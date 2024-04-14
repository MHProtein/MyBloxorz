using System;
using UnityEngine;

public enum Direction
{
    FORWARD,
    BACK,
    LEFT,
    RIGHT
}

public class CubeAppr : MonoBehaviour
{
    public AnimationCurve curve;
    public Cube cube;
    public float rotateTime = 0.5f;
    public bool isFalling = false;
    public static CubeAppr instance;
    
    private Direction direction;
    private Vector3 position;
    private Vector3 offset;
    private float rotateTimer = 0.0f;
    private float fallingTimer = 0.0f;
    private CubeState nextState;
    private Vector3 angle;
    private Vector3 targetAngle;
    public RotationState rotationState { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (isFalling)
        {
            if (fallingTimer < 5.0f)
            {
                transform.parent.Translate(0.0f, -(fallingTimer / 5.0f) * 20.0f, 0.0f);
            }
            fallingTimer += Time.deltaTime;
        }
        else if (rotationState == RotationState.ROTATING)
        {
            if (rotateTimer > rotateTime)
            {
                EndRotaion();
                return;
            }
            rotateTimer += Time.deltaTime;
            angle = curve.Evaluate(Mathf.Clamp01(rotateTimer / rotateTime)) * targetAngle;
            Rotate();
        }
    }

    public void BeginRotation(Direction direction)
    {
        rotationState = RotationState.ROTATING;
        this.direction = direction;
        position = transform.position;
        switch (cube.state)
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
                        offset = new Vector3(0.5f, 0.5f,0.0f);
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
    }

    public void Rotate()
    {
        var mat = Matrix4x4.Translate(-offset) * Matrix4x4.Rotate(Quaternion.Euler(angle)) *
                  Matrix4x4.Translate(offset) * Matrix4x4.Rotate(Quaternion.LookRotation(cube.axis_z, cube.axis_y));
        transform.rotation = mat.rotation;
        
        if(cube.state == CubeState.STAND)
            transform.position = mat.GetPosition() + new Vector3(position.x, 1.0f, position.z);
        else
            transform.position = mat.GetPosition() + new Vector3(position.x, 0.5f, position.z);
    }

    public void EndRotaion()
    {
        cube.EndRotation(nextState, targetAngle, transform.position);
        angle = Vector3.zero;
        targetAngle = Vector3.zero;
        rotationState = RotationState.STATIONARY;
        rotateTimer = 0.0f;
        if (!Ground.instance.PlayerOnGround(cube))
        {
            isFalling = true;
            if(GameManager.instance.State != GameState.WIN)
                GameManager.instance.ChangeGameState(GameState.DEAD);
            Debug.Log("end");
        }
    }
}
