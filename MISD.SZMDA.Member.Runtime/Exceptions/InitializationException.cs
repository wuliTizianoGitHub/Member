using System;
using System.Runtime.Serialization;

namespace MISD.SZMDA.Member.Runtime.Exceptions
{
    /// <summary>
    /// 如果问题出在initialization处理中，则抛出这个异常
    /// </summary>
    [Serializable]
    public class InitializationException :BaseException
    {
        public InitializationException()
        {

        }

        public InitializationException(SerializationInfo serializationInfo, StreamingContext context)
            :base(serializationInfo,context)
        {

        }

        public InitializationException(string message)
            :base(message)
        {

        }

        public InitializationException(string message, System.Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
