using System;
using System.Collections.Generic;
using System.Text;
using Zork.Common;

namespace Zork
{
    internal class ConsoleInputService : IInputService
    {
        public event EventHandler<string> InputReceived;

        public void GetInput()
        {
            string inputString = Console.ReadLine();
            
            if(!string.IsNullOrWhiteSpace(inputString))
            {
                InputReceived?.Invoke(this, inputString);
            }
        }
    }
}
