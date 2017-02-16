using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetCenter_GCP.CustomException
{
    public static class ExceptionHelper
    {
        public static string LineNumber(this Exception e)
        {
            string error = string.Empty;
            try
            {
                error = e.StackTrace;
            }
            catch
            {
            }
            return error;
        }
    }
}
