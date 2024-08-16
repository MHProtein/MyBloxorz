using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Bloxorz.CommandControl
{
    [Serializable]
    public class CommandManager
    {
        public List<ICommand> commands;
        private int current = -1;
        private int nextUndo = -1;
        private int nextRedo = -1;
        public static CommandManager instance;
        public bool replayed = false;
        
        public CommandManager()
        {
            instance = this;
            commands = new List<ICommand>();
        }
        
        public void Undo()
        {
            if (nextUndo < 0)
                return;
            
            replayed = true;
            current = nextUndo;
            commands[current].Undo();
            nextUndo = current - 1;
            nextRedo = current;
        }

        public void Redo()
        {
            if (nextRedo >= commands.Count)
                return;
            replayed = true;
            current = nextRedo;
            commands[current].Redo();
            nextUndo = current;
            nextRedo = current + 1;
        }

        public void Record(ICommand command)
        {
            if (replayed)
            {
                commands.Clear();
                current = -1;
            }
            commands.Add(command);
            current++;
            nextUndo = current;
            nextRedo = current;
        }
    }
}