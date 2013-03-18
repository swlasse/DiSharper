using System;

namespace DiSharper
{
    internal class Binding : IBinding
    {
        private readonly Kernel _kernel;
        private readonly Type _sourceType;
        private readonly Type _targetType;

        public Binding(Kernel kernel, Type sourceType, Type targetType)
        {
            _kernel = kernel;
            _sourceType = sourceType;
            _targetType = targetType;
        }

        public Binding(Kernel kernel, Type sourceType) : this(kernel, sourceType, null)
        {
        }

        public Type SourceType
        {
            get { return _sourceType; }
        }

        public Type TargetType
        {
            get { return _targetType; }
        }

        public IScope ToSelf()
        {
            _kernel.Bind(_sourceType, _sourceType);
            return new Scope(_kernel, this);
        }

        public IScope To<TTarget>()
        {
            _kernel.Bind(_sourceType, typeof(TTarget));
            return new Scope(_kernel, this);
        }

        public IScope To(Type targetType)
        {
            _kernel.Bind(_sourceType, targetType);
            return new Scope(_kernel, this);
        }
    }
}
