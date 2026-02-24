using System;
using System.Collections.Generic;

namespace ctr_wp7
{
    // Token: 0x0200005A RID: 90
    public class AppServiceProvider : IServiceProvider
    {
        // Token: 0x060002AE RID: 686 RVA: 0x00011868 File Offset: 0x0000FA68
        public void AddService(Type serviceType, object service)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }
            if (!serviceType.IsAssignableFrom(service.GetType()))
            {
                throw new ArgumentException("service does not match the specified serviceType");
            }
            services.Add(serviceType, service);
        }

        // Token: 0x060002AF RID: 687 RVA: 0x000118B7 File Offset: 0x0000FAB7
        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            return services[serviceType];
        }

        // Token: 0x060002B0 RID: 688 RVA: 0x000118D3 File Offset: 0x0000FAD3
        public void RemoveService(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            _ = services.Remove(serviceType);
        }

        // Token: 0x040008A9 RID: 2217
        private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();
    }
}
