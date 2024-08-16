using System;
using Bloxorz.CommandControl;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bloxorz.Player
{
    public enum RotationState
    {
        STATIONARY,
        ROTATING
    }

    [RequireComponent(typeof(Cube))]
    public class CubeController : MonoBehaviour
    {
        private Cube cube;

        private void Awake()
        {
            cube = GetComponent<Cube>();
        }
        
        private void Update()
        {
            if (GameManager.instance.State != GameState.PLAYING)
                return;
            if (Cube.instance.isFalling)
                return;
            if (cube.rotationState == RotationState.STATIONARY)
            {
                MovementCommand command = null;
                if (Input.GetKey(KeyCode.W))
                    command = new MovementCommand(Direction.FORWARD);
                else if (Input.GetKey(KeyCode.S))
                    command = new MovementCommand(Direction.BACK);
                else if (Input.GetKey(KeyCode.A)) 
                    command = new MovementCommand(Direction.LEFT);
                else if (Input.GetKey(KeyCode.D)) 
                    command = new MovementCommand(Direction.RIGHT);
                else if (Input.GetKey(KeyCode.Q)) 
                    CommandManager.instance.Undo();
                else if (Input.GetKey(KeyCode.E)) 
                    CommandManager.instance.Redo();
                
                if(command is not null)
                    cube.Execute(command);
            }
        }
    }

}

