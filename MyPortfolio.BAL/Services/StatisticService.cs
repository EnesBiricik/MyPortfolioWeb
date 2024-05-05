using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyPortfolio.BAL.Interfaces;
using MyPortfolio.Common;
using MyPortfolio.DAL.UnitOfWork;
using MyPortfolio.Dtos;
using MyPortfolio.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.BAL.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IUow _uow;

        public StatisticService(IUow uow)
        {
            _uow = uow;
        }

        public async Task CreateAsync()
        {
            var newStat = new Statistic
            {
                Date = DateTime.Now,
                VisitorCount = 0
            };
            await _uow.GetRepository<Statistic>().CreateAsync(newStat);
            await _uow.SaveChanges();

        }

        public async Task SeedAsync()
        {
            // Bugünden geriye doğru 30 gün boyunca seed veri oluşturulur
            DateTime currentDate = DateTime.Today;
            for (int i = 0; i < 30; i++)
            {
                var statistic = new Statistic
                {
                    Date = currentDate.AddDays(-i),
                    VisitorCount = 0
                };

                await _uow.GetRepository<Statistic>().CreateAsync(statistic);
            }
            await _uow.SaveChanges();
        }

        public async Task<IResponse<List<StatisticListDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<Statistic>().GetAllAsync();
            //var dto = _mapper.Map<List<StatisticListDto>>(data);

            var dto = data.Select(x => new StatisticListDto
            {
                date = x.Date.ToString()/*("dd/MM/yyyy")*/,
                value = x.VisitorCount
            }).ToList();

            return new Response<List<StatisticListDto>>(ResponseType.Success, dto);
        }

        public async Task IncreaseVisitor()
        {
            var lastDay = _uow.GetRepository<Statistic>()
           .GetQuery()
           .OrderByDescending(x => x.Date)
           .FirstOrDefault(); //it means today actually
            if (lastDay != null)
            {
                lastDay.VisitorCount += 1;

                await _uow.SaveChanges();

            }
        }

        public async Task IncreaseVisitorSettings()
        {
            var todayVisitor = _uow.GetRepository<Statistic>().GetQuery().OrderByDescending(x=> x.Date).First().VisitorCount;

            var settings = _uow.GetRepository<PageSettings>()
           .GetQuery()
           .First(); //it means today actually
            if (settings != null)
            {
                settings.VisitorCount += todayVisitor;
                await _uow.SaveChanges();
            }
        }

        public async Task RemoveAsync(Statistic statistic)
        {
            _uow.GetRepository<Statistic>().Remove(statistic);
            await _uow.SaveChanges();
        }

        public IQueryable<Statistic> GetQuery()
        {

            return _uow.GetRepository<Statistic>().GetQuery();

        }

    }
}
