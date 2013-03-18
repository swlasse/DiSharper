using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DiSharper.Web
{
    public class DiSharperDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;
        private readonly IDependencyResolver _originalResolver;

        public DiSharperDependencyResolver(IKernel kernel, IDependencyResolver originalResolver)
        {
            _originalResolver = originalResolver;
            _kernel = kernel;
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

        public IKernel Kernel
        {
            get { return _kernel; }
        }
    }
}
