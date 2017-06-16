using MISD.SZMDA.Member.Runtime.Attributes;
using System;
using System.Diagnostics;

namespace MISD.SZMDA.Member.Runtime.Tools
{
    [DebuggerStepThrough]
    public static class Check
    {
        [ContractAnnotation("value:null => halt")]
        public static T NotNull<T>(T value, [InvokerParameterName][NotNull]string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
            return value;
        }
    }
}
