using System;

namespace DiSharper
{
    public interface IKernel
    {
        IBinding Bind<T>();
        T Resolve<T>();
        object Resolve(Type serviceType);
    }
}
