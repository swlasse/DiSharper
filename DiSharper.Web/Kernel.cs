using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DiSharper.Web
{
    public class Kernel : IDependencyResolver
    {
        private readonly IKernel _kernel;
        private readonly IDependencyResolver _originalResolver;

        public Kernel(IDependencyResolver originalResolver)
        {
            _originalResolver = originalResolver;
            _kernel = DiSharper.Kernel;
        }

        public object GetService(Type serviceType)
        {
            return _kernel.Resolve(serviceType) ?? _originalResolver.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            // Currently we do not support multiple bindings against same type
            var instance = _kernel.Resolve(serviceType);
            return (instance != null) ? new[] { instance } : _originalResolver.GetServices(serviceType);
        }
    }
}
