using System;

namespace DiSharper
{
    public interface IBinding
    {
        Type SourceType { get; }
        Type TargetType { get; }
        IScope ToSelf();
        IScope To<TTarget>();
        IScope To(Type targetType);
    }
}
