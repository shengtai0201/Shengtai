using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shengtai
{
    // https://github.com/unitycontainer/commonservicelocator/blob/master/test/MockServiceLocator.cs
    // https://dotnetcoretutorials.com/2018/05/06/servicelocator-shim-for-net-core/
    public class ServiceLocator : CommonServiceLocator.ServiceLocatorImplBase
    {
        private readonly ServiceProvider serviceProvider;

        public ServiceLocator(ServiceProvider serviceProvider) : base()
        {
            this.serviceProvider = serviceProvider;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return this.serviceProvider.GetServices(serviceType);
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return key == null ? this.serviceProvider.GetService(serviceType) :
                this.serviceProvider.GetServices(serviceType).First(o => o.GetType().FullName == key);
        }
    }
}