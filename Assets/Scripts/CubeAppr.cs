using System;
using Unity.VisualScripting;
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
    public bool isSolving = false;
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

    private void Start()
    {
        cube.SolvePosition(CubeState.STAND, transform.position);
        cube.GetHeuristicValue();
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
        var ret = Utilities.BeginRotation(cube, this.direction);
        offset = ret.offset;
        targetAngle = ret.targetAngle;
        nextState = ret.nextState;
    }
    
    public void Rotate()
    {
        var mat = Utilities.Rotate(cube, angle, offset);
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
