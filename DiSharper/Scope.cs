namespace DiSharper
{
    internal class Scope : IScope
    {
        private readonly IBinding _binding;
        private readonly Kernel _kernel;

        public Scope(Kernel kernel, IBinding binding)
        {
            _binding = binding;
            _kernel = kernel;
        }

        public void InSingletonScope()
        {
            _kernel.Singletons.Add(_binding.SourceType, null);
        }

        public void InTransientScope()
        {
            // Default
        }
    }
}
