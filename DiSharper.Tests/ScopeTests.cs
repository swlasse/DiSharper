using NUnit.Framework;

namespace DiSharper.Tests
{
    [TestFixture]
    public class ScopeTests
    {
        [Test]
        public void SingletonScopeTest()
        {
            var kernel = DiSharper.Kernel;
            kernel.Bind<ICar>().To<Ford>().InSingletonScope();
            var ford = kernel.Resolve<ICar>();
            ford.Kilometers++;
            ford = kernel.Resolve<ICar>();
            ford.Kilometers++;
            Assert.AreEqual(2, ford.Kilometers);
        }

        [Test]
        public void TransientScopeTest()
        {
            var kernel = DiSharper.Kernel;
            kernel.Bind<ICar>().To<Ford>();
            var ford = kernel.Resolve<ICar>();
            ford.Kilometers++;
            ford = kernel.Resolve<ICar>();
            ford.Kilometers++;
            Assert.AreEqual(1, ford.Kilometers);
        }
    }
}
