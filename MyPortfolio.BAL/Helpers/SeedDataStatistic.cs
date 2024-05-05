using MyPortfolio.BAL.Interfaces;
using MyPortfolio.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.BAL.Helpers
{
    public class SeedDataStatistic
    {
        private readonly IStatisticService _service;

        public SeedDataStatistic(IStatisticService service)
        {
            _service = service;
        }

        public void UpdateDates()
        {
            var oldestStat = _service.GetQuery().OrderBy(s => s.Date).FirstOrDefault();
            if (oldestStat != null)
            {
                _service.RemoveAsync(oldestStat);
            }

            _service.IncreaseVisitorSettings();
            _service.CreateAsync();
        }

        public async Task Seed()
        {
            if (!_service.GetQuery().Any())
            {
                await _service.SeedAsync();
            }
        }
    }
}
