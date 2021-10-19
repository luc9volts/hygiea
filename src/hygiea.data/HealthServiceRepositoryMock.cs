using System.Collections.Generic;
using hygiea.domain;

namespace hygiea.data
{
    public sealed class HealthServiceRepositoryMock : HealthServiceRepository
    {
        private readonly Dictionary<string, HealthService> _healthServices;

        public HealthServiceRepositoryMock()
        {
            _healthServices = new Dictionary<string, HealthService>
            {
                ["P001"] = new HealthService { Id = "P001", Price = 100.13M },
                ["P002"] = new HealthService { Id = "P002", Price = 110.13M },
                ["P003"] = new HealthService { Id = "P003", Price = 104.13M },
                ["P004"] = new HealthService { Id = "P004", Price = 100.23M },
                ["P005"] = new HealthService { Id = "P005", Price = 100M },
                ["P006"] = new HealthService { Id = "P006", Price = 105M },
                ["P007"] = new HealthService { Id = "P007", Price = 108.19M },
                ["P008"] = new HealthService { Id = "P008", Price = 100M },
                ["P009"] = new HealthService { Id = "P009", Price = 140M }
            };
        }

        public HealthService GetBy(string id) => _healthServices[id];
    }
}
