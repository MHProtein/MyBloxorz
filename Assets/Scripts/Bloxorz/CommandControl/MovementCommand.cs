using System;
using Bloxorz.Player;
using UnityEngine;

namespace Bloxorz.CommandControl
{
    public class MovementCommand : ICommand
    {
        public Direction direction;
        public Direction reverseDirection;
        
        public MovementCommand(Direction d)
        {
            direction = d;
            switch (direction)
            {
                case Direction.FORWARD:
                    reverseDirection = Direction.BACK;
                    break;
                case Direction.BACK:
                    reverseDirection = Direction.FORWARD;
                    break;
                case Direction.LEFT:
                    reverseDirection = Direction.RIGHT;
                    break;
                case Direction.RIGHT:
                    reverseDirection = Direction.LEFT;
                    break;
            }
        }
        
        public void Undo()
        {
            Cube.instance.BeginRotation(new MovementCommand(reverseDirection));

        }

        public void Redo()
        {

            Cube.instance.BeginRotation(this);
        }

        public string ToString()
        {
            return direction.ToString();
        }
    }
}