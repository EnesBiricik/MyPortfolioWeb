using Microsoft.AspNetCore.Http;
using MyPortfolio.Common;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.BAL.Interfaces
{
    public interface IStatisticService 
    {
        Task CreateAsync();
        Task IncreaseVisitor();
        Task RemoveAsync(Statistic statistic);
        Task<IResponse<List<StatisticListDto>>> GetAllAsync();
        IQueryable<Statistic> GetQuery();
        Task SeedAsync();
        Task IncreaseVisitorSettings();
    }
}
