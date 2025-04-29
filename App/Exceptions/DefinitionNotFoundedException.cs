using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    class DefinitionNotFoundedException: Exception
    {
        public DefinitionNotFoundedException(string message)
            :base(message)
        {

        }
    }
}
