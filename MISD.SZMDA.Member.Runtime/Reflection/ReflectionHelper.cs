using System;

namespace MISD.SZMDA.Member.Runtime.Reflection
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// 检查 <paramref name="givenType"/>实现/继承 <paramref name="genericType"/> 其中之一。
        /// </summary>
        /// <param name="givenType">需要进行检查的类型</param>
        /// <param name="genericType">被比较的泛型类型</param>
        /// <returns></returns>
        public static bool IsAssignableToGenericType(Type givenType,Type genericType)
        {
            //判断需要进行检查的类型是否是genericType
            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            foreach (var interfaceType in givenType.GetInterfaces())
            {
                //判断需要进行检查的类型的接口类型是否是genericType
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            //判断类型继承的父类类型
            if (givenType.BaseType == null)
            {
                return false;
            }

            //根据需要进行检查的类型的父类类型依次往上检查
            return IsAssignableToGenericType(givenType.BaseType, genericType);
        }
    }
}
