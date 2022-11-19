using IPC.Correios.Web.Repository;
using IPC.Correios.Web.Repository.Interface;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IPC.Correios.Web.App_Start
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel Kernel;
        public NinjectControllerFactory()
        {
            Kernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(
            RequestContext requestContext,
            Type controllerType)
        {
            return controllerType != null
                ? SetController(controllerType)
                : null;
        }

        private void AddBindings()
        {
            Kernel.Bind<IBuscarCepRepository>().To<BuscarCepRepository>();
        }

        private IController SetController(Type controllerType)
        {
            return (IController)Kernel.Get(controllerType);
        }

        public object GetService(Type serviceType)
        {
            return this.Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.Kernel.GetAll(serviceType);
        }
    }
}