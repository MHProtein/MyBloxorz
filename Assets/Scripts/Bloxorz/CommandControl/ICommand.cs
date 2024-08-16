


using System;

namespace Bloxorz.CommandControl
{
    public interface ICommand
    {
        public void Undo();
        public void Redo();
        
    }
}