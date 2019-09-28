using DLearnRepositories.UnitOfWork;
using DLearnServices.Interfaces;
using DLearnServices.Services;
using Unity;

namespace DLearnInfrastructure.Unity
{
    public static class UnityConfig
    {
        public static UnityContainer RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            container.RegisterType<IUserService, UserService>();

            return container;
        }
    }
}
