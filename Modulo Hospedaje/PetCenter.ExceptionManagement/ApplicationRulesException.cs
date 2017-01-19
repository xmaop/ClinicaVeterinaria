using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetCenter.ExceptionManagement
{
    public class ApplicationRulesException : Exception
    {
        public ApplicationRulesException(String message)
            : base(message)
        {

        }
    }
}
