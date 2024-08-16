using Bloxorz.Blocks;
using Bloxorz.CommandControl;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bloxorz.Player
{
    public enum Direction
    {
        FORWARD,
        BACK,
        LEFT,
        RIGHT
    }

    public class Cube : MonoBehaviour
    {
        public AnimationCurve curve;
        [FormerlySerializedAs("cube")] public CubeData cubeData;
        public float rotateTime = 0.5f;
        public bool isFalling = false;
        public static Cube instance;
        
        private Direction direction;
        private Vector3 position;
        private float rotateTimer = 0.0f;
        private float fallingTimer = 0.0f;
        private Vector3 angle;
        
        private CubeState nextState;
        private Vector3 offset;
        private Vector3 targetAngle;
        public RotationState rotationState { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            cubeData.SolvePosition(CubeState.STAND, transform.position);
            cubeData.GetHeuristicValue();
        }

        public void Execute(MovementCommand command)
        {
            BeginRotation(command);
            CommandManager.instance.Record(command);
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

        public void BeginRotation(MovementCommand command)
        {
            rotationState = RotationState.ROTATING;
            this.direction = command.direction;
            position = transform.position;
            var ret = Utilities.BeginRotation(cubeData, this.direction);
            
            offset = ret.offset;
            targetAngle = ret.targetAngle;
            nextState = ret.nextState;
        }
        
        public void Rotate()
        {
            var mat = Utilities.Rotate(cubeData, angle, offset);
            transform.rotation = mat.rotation;
            
            if(cubeData.state == CubeState.STAND)
                transform.position = mat.GetPosition() + new Vector3(position.x, 1.0f, position.z);
            else
                transform.position = mat.GetPosition() + new Vector3(position.x, 0.5f, position.z);
        }

        public void EndRotaion()
        {
            cubeData.EndRotation(nextState, targetAngle, transform.position);
            angle = Vector3.zero;
            targetAngle = Vector3.zero;
            rotationState = RotationState.STATIONARY;
            rotateTimer = 0.0f;
            if (!Ground.instance.PlayerOnGround(cubeData))
            {
                isFalling = true;
                if(GameManager.instance.State != GameState.WIN)
                    GameManager.instance.ChangeGameState(GameState.DEAD);
            }
        }
    }
}


