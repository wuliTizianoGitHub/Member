using System;

namespace MISD.SZMDA.Member.Runtime.Dependency
{
    /// <summary>
    ///  <see cref="IIocRegistrar"/>接口的扩展方法
    /// </summary>
    public static class IocRegistrarExtensions
    {
        /// <summary>
        /// 注册类型，如果在此之前没有注册过
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iocRegistrar"></param>
        /// <param name="lifestyle"></param>
        /// <returns></returns>
        public static bool RegisterIfNot<T>(this IIocRegistrar iocRegistrar, DependencyLifeStyle lifestyle = DependencyLifeStyle.Singleton)
            where T : class
        {
            if (iocRegistrar.IsRegistered<T>())
            {
                return false;
            }
            iocRegistrar.Register<T>(lifestyle);
            return true;
        }

        public static bool RegisterIfNot(this IIocRegistrar iocRegistrar, Type type, DependencyLifeStyle lifestyle = DependencyLifeStyle.Singleton)
        {
            if (iocRegistrar.IsRegistered(type))
            {
                return false;
            }
            iocRegistrar.Register(type, lifestyle);
            return true;
        }

        public static bool RegisterIfNot<TType, TImpl>(this IIocRegistrar iocRegistrar, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
           where TType : class
           where TImpl : class, TType
        {
            if (iocRegistrar.IsRegistered<TType>())
            {
                return false;
            }

            iocRegistrar.Register<TType, TImpl>(lifeStyle);
            return true;
        }

        public static bool RegisterIfNot(this IIocRegistrar iocRegistrar, Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            if (iocRegistrar.IsRegistered(type))
            {
                return false;
            }

            iocRegistrar.Register(type, impl, lifeStyle);
            return true;
        }
    }
}
