using System;
using System.Runtime.Serialization;

namespace MISD.SZMDA.Member.Runtime.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : BaseException
    {

        /// <summary>
        /// 实体类型
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// 实体ID
        /// </summary>
        public object Id { get; set; }

        public EntityNotFoundException() { }
        public EntityNotFoundException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context) { }

        public EntityNotFoundException(Type entityType, object id)
            : this(entityType, id, null) { }

        public EntityNotFoundException(Type entityType, object id, System.Exception innerException)
            : base($"找不到ID为{id}的{ entityType.Name }实体。", innerException)
        {
            EntityType = entityType;
            Id = id;
        }

        public EntityNotFoundException(string message) : base(message) { }

        public EntityNotFoundException(string message,System.Exception innerException)
             : base(message, innerException) { }
    }
}
