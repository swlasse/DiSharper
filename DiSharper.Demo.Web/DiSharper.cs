//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using MvcApplication4.Models;
//using MvcApplication4.Controllers;

//namespace MvcApplication4
//{
//    /// <summary>
//    /// Based on..: http://ayende.com/blog/2886/building-an-ioc-container-in-15-lines-of-code
//    /// Support...: constuctor injection, property injection, auto-wiring
//    /// </summary>
//    public class DiSharper : IDependencyResolver
//    {
//        // TODO: fluent API

//        public delegate object Creator(DiSharper container);
//        private readonly Dictionary<Type, Creator> _typeToCreator = new Dictionary<Type, Creator>();
//        private readonly Dictionary<Type, Type> _typeToType = new Dictionary<Type, Type>(); 
//        private readonly IDependencyResolver _originalResolver;

//        public DiSharper(IDependencyResolver originalResolver)
//        {
//            _originalResolver = originalResolver;
//        }

//        public void Bind<T>(Creator creator)
//        {
//            _typeToCreator.Add(typeof(T), creator);
//        }

//        public void Bind<TI, TC>() where TC : TI
//        {
//            _typeToType.Add(typeof(TI), typeof(TC));
//        }

//        private object Resolve(Type serviceType)
//        {
//            var ctors = serviceType.GetConstructors();
//            object instance;

//            // If the service has a ctor with parameters and we contain it
//            if (ctors.Length > 0 && _typeToType.ContainsKey(serviceType))
//            {
//                // We pick the first ctor per default
//                var ctor = ctors[0];
//                var parameters = ctor.GetParameters();
//                instance = Activator.CreateInstance(serviceType, parameters.Select(parameterInfo => Resolve(parameterInfo.ParameterType)).ToArray());
//                InjectProperties(instance);
//                return instance;
//            }

//            instance = (_typeToType.ContainsKey(serviceType)) ? Activator.CreateInstance(_typeToType[serviceType]) : null;
            
//            if (instance != null)
//                InjectProperties(instance);
                
//            return instance;
//        }

//        private void InjectProperties(object instance)
//        {
//            var props = instance.GetType().GetProperties();

//            foreach (var propertyInfo in props)
//            {
//                if (_typeToType.ContainsKey(propertyInfo.PropertyType))
//                {
//                    propertyInfo.SetValue(instance, Resolve(propertyInfo.PropertyType));
//                }
//            }
//        }

//        public T GetService<T>()
//        {
//            return (T)Resolve(typeof(T));
//        }

//        public object GetService(Type serviceType)
//        {
//            return Resolve(serviceType) ?? _originalResolver.GetService(serviceType);
//        }

//        public IEnumerable<object> GetServices(Type serviceType)
//        {
//            // Currently we do not support multiple bindings against same type
//            var instance = Resolve(serviceType);
//            return (instance != null) ? new[] {instance} : _originalResolver.GetServices(serviceType);
//        }
//    }
//}