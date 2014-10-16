#region

using System;

#endregion

namespace Vehement.Process.Stalker.Diagnostics.ProcessMemory
{
    public class ProcessCheckExceededMaximumSetException : Exception
    {
        public ProcessCheckExceededMaximumSetException()
        {
        }

        public ProcessCheckExceededMaximumSetException(string message) : base(message)
        {
        }

        public ProcessCheckExceededMaximumSetException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}