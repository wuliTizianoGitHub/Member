using System;
using System.Collections.Generic;
using System.Linq;

namespace MISD.SZMDA.Member.Runtime.Dependency
{
    public class ScopedIocResolver : IScopedIocResolver
    {
        private readonly IIocResolver _iocResolver;
        private readonly List<object> _resolvedObjects;

        public ScopedIocResolver(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
            _resolvedObjects = new List<object>();
        }

        public void Dispose()
        {
            _resolvedObjects.ForEach(_iocResolver.Release);
        }

        public bool IsRegistered(Type type)
        {
            return _iocResolver.IsRegistered(type);
        }

        public bool IsRegistered<T>()
        {
            return IsRegistered(typeof(T));
        }

        public void Release(object obj)
        {
            _resolvedObjects.Remove(obj);
            _iocResolver.Release(obj);
        }

        public object Resolve(Type type)
        {
            return Resolve(type, null);
        }

        public object Resolve(Type type, object argumentsAsAnonymousType)
        {
            var resolveObject = argumentsAsAnonymousType != null ? _iocResolver.Resolve(type, argumentsAsAnonymousType) : _iocResolver.Resolve(type);
            _resolvedObjects.Add(resolveObject);
            return resolveObject;
        }

        public T Resolve<T>()
        {
            return Resolve<T>(typeof(T));
        }

        public T Resolve<T>(object argumentsAsAnonymousTypes)
        {
            return (T)Resolve(typeof(T), argumentsAsAnonymousTypes);
        }

        public T Resolve<T>(Type type)
        {
            return Resolve<T>(type);
        }

        public object[] ResolveAll(Type type)
        {
            return ResolveAll(type, null);
        }

        public object[] ResolveAll(Type type, object argumentsAsAnonymousType)
        {
            var resolveObjects = argumentsAsAnonymousType != null ? _iocResolver.ResolveAll(type) : _iocResolver.ResolveAll(type, argumentsAsAnonymousType);
            _resolvedObjects.AddRange(resolveObjects);
            return resolveObjects;
        }

        public T[] ResolveAll<T>()
        {
            return ResolveAll(typeof(T)).OfType<T>().ToArray();
        }

        public T[] ResolveAll<T>(object argumentsAsAnonymousType)
        {
            return ResolveAll(typeof(T), argumentsAsAnonymousType).OfType<T>().ToArray();
        }
    }
}
