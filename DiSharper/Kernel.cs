using System;
using System.Collections.Generic;
using System.Linq;

namespace DiSharper
{
    internal class Kernel : IKernel
    {
        /// <summary>
        /// Based on..: http://ayende.com/blog/2886/building-an-ioc-container-in-15-lines-of-code
        /// Supports..: constuctor injection, property injection, auto-wiring
        /// </summary>
        // TODO: fluent API (scope + more binding), Kernel factory, extract to classes, write tests

        internal delegate object Creator(Kernel container);
        internal readonly Dictionary<Type, Creator> TypeToCreator = new Dictionary<Type, Creator>();
        internal readonly Dictionary<Type, Type> TypeToType = new Dictionary<Type, Type>();
        internal readonly Dictionary<Type, object> Singletons = new Dictionary<Type, object>();

        public IBinding Bind<T>()
        {
            return new Binding(this, typeof(T));
        }

        public IBinding Bind(Type sourceType)
        {
            return new Binding(this, sourceType);
        }

        public IScope Bind<T1, T2>() where T2 : T1
        {
            var sourceType = typeof (T1);
            var targetType = typeof (T2);
            TypeToType.Add(sourceType, targetType);
            return new Scope(this, new Binding(this, sourceType, targetType));
        }

        public IScope Bind(Type sourceType, Type targetType)
        {
            TypeToType.Add(sourceType, targetType);
            return new Scope(this, new Binding(this, sourceType, targetType));
        }

        public void Bind<T>(Creator creator)
        {
            TypeToCreator.Add(typeof(T), creator);
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type serviceType)
        {
            if (Singletons.ContainsKey(serviceType) && Singletons[serviceType] != null)
            {
                return Singletons[serviceType];
            }

            var ctors = serviceType.GetConstructors();
            object instance;

            // If the service has a ctor with parameters and we contain it
            if (ctors.Length > 0 && (TypeToType.ContainsKey(serviceType) || Singletons.ContainsKey(serviceType)))
            {
                // We pick the first ctor per default
                var ctor = ctors[0];
                var parameters = ctor.GetParameters();
                instance = Activator.CreateInstance(serviceType, parameters.Select(parameterInfo => Resolve(parameterInfo.ParameterType)).ToArray());
                InjectProperties(instance);

                if (Singletons.ContainsKey(serviceType))
                {
                    Singletons[serviceType] = instance;
                }

                return instance;
            }

            if (Singletons.ContainsKey(serviceType))
            {
                instance = Singletons[serviceType] ?? Activator.CreateInstance(TypeToType[serviceType]);
                Singletons[serviceType] = instance;
            }
            else
            {
                instance = (TypeToType.ContainsKey(serviceType)) ? Activator.CreateInstance(TypeToType[serviceType]) : null;
            }

            if (instance != null)
                InjectProperties(instance);

            return instance;
        }

        private void InjectProperties(object instance)
        {
            var props = instance.GetType().GetProperties();

            foreach (var propertyInfo in props)
            {
                if (TypeToType.ContainsKey(propertyInfo.PropertyType))
                {
                    propertyInfo.SetValue(instance, Resolve(propertyInfo.PropertyType));
                }
            }
        }
    }
}
