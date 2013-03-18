namespace DiSharper
{
    public interface IScope
    {
        void InSingletonScope();
        void InTransientScope();
    }
}
