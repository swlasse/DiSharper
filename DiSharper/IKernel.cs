using System;

namespace DiSharper
{
    public interface IKernel
    {
        IBinding Bind<T>();
        IBinding Bind(Type sourceType);
        IScope Bind<T1, T2>() where T2 : T1;
        IScope Bind(Type sourceType, Type targetType);
        T Resolve<T>();
        object Resolve(Type serviceType);
    }
}
