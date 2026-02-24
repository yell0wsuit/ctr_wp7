using System;
using System.Collections.Generic;

namespace ctr_wp7
{
    public class AppServiceProvider : IServiceProvider
    {
        public void AddService(Type serviceType, object service)
        {
            ArgumentNullException.ThrowIfNull(serviceType);
            ArgumentNullException.ThrowIfNull(service);
            if (!serviceType.IsAssignableFrom(service.GetType()))
            {
                throw new ArgumentException("service does not match the specified serviceType");
            }
            services.Add(serviceType, service);
        }

        public object GetService(Type serviceType)
        {
            ArgumentNullException.ThrowIfNull(serviceType);
            return services[serviceType];
        }

        public void RemoveService(Type serviceType)
        {
            ArgumentNullException.ThrowIfNull(serviceType);
            _ = services.Remove(serviceType);
        }

        private readonly Dictionary<Type, object> services = [];
    }
}
