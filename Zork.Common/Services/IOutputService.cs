using System;
using System.Collections.Generic;
using System.Text;

namespace Zork.Common
{
    public interface IOutputService
    {
        void Write(string value);

        void Write(object value);

        void WriteLine(string value);

        void WriteLine(object value);

        void Clear();
        
    }
}
