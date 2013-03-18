using System.Web.Mvc;
using DiSharper.Web;
using MvcApplication4.Controllers;
using MvcApplication4.Models;

namespace MvcApplication4.App_Start
{
    public static class Bootstrapper
    {
        public static void Initialize()
        {
            var container = new Kernel(DependencyResolver.Current);
            container.Bind<HomeController>().ToSelf();
            container.Bind<ICar, Ferrari>(); //Ferrari, Ford
            DependencyResolver.SetResolver(container);
        }
    }

    //public class MyDependencyResolver : IDependencyResolver
    //{
    //    private readonly DiLite _container;
    //    private readonly IDependencyResolver _resolver;

    //    public MyDependencyResolver(DiLite container, IDependencyResolver resolver)
    //    {
    //        _container = container;
    //        _resolver = resolver;
    //    }

    //    public object GetService(Type serviceType)
    //    {
    //        try
    //        {
    //            return _container.Resolve(serviceType);
    //        }
    //        catch (Exception)
    //        {
    //            return _resolver.GetService(serviceType);
    //        }
            
    //    }

    //    public IEnumerable<object> GetServices(Type serviceType)
    //    {
    //        return _resolver.GetServices(serviceType);
    //    }
    //}
}