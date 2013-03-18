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
            var container = DiSharper.DiSharper.SingletonKernel;
            container.Bind<HomeController>().ToSelf();
            container.Bind<ICar, Ferrari>(); //Ferrari, Ford
            DependencyResolver.SetResolver(new DiSharperDependencyResolver(container, DependencyResolver.Current));
        }
    }
}