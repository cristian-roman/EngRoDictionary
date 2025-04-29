using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    class WrongWordExceptionEng : Exception
    {
        public WrongWordExceptionEng( string project, string method )
            : base(string.Format("In the project {0}, in method {1}, the system noticed that the word is wrong spelled.", project, method))
        {

        }
    }

    class WrongWordExceptionRom:Exception
    {
        public WrongWordExceptionRom(string project, string method)
           : base(string.Format("In the project {0}, in method {1}, the system noticed that the word is wrong spelled.", project, method))
        {

        }
    }
}
