using System;

namespace DiSharper
{
    public static class DiSharper
    {
        private static readonly Lazy<IKernel> LazyKernel = new Lazy<IKernel>(() => new Kernel());
        
        public static IKernel SingletonKernel
        {
            get
            {
                return LazyKernel.Value;
            }
        }
        
        public static IKernel Kernel
        {
            get
            {
                return new Kernel();
            }
        }
    }
}
